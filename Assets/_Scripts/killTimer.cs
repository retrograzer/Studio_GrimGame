using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killTimer : MonoBehaviour
{

    public float ttk = 1f;

    private void Start()
    {
        Destroy(gameObject, ttk);
    }
}
