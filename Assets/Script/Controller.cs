using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

    string horizontal;
    string dpad;
    KeyCode kcJump;
    KeyCode kcLeft;
    KeyCode kcRight;
    KeyCode kcAtk;


    public bool isDpadPressed;
    public float Force;
    float muti = 30;

    bool isRPadDown;
    bool isLPadDown;
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
        jump = "w";
        left = "a";
        right = "d";
        atk = "Space";


        
      
        //KeyCode.Joystick1Button18;
    }
    
    public void SetPlayerJoyStick(int index)
    {
        joyAtk = "Joystick" + index + "Button2";
        joyJump = "Joystick" + index + "Button0";
        joyLeft = "Joystick" + index + "Button7";
        joyRight = "Joystick" + index + "Button8";
        kcAtk = (KeyCode)System.Enum.Parse(typeof(KeyCode), joyAtk);
        kcJump = (KeyCode)System.Enum.Parse(typeof(KeyCode), joyJump);
        kcLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), joyLeft);
        kcRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), joyRight);
        horizontal = "Horizontal" + index;
        dpad = "HDpad" + index;
    }


    public float Horizontal
    {
        get { return Input.GetAxis(horizontal); }
    }public float Dpad
    {
        get { return Input.GetAxis(dpad); }
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


    public void MoveController(System.Action action)
    {
        if (Input.GetAxis(dpad) > 0.3 || Input.GetAxis(dpad) < -0.3)
        {
            if (Mathf.Abs(Force) < 10)
                Force += (muti * Time.deltaTime) * Input.GetAxis(dpad);
            print("press DPAD");
        }
        else if (Input.GetAxis(horizontal) > 0.3 || Input.GetAxis(horizontal) < -0.3)
        {
            if (Mathf.Abs(Force) < 10)
                Force += (muti * Time.deltaTime) * Input.GetAxis(horizontal);
        }
        else if (Input.GetKey(left))
        {
            if (Force > -10)
                Force -= muti * Time.deltaTime;
        }
        else if (Input.GetKey(right))
        {
            if (Force < 10)
                Force += muti * Time.deltaTime;
        }
        else
        {
            if (Force > 0)
            {
                Force -= muti * Time.deltaTime;
                if (Force <= 0)
                    Force = 0;
            }
            else if (Force < 0)
            {
                Force += muti * Time.deltaTime;
                if (Force >= 0)
                    Force = 0;
            }
            else
            {
                Force = 0;
            }
        }
    }

    public void Left(System.Action action,System.Action action2)
    {
        if (Input.GetKey(left) || Input.GetAxis(dpad) <-0.1 || Input.GetAxis(horizontal) < -0.3)
        {
            if (action != null)
            { action();
                Debug.Log("Left Action");
            }
            isDpadPressed = true;
        }
        else
        {
            isDpadPressed = false;
            Debug.Log("No NoAction");
            if (action2 != null)
            { action2(); }
            return;
        }

       
    }
    public void Right(System.Action action,System.Action action2)
    {
        if (Input.GetKey(right) || Input.GetAxis(dpad) > 0.1 || Input.GetAxis(horizontal) > 0.3)
        {
            if (action != null)
            { action();

                isDpadPressed = true;
            }
        }
        else
        {
            isDpadPressed = false;
            Debug.Log("No NoAction");
            if (action2 != null)
            { action2(); }
                return;
        }
    }
    public void RightDown(System.Action action)

    {
        if (Input.GetKeyDown(right) || (Input.GetAxis(dpad) == 1))
        {
            if (action != null && !isRPadDown)
                action();
            isRPadDown = true;
        }
        else
        {
            isRPadDown = false;
        }
    }
    public void LeftDown(System.Action action)
    {
        if (Input.GetKeyDown(left) || (Input.GetAxis(dpad) == -1))
        {
            if (action != null&& !isLPadDown)
                action();
            isLPadDown = true;
        }
        else
        {
            isLPadDown = false;
        }
    }
    public void Item(System.Action action)
    {
        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(kcAtk)))
        {
            if (action != null)
            { 
                action(); 
            }
        }
        else
        {
            //Debug.Log("No NoAction");
            return;
        }
    }


}
