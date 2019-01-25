using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Controller : MonoBehaviour {

    [Header("Button")]
   [SerializeField] string jump;
    [SerializeField]string left;
   [SerializeField] string right;
    [SerializeField] string item;

    [SerializeField] PlayerIndex playerIndex;

    GamePadState state;
    GamePadState prevState;



    public void CheckGamepadState()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

    }

    public Controller()
    {
        jump = "a";
        left = "f";
        right = "g";
        item = "s";
    }

    public PlayerIndex PIndex
    {
        get { return playerIndex; }
        set { playerIndex = value; }
    }
	
    public void Jump(System.Action action = null)
    {
        if(Input.GetKeyDown(jump) || (state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released))
        {
            if(action != null)
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
        if(Input.GetKey(left)||state.DPad.Left == ButtonState.Pressed)
        {
            if(action != null)
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
        if (Input.GetKey(right) || state.DPad.Right == ButtonState.Pressed)
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
        if (Input.GetKeyDown(item)||( state.Buttons.X == ButtonState.Pressed && prevState.Buttons.X == ButtonState.Released))
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
