using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private Button startGameButton, exitGameButton;

    public void StartGame()
    {
        AudioController.instance.playButtonClickSound();
        SceneFader.instance.LoadLevel("Gameplay");
    }


    public void ExitGame()
    {
        AudioController.instance.playButtonClickSound();
        Application.Quit();
    }

 
}//MainMenuController





