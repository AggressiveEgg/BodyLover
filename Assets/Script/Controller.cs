using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : MonoBehaviour
{


    string jump;
    string left;
    string right;
    string atk;

    string joyJump;
    string joyLeft;
    string joyRight;
    string joyAtk;


    KeyCode kcJump;
    KeyCode kcLeft;
    KeyCode kcRight;
    KeyCode kcAtk;
    //[SerializeField] PlayerIndex playerIndex;

    //GamePadState state;
    //GamePadState prevState;



    public void CheckGamepadState()
    {
        //prevState = state;
        //state = GamePad.GetState(playerIndex);

    }

    public Controller()
    {
        jump = "a";
        left = "f";
        right = "g";
        atk = "s";

      
        //KeyCode.Joystick1Button18;
    }
    
    public void SetPlayerJoyStick(int index)
    {
        joyAtk = "Joystick" + index + "Button18";
        joyJump = "Joystick" + index + "Button16";
        joyLeft = "Joystick" + index + "Button7";
        joyRight = "Joystick" + index + "Button8";
        kcAtk = (KeyCode)System.Enum.Parse(typeof(KeyCode), joyAtk);
        kcJump = (KeyCode)System.Enum.Parse(typeof(KeyCode), joyJump);
        kcLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), joyLeft);
        kcRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), joyRight);
    }


    public void Jump(System.Action action = null)
    {
        if (Input.GetKeyDown(jump) || (Input.GetKeyDown(kcJump)))
        {
            if (action != null)
            {
                action();
            }
            else
            {
                Debug.Log("No action");
                return;
            }
        }


    }

    public void Left(System.Action action)
    {
        if (Input.GetKey(left) || (Input.GetKey(kcLeft)) || Input.GetAxis("Horizontal") < 0)
        {
            if (action != null)
            { action(); }
        }
        else
        {
            Debug.Log("No NoAction");
            return;
        }
    }
    public void Right(System.Action action)
    {
        if (Input.GetKey(right) || Input.GetKey(kcRight) || Input.GetAxis("Horizontal") > 0)
        {
            if (action != null)
            { action(); }
        }
        else
        {
            Debug.Log("No NoAction");
            return;
        }
    }

    public void Item(System.Action action)
    {
        if (Input.GetKeyDown(atk) || (Input.GetKeyDown(kcAtk)))
        {
            if (action != null)
            { action(); }
        }
        else
        {
            Debug.Log("No NoAction");
            return;
        }
    }

}
