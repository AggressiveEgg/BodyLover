using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {


    float maxSpeed = 30;
    float itemEffect = 1;

    int hp;
    int maxhp;

    

    public float MaxSpeed
    {
        get { return MaxSpeed * itemEffect; }
    }
    
    public void SetItemEffect(float effect)
    {
        itemEffect = effect;


    }


}
