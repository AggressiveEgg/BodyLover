using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager 
{
    GameplayManager gameplay;
    public float currentTime;
    float MaxTime;
    bool timeUp = false;

    public TimeManager(GameplayManager gp)
    {
        gameplay = gp;
    }

    public void Update()
    {
        if (timeUp)
            return;
        
        if(currentTime < MaxTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            gameplay.gameState = GameState.GameOver;
            gameplay.GameStart = true;
            timeUp = true;
        }
    }

    public void init(float maxTime)
    {
        timeUp = false;
        currentTime = 0;
        MaxTime = maxTime;
    }

    public void reset()
    {
        init(MaxTime);
    }
}
