using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_Console : MonoBehaviour
{
    public GameObject commandDisplay;

    bool waitingForInput = false;
    GameObject playerGO;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Debug.Log("Pressed Tilde");
            commandDisplay.SetActive(!commandDisplay.activeSelf);
            waitingForInput = commandDisplay.activeSelf;
        }

        if (waitingForInput)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                playerGO.GetComponent<PlayerInteraction>().PickupSoul(10);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                playerGO.GetComponent<PlayerHealth>().AddLife(1);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                playerGO.GetComponent<PlayerHealth>().TakeDamage(-1);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                //Not working till crows implemented
                //playerGO.GetComponent<PlayerInteraction>().PickupSoul(10);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                GetComponent<FPS_Counter>().ChangeDisplayStatus();
            }
        }
    }
}
