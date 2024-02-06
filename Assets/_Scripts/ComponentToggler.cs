using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentToggler : MonoBehaviour
{
    public MonoBehaviour[] toToggle;

    public void ToggleComponents (bool turnOn)
    {
        foreach (MonoBehaviour index in toToggle)
        {
            index.enabled = turnOn;
        }
    }
}
