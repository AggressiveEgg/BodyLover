using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ItemType
{
    ConsumeAble,
    ThrowAble
}
[System.Serializable]
public class Item : MonoBehaviour {



    [SerializeField] float SpeedEffect;
    
    [SerializeField] float dulation;
    [SerializeField] ItemType itemType;

    public float Dulation
    {
        get { return dulation; }
    }
    public float ItemEffrct()
    {
        if(itemType == 0)
        {
            return SpeedEffect;
        }
        else
        {

        }
        return 0;
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag =="Player")
        {
            
        }
    }



}
