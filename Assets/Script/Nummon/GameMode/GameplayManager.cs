using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Start,
    Playing,
    End
}

public class GameplayManager : MonoBehaviour 
{
    public TimeManager timeManager;
    public BoxManager boxManager;

    public GameObject[] ListPlayer;
    public GameObject[] PlayerPoints;
    public bool GameStart = true;
    public GameState gameState;

	void Start ()
	{
        init();
        FindPlayer();
        gameState = GameState.Start;
	}

    void FindPlayer()
    {
        ListPlayer = GameObject.FindGameObjectsWithTag("Player");
        PlayerPoints = GameObject.FindGameObjectsWithTag("PlayerPoint");
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

    void MovePlayerToPoint()
    {
        int amount = ListPlayer.Length;
        for (int i = 0; i < amount; i++)
        {
            ListPlayer[i].transform.position = PlayerPoints[i].transform.position;
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
                break;
            case GameState.Playing:
                timeManager.Update();
                break;
            case GameState.End:
                OnEndGame();
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
        yield return new WaitForSeconds(3.0f);
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
        yield return new WaitForSeconds(3.0f);
        gameState = GameState.Start;
        GameStart = true;
    }
}
