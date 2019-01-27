using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    public static CameraFollow instance;
    public GameObject ObjectFollow;
    public GameObject Enemy;
    public Vector3 offset;
    public Vector3 offset_s;
    public float speed;

    public int mode;

	void Start ()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        mode = 0;
	}

	void Update ()
    {
        if (ObjectFollow == null)
            return;
        
        switch (mode)
        {
            case 0:
                this.transform.position = Vector3.Lerp(this.transform.position, ObjectFollow.transform.position + offset, speed * Time.deltaTime);
                break;
            case 1:
                if(Enemy != null)
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, Enemy.transform.position + offset, speed * Time.deltaTime);
                }
                break;
            default:
                break;
        }
	}

    public void ChangeMode(int m,GameObject target = null)
    {
        mode = m;

        if(target != null)
        {
            Enemy = target;
        }
    }
}
