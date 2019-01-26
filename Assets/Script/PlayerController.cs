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
        control.Jump(() => {
            if (!isJump)
            {
                isJump = true;
                
                // gameObject.transform.LookAt(direction.transform);
                rb.velocity = new Vector3(rb.velocity.x, jumpForce);
            }
        });


    }
    public void MoveTarget()
    {
        if (Mathf.Abs(rb.velocity.x) < maxSpeed && !isJump)
        {
            //float force = Mathf.Abs(control.Dpad+)
            rb.velocity = new Vector3(speed * control.Force, rb.velocity.y);
            //control.Left(() =>
            //{if(speed < maxSpeed)
            //    speed += 2;
            //    rb.velocity = new Vector3(speed*, rb.velocity.y);
                
            //},()=> {
            //    speed = 0;
            //});
            //control.Right(() =>
            //{
            //    if (speed < maxSpeed)
            //        speed += 2;
            //    rb.velocity = new Vector3(speed , rb.velocity.y);
            //},()=> {
            //    speed = 0;
            //});
            
        }
        else if(Mathf.Abs(rb.velocity.x) < maxSpeed&&isJump)
        {
            if(CanMove(rb.velocity.normalized))
                rb.velocity = new Vector3(rb.velocity.x+(speed * (control.Force/2))/2, rb.velocity.y);
        }
    }

    bool CanMove(Vector3 Dir)
    {
        Dir.x = Mathf.Round(Dir.x);
        Dir.y = 0;
        Dir.z = 0;
        print(Dir);

        RaycastHit hit;
        float range = 2.5f;
        print("target point : " + this.transform.position + (Dir * range) + "results : " + (this.transform.position + (Dir * range)));
        if (Physics.Raycast(transform.position, Dir, out hit, range))
        {
            Debug.DrawRay(transform.position, Dir * range, Color.green,2.0f);
            return false;
        }
        else
        {
            Debug.DrawRay(transform.position, Dir * range, Color.red,2.0f);
            return true;
        }

    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "ground" )
        {
            isJump = true;
        }
    }

    public void OnCollisionStay(Collision col)
    {
        //print("Bound : " + col.gameObject.GetComponent<Collider>().bounds.size);
        //print("jump : " + (this.gameObject.transform.localPosition.y > col.gameObject.transform.position.y + this.gameObject.GetComponent<Collider>().bounds.size.y / 2));
        if ((col.gameObject.tag == "ground" || col.gameObject.tag == "Player")
            && (this.gameObject.transform.localPosition.y > col.gameObject.transform.position.y + this.gameObject.GetComponent<Collider>().bounds.size.y / 2))
        {
            HitFunction();
        }
    }

    public void OnCollisionEnter(Collision col)
    {

        if(col.gameObject.tag == "ground")
        {
            control.Force = 0;
        }
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
