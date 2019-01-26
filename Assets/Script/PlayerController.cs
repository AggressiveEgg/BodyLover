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
    // Use this for initialization
    void Start()
    {
        control = new Controller();
        animation = new AnimationController(this);
        rb = GetComponent<Rigidbody>();
        control.SetPlayerJoyStick(playerindex);
        e = new Event();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active)
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
        List<PlayerController> listP = new List<PlayerController>();
        float time_anim = 1.0f;
        while (true)
        {
            Dir.x = Mathf.Round(Dir.x);
            Dir.y = 0;
            Dir.z = 0;
            float range = 2f;
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, Vector3.down, out hit, range))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    print("Found Player");
                    hit.collider.gameObject.GetComponent<PlayerController>().Force(Dir * -1);
                    int index = listP.FindIndex(x => x == hit.collider.gameObject.GetComponent<PlayerController>());
                    if (index == -1)
                    {
                        print("Attack : " + hit.collider.gameObject.name);
                        listP.Add(hit.collider.gameObject.GetComponent<PlayerController>());
                    }
                }
            }

            Debug.DrawRay(this.transform.position, Dir * range, Color.red);
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
        yield return null;
    }
    public void Force(Vector3 dir)
    {
        print("Force");
    }
}
