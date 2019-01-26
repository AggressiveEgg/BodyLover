using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIManager : MonoBehaviour 
{
    public GameplayManager gameManager;
    public TimeUI timeUI;
    public DebugText DebugText;

    public void Start()
    {
        DebugText.init(this);
    }

    public void Update () 
    {
        if (gameManager == null)
            return;
        
        if(timeUI.timeText != null)
            timeUI.timeText.text = "time : " + gameManager.timeManager.currentTime;

        if(DebugText != null)
        {
            DebugText.update();
        }
	}
}

[System.Serializable]
public class TimeUI
{
    public Text timeText;
}

[System.Serializable]
public class DebugText
{
    UIManager UIManager;
    public Text Text;

    public void init(UIManager uim)
    {
        UIManager = uim;
    }

    public void update()
    {
        if (Text == null)
            return;

        Text.text = "Game State : " + UIManager.gameManager.gameState.ToString();
    }
}

