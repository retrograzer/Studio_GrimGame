using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject settingsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
        //switch to game Scene
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void OpenMain()
    {
        mainCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
    }

    public void QuitOut()
    {
        Application.Quit();
    }
}
