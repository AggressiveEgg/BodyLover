using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{



    Controller control;
    [Header("Setting")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] int playerindex;


    [SerializeField] GameObject player;


    bool isJump;
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        control = new Controller();
        rb = GetComponent<Rigidbody>();
        control.SetPlayerJoyStick(playerindex);
        Debug.Log(control);
    }

    // Update is called once per frame
    void Update()
    {
        control.CheckGamepadState();
        ForceToDirection();
        RotateTarget();

    }


    public void ForceToDirection()
    {
        control.Jump(() => {
            if (!isJump)
            {
                isJump = true;
                rb.useGravity = true;
                // gameObject.transform.LookAt(direction.transform);
                rb.AddForce(0, jumpForce, 0);
            }
        });


    }
    public void RotateTarget()
    {
        if (Mathf.Abs(rb.velocity.x) < 100)
        {
            control.Left(() =>
            {
                rb.AddForce(-speed, 0, 0);
            });
            control.Right(() =>
            {
                rb.AddForce(speed, 0, 0);
            });
            Debug.Log("Velocity " + rb.velocity.x);
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "Player")
        {
            HitFunction();
        }
    }
    public void HitFunction()
    {
        isJump = false;
    }
}
