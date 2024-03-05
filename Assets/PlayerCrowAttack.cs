using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrowAttack : MonoBehaviour
{
    public int crowsHeld = 0;
    public int maxCrowsHeld = 3;


    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(2) && crowsHeld > 0)
        {
            ThrowCrow();
        }
    }

    void ThrowCrow ()
    {
        crowsHeld--;
    }

    public void AddCrows (int crowsAdded)
    {
        crowsHeld += crowsAdded;

        Mathf.Clamp(crowsHeld, 0, maxCrowsHeld);
    }
}
