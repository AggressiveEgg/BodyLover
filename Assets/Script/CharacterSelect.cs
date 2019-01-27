using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour {

    [SerializeField] int playerIndex;
    public int ColorIndex;

    [SerializeField] Image ImgUI;
    [SerializeField] Text textUI;
   public Controller control;

    [SerializeField] bool isActive;



    public bool isReady;
    int totalColor;

    private void Start()
    {
        control = new Controller();
        control.SetPlayerJoyStick(playerIndex);
        InitCha();
    }
    public void InitCha()
    {
        ColorIndex = playerIndex-1;
        totalColor = MenuController.Instance.GetTotalColor()-1;
        ImgUI.enabled = false;
    }
    private void Update()
    {
        if (isActive && !isReady)
        {
            if (MenuController.Instance.GM.gameState == GameState.Select)
            {
                ChoosingCha();
                print("Choosing");
            }
        }
        else if (!isActive)
        {
            control.Item(() => { isActive = true; textUI.text = "Player " + playerIndex; ImgUI.enabled = true;
                
                MenuController.Instance.totalPlayer++; });
        }
    }
    

    void ChoosingCha()
    {
        if (!isReady)
        {
            control.LeftDown(() =>
            {
                if (ColorIndex > 0)
                {
                    ColorIndex--;
                }
                else
                {
                    ColorIndex = 0;
                }

            });
            control.RightDown(() =>
            {
                if (ColorIndex < totalColor)
                {
                    ColorIndex++;
                }
                else
                {
                    ColorIndex = totalColor;
                }

            });
        }
        control.Item(()=> {

            if (!isReady)
                MenuController.Instance.readyPlayer++;
            else
                MenuController.Instance.readyPlayer--;

            MenuController.Instance.StartGame();
                isReady = !isReady;
            textUI.text = "Player " + playerIndex + "\n READY";

        });
        ImgUI.sprite = InGameMenuController.Instance.pIcon[ColorIndex];

    }





}
