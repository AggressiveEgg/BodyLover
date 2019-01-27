using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {

    public Controller control;

    void Start()
    {
        control = new Controller();
        control.SetPlayerJoyStick(1);
    }

    void Update()
    {
        control.Jump(()=> {

            SceneManager.LoadScene("GamePlay");


        });
    }
}
