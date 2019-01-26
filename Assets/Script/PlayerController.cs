using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{


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
            rb.velocity = new Vector3(rb.velocity.x+(speed * (control.Force/2))/2, rb.velocity.y);
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
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "Player")
        {
            HitFunction();
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            control.Force = 0;
        }
    }
    public void HitFunction()
    {
        isJump = false;
        
    }
}
