using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconScript : MonoBehaviour
{
    public GameObject icon;

    // Start is called before the first frame update
    void Start()
    {
        icon.transform.position = new Vector3(330, 240, 0);
    }

    public void button1Pos()
    {
        icon.transform.position = new Vector3(330, 240, 0);
    }

    public void button2Pos()
    {
        icon.transform.position = new Vector3(330, 160, 0);
    }

    public void button3Pos()
    {
        icon.transform.position = new Vector3(330, 90, 0);
    }
}
