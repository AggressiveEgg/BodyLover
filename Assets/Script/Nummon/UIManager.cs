using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIManager : MonoBehaviour 
{
    public GameplayManager gameManager;
    public TimeUI timeUI;

    public void Start()
    {
        
    }

    public void Update () 
    {
        if (gameManager == null)
            return;
        
        if(timeUI.timeText != null)
            timeUI.timeText.text = "time : " + gameManager.timeManager.currentTime;
	}
}

[System.Serializable]
public class TimeUI
{
    public Text timeText;
}

