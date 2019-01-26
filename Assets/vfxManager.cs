using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxManager : MonoBehaviour {

    public static vfxManager instance;
	void Start () 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void PlayVFX(string name,Vector3 pos,float time)
    {
        
    }
}
