using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager 
{
    GameplayManager GameplayManager;
    public Box[] Boxes;

    public BoxManager (GameplayManager gp)
    {
        GameplayManager = gp;
        FindAllBoxes();
	}

    void FindAllBoxes()
    {
        Boxes = GameObject.FindObjectsOfType<Box>();
    }
	
    public void resetAllBox()
    {
        foreach (Box item in Boxes)
        {
            item.Reset();
        }
    }
}
