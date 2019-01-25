using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour 
{
    public static LoadLevel instance;
	void Start () 
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
	}

    public void OnLoadLevel(int index)
    {
        Application.LoadLevel(index);
    }
}
