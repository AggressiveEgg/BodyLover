using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MenuController : MonoBehaviour {


    static public MenuController Instance;
    [SerializeField] bool isSkipSelect;
    [SerializeField] RectTransform StartMenu;
    [SerializeField] RectTransform SelectCanvas;
    [SerializeField] RectTransform CutSceneCanvas;
    [SerializeField] int countDown;
    public CharacterSelect[] chaSeclet;
    [SerializeField] Color[] imgColor;
    public Material[] chaColor;
    public GameplayManager GM;
    public int totalPlayer;
    public int readyPlayer;
    Vector3 OffscreenPos = new Vector2(1280, 0);

    int count;
    private void Awake()
    {

        Instance = this;
    }



    // Use this for initialization
    void Start() {
        Reset();
    }

    void Reset()
    {
        ShowStart();
        count = countDown;
        GM.gameState = GameState.Start;
    }

    public Material GetChaColor(int index)
    {
        return chaColor[index];
    }
    public Color GetSoldColor(int index)
    {
        return imgColor[index];
    }
    public int GetTotalColor()
    {
        return imgColor.Length;
    }

    // Update is called once per frame
    void Update() {
        if (GM.gameState == GameState.Start)
        {
            PressToStart();
        }

    }
   public void StartGame()
    {
        if (readyPlayer >= totalPlayer)
        {
            if (totalPlayer > 1)
                CountDown();

        }

    }

    public void ShowStart()
    {
        StartMenu.localPosition = Vector2.zero;
        SelectCanvas.localPosition = OffscreenPos;
        CutSceneCanvas.localPosition = OffscreenPos;

    }
    public void ShowSelect()
    {
        StartMenu.localPosition = OffscreenPos;
        SelectCanvas.localPosition = Vector2.zero;
        CutSceneCanvas.localPosition = OffscreenPos;

    }
    public void ShowCutScene()
    {
        StartMenu.localPosition = OffscreenPos;
        SelectCanvas.localPosition = OffscreenPos;
        CutSceneCanvas.localPosition = Vector2.zero;
    }
    public void StartPlaying()
    {
        StartMenu.localPosition = OffscreenPos;
        SelectCanvas.localPosition = OffscreenPos;
        CutSceneCanvas.localPosition = OffscreenPos;
    }
    void PressToStart()
    {
        chaSeclet[0].control.Item(() =>
        {if(isSkipSelect)
            {
                GM.GameStart = true;
                GM.gameState = GameState.Playing;
                gameObject.SetActive(false);
            }
            ShowSelect();

            GM.gameState = GameState.Select;

        });

    }
    IEnumerator Count()
    {
        yield return new WaitForSeconds(1);
        count--;
        if(count >0)
            StartCoroutine(Count());
        else
        {
            StartPlaying(); 
            GM.gameState = GameState.Start;
            GM.GameStart = true;
            InGameMenuController.Instance.InitMenu(InGameMenuController.Instance.finishPos.position.y);
            InGameMenuController.Instance.StartUpdateGuage();
           
        }

    }
    public void CountDown()
    {
        StartCoroutine(Count());

    }
}
