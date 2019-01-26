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
        control.MoveController(null);
        ForceToDirection();
        MoveTarget();
    }


    public void ForceToDirection()
    {
        control.Jump(() => 
        {
            if (!isJump)
            {
                isJump = true;
                rb.velocity = new Vector3(rb.velocity.x, jumpForce);
            }
        });


    }
    public void MoveTarget()
    {
        if (!isJump)
        {
            float moveX = Mathf.Clamp(speed * control.Force, -maxSpeed, maxSpeed);
            //print(moveX);
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
}
