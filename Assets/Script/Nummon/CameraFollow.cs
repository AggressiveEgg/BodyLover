using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    public GameObject ObjectFollow;
    public Vector3 offset;
    public float speed;

	void Start ()
    {
		
	}

	void Update ()
    {
        if (ObjectFollow == null)
            return;

        this.transform.position = Vector3.Lerp(this.transform.position,ObjectFollow.transform.position + offset,speed * Time.deltaTime);
	}
}
