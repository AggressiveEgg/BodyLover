using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public bool Active = false;

    [SerializeField]

    Controller control;
    AnimationController animation;

    [Header("Setting")]
    [SerializeField] float maxSpeed;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] int playerindex;


    [SerializeField] GameObject player;

    Event e;
    [SerializeField]bool isJump;
    [SerializeField] bool isWall;
    Rigidbody rb;

    Coroutine AttackCorotine;
    bool isAction = false;
    // Use this for initialization
    void Start()
    {
        control = new Controller();
        animation = new AnimationController(this);
        rb = GetComponent<Rigidbody>();
        control.SetPlayerJoyStick(playerindex);
        e = new Event();

        maxSpeed = CommonConfig.MaxSpeed;
        jumpForce = CommonConfig.Jump;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active || isAction)
            return;
        
        checkGround();
        attack();
        control.MoveController(null);
        ForceToDirection();
        MoveTarget();
        animation.RotateDirection(rb.velocity);
    }


    public void ForceToDirection()
    {
        control.Jump(() => 
        {
            if (!isJump)
            {
                isJump = true;
                animation.playAnimBool("IsJump", true);
                rb.velocity = new Vector3(rb.velocity.x, jumpForce);
                vfxManager.instance.PlayVFX("Jump",this.transform.position,0.25f);
            }
        });


    }
    public void MoveTarget()
    {
        if (!isJump)
        {
            float moveX = Mathf.Clamp(speed * control.Force, -maxSpeed, maxSpeed);
            animation.playAnimBool("IsRun",rb.velocity.x != 0);

            rb.velocity = new Vector3(moveX, rb.velocity.y);   
        }
        else if(isJump)
        {
            
            if (CanMove(rb.velocity.normalized) && !isWall)
            {
                float moveX = Mathf.Clamp(rb.velocity.x + (speed * (control.Force / 2)) / 2, -1 * (maxSpeed / 2), maxSpeed / 2);
                rb.velocity = new Vector3(moveX, rb.velocity.y);
            }
            else
            {
                isWall = true;
                rb.velocity = new Vector3(0, -10, 0);
            }
        }
    }

    bool CanMove(Vector3 Dir)
    {
        Dir.x = Mathf.Round(Dir.x);
        Dir.y = 0;
        Dir.z = 0;

        RaycastHit hit;
        float range = 0.5f;
        return !(Physics.Raycast(this.transform.localPosition - new Vector3(0, this.GetComponent<Collider>().bounds.size.x/2, 0), Dir, out hit, range) || Physics.Raycast(this.transform.localPosition + new Vector3(0, this.GetComponent<Collider>().bounds.size.x / 2, 0), Dir, out hit, range));
    }

    void checkGround()
    {
        if (!isJump || rb.velocity.y > 0.0f)
            return;
        
        //print(rb.velocity.y);
        float range = 0.5f;
        RaycastHit hit;
        float sizeX = this.GetComponent<Collider>().bounds.size.x / 4;
        Vector3 ray1 = new Vector3(sizeX,0,0);
        if (Physics.Raycast(this.transform.position + ray1, Vector3.down , out hit, range) || Physics.Raycast(this.transform.position - ray1, Vector3.down, out hit, range))
        {
            if ((hit.collider.gameObject.tag == "ground" ))
            {
                Debug.DrawRay(this.transform.position + ray1, Vector3.down * range, Color.green, 2.0f);
                Debug.DrawRay(this.transform.position - ray1, Vector3.down * range, Color.green, 2.0f);
                HitFunction();
            }
        }
    }

    public void HitFunction()
    {
        animation.playAnimBool("IsJump", false);
        isJump = false;
        isWall = false;
    }

    public void Reset()
    {
        isAction = false;
        Active = true;
        HitFunction();
        control.Force = 0;
        rb.velocity = new Vector3(0,0,0);
    }

    public void attack()
    {
        control.Item(() =>
        {
        animation.RotateAttack();
        animation.playAnimTriiger("isAttack");
        RaycastAttack(new Vector3(animation.right,0,0));
        });
    }

    public void RaycastAttack(Vector3 Dir)
    {
        if (AttackCorotine != null)
            StopCoroutine(AttackCorotine);
        AttackCorotine = StartCoroutine(Attacking(Dir));
    }

    IEnumerator Attacking(Vector3 Dir)
    {
        isAction = true;
        List<PlayerController> listP = new List<PlayerController>();
        List<WhiteBlood> listE = new List<WhiteBlood>();
        float Maxtime_anim = CommonConfig.attackTime;
        float time_anim = Maxtime_anim;
        while (true)
        {
            if (time_anim <= Maxtime_anim/2)
            {
                Dir.x = Mathf.Round(Dir.x);
                Dir.y = 0;
                Dir.z = 0;
                float range = CommonConfig.AttackRange;
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position, Dir, out hit, range))
                {
                    if (hit.collider.tag == "Player")
                    {
                        int index = listP.FindIndex(x => x == hit.collider.GetComponent<PlayerController>());
                        if (index == -1)
                        {
                            hit.collider.GetComponent<PlayerController>().Force(Dir);
                            listP.Add(hit.collider.GetComponent<PlayerController>());
                            vfxManager.instance.PlayVFX("Attack",hit.point,0.5f);
                        }
                    }

                    if(hit.collider.tag == "Enemy")
                    {
                        int index = listE.FindIndex(x => x == hit.collider.GetComponent<WhiteBlood>());
                        if (index == -1)
                        {
                            hit.collider.GetComponent<WhiteBlood>().Force(Dir);
                            listE.Add(hit.collider.GetComponent<WhiteBlood>());
                            vfxManager.instance.PlayVFX("Attack", hit.point, 0.5f);
                        }
                    }
                }

                Debug.DrawRay(this.transform.position, Dir * range, Color.red);
            }
            if(time_anim <= 0)
            {
                break;
            }
            else
            {
                time_anim -= Time.deltaTime;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        control.Force = 0;
        isAction = false;
        yield return null;
    }

    public void Force(Vector3 dir)
    {
        
        animation.RotateAttack();
        if (AttackCorotine != null)
            StopCoroutine(AttackCorotine);
        AttackCorotine = StartCoroutine(Forcing(dir));
    }

    IEnumerator Forcing(Vector3 dir)
    {
        isAction = true;
        animation.resetAnim();
        animation.playAnimTriiger("IsHit");
        float time = CommonConfig.HitTime;
        while (true)
        {
            if(time <= 0)
            {
                break;
            }
            else
            {
                time -= Time.deltaTime;
            }
            this.rb.velocity = (dir + Vector3.up) * 10.0f * time;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        isAction = false;
        yield return null;
    }

    public void Stun(Vector3 dir)
    {

        animation.RotateAttack();
        if (AttackCorotine != null)
            StopCoroutine(AttackCorotine);
        AttackCorotine = StartCoroutine(Stuning(dir));
    }

    IEnumerator Stuning(Vector3 dir)
    {
        isAction = true;
        animation.resetAnim();
        animation.playAnimTriiger("IsHit");
        animation.playAnimBool("IsStun",true);
        vfxManager.instance.PlayVFX("Stun",this.transform.position,1.0f);
        float Maxtime = CommonConfig.StunTime;
        float time = CommonConfig.StunTime;
        while (true)
        {
            if (time <= 0)
            {
                break;
            }
            else
            {
                time -= Time.deltaTime;
            }

            if (time > Maxtime * 0.75f)
            {
                this.rb.velocity = (dir + Vector3.up) * 10.0f * time;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
        animation.playAnimBool("IsStun", false);
        isAction = false;
        yield return null;
    }
}
