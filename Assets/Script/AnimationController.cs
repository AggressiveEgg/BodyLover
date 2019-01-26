using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController 
{
    PlayerController playerController;
    Animator animator;
    GameObject modelCharacter;

    public int right;
    public AnimationController(PlayerController pc)
    {
        playerController = pc;
        modelCharacter = playerController.transform.GetChild(0).gameObject;
        animator = modelCharacter.GetComponent<Animator>();
    }

    public void playAnimBool(string name,bool on)
    {
        animator.SetBool(name,on);
    }

    public void playAnimTriiger(string name)
    {
        animator.SetTrigger(name);
    }

    public void RotateDirection(Vector3 dir)
    {
        if (modelCharacter == null)
            return;
        
        if(dir.x > 0)
        {
            right = 1;
            modelCharacter.transform.eulerAngles = Vector3.Lerp(modelCharacter.transform.eulerAngles,new Vector3(modelCharacter.transform.eulerAngles.x,135,0), 5 * Time.deltaTime);
        }
        else if(dir.x < 0)
        {
            right = -1;
            modelCharacter.transform.eulerAngles = Vector3.Lerp(modelCharacter.transform.eulerAngles, new Vector3(modelCharacter.transform.eulerAngles.x,235, 0), 5 * Time.deltaTime);
        }
    }

    public void RotateAttack()
    {
        if(right == 1)
        {
            modelCharacter.transform.eulerAngles = Vector3.Lerp(modelCharacter.transform.eulerAngles, new Vector3(modelCharacter.transform.eulerAngles.x, 135, 0), 100 * Time.deltaTime);
        }
        else
        {
            modelCharacter.transform.eulerAngles = Vector3.Lerp(modelCharacter.transform.eulerAngles, new Vector3(modelCharacter.transform.eulerAngles.x, 235, 0), 100 * Time.deltaTime);
        }
    }
}
