using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour {

    [SerializeField] int playerIndex;
    [SerializeField] int ColorIndex;

    [SerializeField] Image ImgUI;
    [SerializeField] Text textUI;
   public Controller control;

    [SerializeField] bool isActive;


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
        if (isActive)
        {
            if (MenuController.Instance.menustate == MenuState.Select)
            {
                ChoosingCha();
                print("Choosing");
            }
        }
        else
        {
            control.Item(() => { isActive = true; textUI.text = "P" + playerIndex; ImgUI.enabled = true; });
        }
    }

    void ChoosingCha()
    {
        control.LeftDown(()=> {
            if(ColorIndex > 0)
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
        ImgUI.color = MenuController.Instance.GetSoldColor(ColorIndex);

    }





}
