using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameMenuController : MonoBehaviour {

    static public InGameMenuController Instance;


    //Player GameObject
    public Image[] PlayerIcon;
    public Image[] PlayerPlace;
    //Setting
    public Sprite[] pIcon;
    public Sprite[] pPlace;

    public Slider[] Progress;

    public PlayerController[] PlayerPos;
    public Transform finishPos;
    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        PlayerPos = GameObject.FindObjectsOfType<PlayerController>();
        
	}
	public void InitMenu(float MaxValues)
    {
        for(int i = 0;i<MenuController.Instance.totalPlayer+1;i++)
        {
            Progress[i].maxValue = MaxValues;
            Progress[i].value = 0;
            if(i!=0)
            {
                PlayerPlace[i - 1].sprite = pPlace[MenuController.Instance.chaSeclet[i].ColorIndex];
                PlayerIcon[i - 1].sprite = pIcon[MenuController.Instance.chaSeclet[i].ColorIndex];
            }
            print(i +" "+ MenuController.Instance.totalPlayer);
        }
    }
    public void StartUpdateGuage()
    {
        InvokeRepeating("UpdateSlider", 1, 0.5f);
    }
    public void StopUpdateGuage()
    {
        CancelInvoke("UpdateSlider");
    }
    public void UpdateSlider()
    {
        for (int i = 0; i < MenuController.Instance.totalPlayer ; i++)
        {
         
            Progress[i+1].value = PlayerPos[i].gameObject.transform.position.y;

       
        }
        print("Update Guage");

    }

}
