using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public bool Active = false;

    [SerializeField]
    Controller control;
    [Header("Setting")]
    [SerializeField] float maxSpeed;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] int playerindex;


    [SerializeField] GameObject player;

    Event e;
    [SerializeField]bool isJump;
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        control = new Controller();
        rb = GetComponent<Rigidbody>();
        control.SetPlayerJoyStick(playerindex);
       
         e = new Event();
        
            
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active)
            return;
        
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
            print("WalkOnGround");
            float moveX = Mathf.Clamp(speed * control.Force, -maxSpeed, maxSpeed);
            //print(moveX);
            rb.velocity = new Vector3(moveX, rb.velocity.y);   
        }
        else if(isJump)
        {
            if (CanMove(rb.velocity.normalized))
            {
                print("WalkOnAir");
                float moveX = Mathf.Clamp(rb.velocity.x + (speed * (control.Force / 2)) / 2, -1 * (maxSpeed / 2), maxSpeed/2);
                //print(moveX);
                rb.velocity = new Vector3(moveX , rb.velocity.y);
            }
        }
    }

    bool CanMove(Vector3 Dir)
    {
        Dir.x = Mathf.Round(Dir.x);
        Dir.y = 0;
        Dir.z = 0;

        RaycastHit hit;
        float range = 2.5f;
        if (Physics.Raycast(transform.position, Dir, out hit, range))
        {
            Debug.DrawRay(transform.position, Dir * range, Color.red,2.0f);
            return false;
        }
        else
        {
            Debug.DrawRay(transform.position, Dir * range, Color.green,2.0f);
            return true;
        }

    }

    private void OnCollisionExit(Collision col)
    {
        /*if (col.gameObject.tag == "ground" )
        {
            isJump = true;
        }*/
    }

    public void OnCollisionStay(Collision col)
    {
        //print("Bound : " + col.gameObject.GetComponent<Collider>().bounds.size);
        //print("jump : " + (this.gameObject.transform.localPosition.y > col.gameObject.transform.position.y + this.gameObject.GetComponent<Collider>().bounds.size.y / 2));

    }

    public void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.tag == "ground" /*|| col.gameObject.tag == "Player"*/)
            && (this.gameObject.transform.localPosition.y >= col.gameObject.transform.localPosition.y)
            && (this.gameObject.transform.localPosition.x > col.gameObject.transform.localPosition.x - col.gameObject.GetComponent<Collider>().bounds.size.x / 2)
            && (this.gameObject.transform.localPosition.x < col.gameObject.transform.localPosition.x + col.gameObject.GetComponent<Collider>().bounds.size.x / 2))
        {
            Debug.DrawRay(col.transform.localPosition - new Vector3(col.gameObject.GetComponent<Collider>().bounds.size.x / 2,0,0), Vector3.up * col.gameObject.GetComponent<Collider>().bounds.size.x / 2, Color.green, 2.0f);
            Debug.DrawRay(col.transform.localPosition + new Vector3(col.gameObject.GetComponent<Collider>().bounds.size.x / 2,0,0), Vector3.up * col.gameObject.GetComponent<Collider>().bounds.size.x / 2, Color.green, 2.0f);
            Debug.DrawRay(col.transform.localPosition, Vector3.up * col.gameObject.GetComponent<Collider>().bounds.size.x / 2, Color.red, 2.0f);
            print("Hit");
            HitFunction();
        }

        /*
        if(col.gameObject.tag == "ground")
        {
            print("ground");
            control.Force = 0;
        }*/
    }

    public void HitFunction()
    {
        isJump = false;
    }

    public void Reset()
    {
        Active = true;
        HitFunction();
        control.Force = 0;
        rb.velocity = new Vector3(0,0,0);
    }
}
