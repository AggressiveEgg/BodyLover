using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MenuController : MonoBehaviour {

    public GameObject followobj;
    static public MenuController Instance;
    [SerializeField] bool isSkipSelect;
    [SerializeField] RectTransform StartMenu;
    [SerializeField] RectTransform SelectCanvas;
    [SerializeField] RectTransform CutSceneCanvas;
    [SerializeField] int countDown;
    public CharacterSelect[] chaSeclet;
    [SerializeField] Color[] imgColor;
    public Material[] chaColor;
    public GameObject[] Body;
    public GameObject[] Arm;
    public GameplayManager GM;
    public int totalPlayer;
    public int readyPlayer;
    public GameObject[] player;
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
        if (isSkipSelect)
        {
            GM.GameStart = true;
            GM.gameState = GameState.Playing;
            gameObject.SetActive(false);
        }
        chaSeclet[0].control.Item(() =>
        {
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
            for(int i = player.Length-1;i>=totalPlayer;i--)
            {
                player[i].SetActive(false);
            }

            GM.gameState = GameState.Start;
            GM.GameStart = true;

            SetPlayerMatt();
            StartPlaying(); InGameMenuController.Instance.InitMenu(InGameMenuController.Instance.finishPos.position.y);
            InGameMenuController.Instance.StartUpdateGuage();
            followobj.SetActive(true);


            
            
          
        }

    }
    public void CountDown()
    {
        StartCoroutine(Count());

    }
    public void SetPlayerMatt()
    {
        for(int i =0;i < totalPlayer;i++)
        {
            SetMatt(i, GetChaColor(chaSeclet[i].ColorIndex));
            player[i].SetActive(true);
            InGameMenuController.Instance.PlayerIcon[i].transform.parent.gameObject.SetActive(true);
            InGameMenuController.Instance.Progress[i+1].gameObject.SetActive(true);
            print("setMatt " + i);
        }
    }
    void SetMatt(int index,Material matt)
    {
        Arm[index].GetComponent<Renderer>().material = matt;
        Body[index].GetComponent<Renderer>().material = matt;
        
    }
}
