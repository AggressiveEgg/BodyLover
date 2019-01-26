using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxPlayer : MonoBehaviour {

    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] float MaxPos;

    Vector3 calpos;
    float lastpos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        calpos = Vector3.Lerp(pos1.position, pos2.position, 0.5f);
        float dif = Mathf.Abs(pos1.position.y - pos2.position.y);
        float pos = 0;
        if (dif > 5&& dif <MaxPos)
        {
            pos = dif / 2;
            lastpos = pos;
        }
        transform.position = new Vector3(calpos.x,calpos.y,calpos.z-lastpos) ;
	}
}
