using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VfxInfo
{
    public string name;
    public GameObject VFXObject;
}

public class vfxManager : MonoBehaviour {

    public static vfxManager instance;
    public List<VfxInfo> vfxInfos;

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
	
    public void PlayVFX(string name,Vector3 pos,float time)
    {
        int index = vfxInfos.FindIndex(x => x.name == name );
        if(index != -1)
        {
            GameObject newVFX = Instantiate(vfxInfos[index].VFXObject) as GameObject;
            newVFX.transform.position = pos;
            Destroy(newVFX,time);
        }

    }
}
