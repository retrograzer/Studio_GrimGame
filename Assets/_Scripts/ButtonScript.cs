using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject settingsCanvas;

    static float musicVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        AudioListener.volume = musicVolume;
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

    public void UpdateVolume(float newVol)
    {
        musicVolume = newVol;
    }

    public void ScreenModeChange()
    {
        string index = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        switch (index)
        {
            case "0":
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;

            case "1":
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;

            case "2":
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }
    }

    public void ScreenResChange()
    {
        string index = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        switch (index)
        {
            case "0":
                Screen.SetResolution(1920, 1080, true);
                break;

            case "1":
                Screen.SetResolution(1280, 720, true);
                break;

            case "2":
                Screen.SetResolution(800, 600, true);
                break;
        }
    }
}
