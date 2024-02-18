using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EM_Manager : MonoBehaviour
{



    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RestartEndlessMode ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMainMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
