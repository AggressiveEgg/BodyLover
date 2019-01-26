using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController 
{
    PlayerController playerController;
    Animator animator;

    GameObject modelCharacter;
    public AnimationController(PlayerController pc)
    {
        playerController = pc;
        modelCharacter = playerController.transform.GetChild(0).gameObject;
        animator = modelCharacter.GetComponent<Animator>();
    }

    public void playAnim(string name)
    {
        
    }

    public void RotateDirection(Vector3 dir)
    {
        if (modelCharacter == null)
            return;
        
        if(dir.x > 0)
        {
            modelCharacter.transform.eulerAngles = Vector3.Lerp(modelCharacter.transform.eulerAngles,new Vector3(0,0,0),1);
        }
        else if(dir.x < 0)
        {
            modelCharacter.transform.eulerAngles = Vector3.Lerp(modelCharacter.transform.eulerAngles, new Vector3(0, 0, 0), 1);
        }
    }
}
