﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat  {


    float maxSpeed = 30;
    float itemEffect = 1;

    int hp;
    int maxhp;

    Item inventory;

    public bool isStun = false;
    public PlayerStat()
    {

    }

    public float MaxSpeed
    {
        get { return maxSpeed * itemEffect; }
    }
    
    public void SetItemEffect(float effect)
    {
        itemEffect = effect;
    }


    


}
