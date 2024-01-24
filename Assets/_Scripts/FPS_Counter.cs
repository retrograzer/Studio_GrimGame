using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPS_Counter : MonoBehaviour
{
    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;

    public TextMeshProUGUI counter;
    public bool displayFPS = false;
    private void Start()
    {
        displayFPS = false;
    }

    void Update()
    {

        if (displayFPS)
        {
            if (m_timeCounter < m_refreshTime)
            {
                m_timeCounter += Time.deltaTime;
                m_frameCounter++;
            }
            else
            {
                //This code will break if you set your m_refreshTime to 0, which makes no sense.
                m_lastFramerate = (float)m_frameCounter / m_timeCounter;
                m_frameCounter = 0;
                m_timeCounter = 0.0f;
            }
            counter.text = "FPS: " + Mathf.RoundToInt(m_lastFramerate);
        }
        else
        {
            counter.text = "";
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            displayFPS = !displayFPS;
        }
    }

}
