using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    Start,
    Select,
    CutScene,
    Playing
}

public class MenuController : MonoBehaviour {


    static public MenuController Instance;
    public MenuState menustate = MenuState.Start;
    [SerializeField] RectTransform StartMenu;
    [SerializeField] RectTransform SelectCanvas;
    [SerializeField] RectTransform CutSceneCanvas;

    [SerializeField] CharacterSelect[] chaSeclet;
    [SerializeField] Color[] imgColor;
    public Texture[] chaColor;

    Vector3 OffscreenPos = new Vector2(1280,0);

    private void Awake()
    {
     
        Instance = this;
    }
  


    // Use this for initialization
    void Start () {
        ShowStart();
	}

    void InitMenu()
    {

    }

    public Texture GetChaColor(int index)
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
	void Update () {
		if(menustate == MenuState.Start)
        {
            PressToStart();
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
        {
            ShowSelect();
            menustate = MenuState.Select;

        });

    }
}
