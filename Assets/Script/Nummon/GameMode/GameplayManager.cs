﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Start,
    Select,
    CutScene,
    Playing,
    GoJi,
    GojiFight,
    GameOver,
    End
}

public class GameplayManager : MonoBehaviour 
{
    public static GameplayManager instance;
    public TimeManager timeManager;
    public BoxManager boxManager;
    public UIManager UIManager;

    public GameObject[] ListPlayer;
    public GameObject[] PlayerPoints;
    public GameObject[] GoJiPoints;
    public bool GameStart = true;
    public GameState gameState;

	void Start ()
	{
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        init();
        FindPlayer();
        gameState = GameState.Start;
        GameStart = false;
	}

    void FindPlayer()
    {
        ListPlayer = GameObject.FindGameObjectsWithTag("Player");
        PlayerPoints = GameObject.FindGameObjectsWithTag("PlayerPoint");
        GoJiPoints = GameObject.FindGameObjectsWithTag("GoJiPoints");
    }

    public void init()
    {
        timeManager = new TimeManager(this);
        boxManager = new BoxManager(this);
        timeManager.init(CommonConfig.Time);
    }
	
	void Update ()
    {
        if (!GameStart)
            return;
        OnState();
	}

    void resetGame()
    {
        boxManager.resetAllBox();
        timeManager.reset();
        MovePlayerToPoint();
    }

    void MoveToGoji()
    {
        GojiMovement goji = GameObject.FindObjectOfType<GojiMovement>();
        if(goji != null)
            goji.init();
        boxManager.resetAllBox();
        timeManager.reset();
        MovePlayerToGojiPoint();

        gameState = GameState.GojiFight;
        GameStart = true;
    }

    void MovePlayerToPoint()
    {
        int amount = ListPlayer.Length;
        for (int i = 0; i < amount; i++)
        {
            ListPlayer[i].transform.position = PlayerPoints[i].transform.position;
            ListPlayer[i].GetComponent<PlayerController>().Reset();
        }
    }

    void MovePlayerToGojiPoint()
    {
        int amount = GoJiPoints.Length;
        for (int i = 0; i < amount; i++)
        {
            ListPlayer[i].transform.position = GoJiPoints[i].transform.position;
            ListPlayer[i].GetComponent<PlayerController>().Reset();
        }
    }

    void EventEndGame()
    {
        int amount = ListPlayer.Length;
        for (int i = 0; i < amount; i++)
        {
            ListPlayer[i].GetComponent<PlayerController>().Reset();
            ListPlayer[i].GetComponent<PlayerController>().Active = false;
        }
    }

    void OnState()
    {
        switch (gameState)
        {
            case GameState.Start:
                OnStartGame();
                InGameMenuController.Instance.InitMenu(InGameMenuController.Instance.finishPos.position.y);
                break;
            case GameState.Playing:
                timeManager.Update();
                if(InGameMenuController.Instance.currentValue >= InGameMenuController.Instance.FinalValue)
                {
                    gameState = GameState.GoJi;
                }
                break;
            case GameState.End:
                OnEndGame();
                break;
            case GameState.GameOver:
                OnGameOver();
                break;
            case GameState.GoJi:
                MoveToGoji();
                InGameMenuController.Instance.InitBoss(GameObject.FindObjectOfType<GojiMovement>());
                break;
            default:
                break;
        }
    }

    void OnStartGame()
    {
        StartCoroutine(IStartGame());
        GameStart = false;
    }

    IEnumerator IStartGame()
    {
        EventEndGame();
        yield return new WaitForSeconds(0.1f);
        resetGame();
        gameState = GameState.Playing;
        GameStart = true;
    }

    void OnEndGame()
    {
        StartCoroutine(IEndGame());
        GameStart = false;
    }

    IEnumerator IEndGame()
    {
        EventEndGame();
        UIManager.UIEnding(true);
        yield return new WaitForSeconds(3.0f);
        UIManager.UIEnding(false);
        yield return new WaitForSeconds(1.0f);
        Application.LoadLevel(1);
    }

    void OnGameOver()
    {
        StartCoroutine(IGameOver());
        GameStart = false;
    }

    IEnumerator IGameOver()
    {
        EventEndGame();
        UIManager.UIEnding(true);
        yield return new WaitForSeconds(3.0f);
        UIManager.UIEnding(false);
        yield return new WaitForSeconds(1.0f);
        gameState = GameState.Start;
        GameStart = true;
    }
}
