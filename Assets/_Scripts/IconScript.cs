using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconScript : MonoBehaviour
{
    public GameObject icon;
    public Transform[] buttonPositions;

    // Start is called before the first frame update
    void Start()
    {
        //icon.transform.position = new Vector3(330, 240, 0);
        icon.transform.position = buttonPositions[0].position;
    }

    public void button1Pos()
    {
        //icon.transform.position = new Vector3(330, 240, 0);
        icon.transform.position = buttonPositions[0].position;
    }

    public void button2Pos()
    {
        //icon.transform.position = new Vector3(330, 160, 0);
        icon.transform.position = buttonPositions[1].position;
    }

    public void button3Pos()
    {
        //icon.transform.position = new Vector3(330, 90, 0);
        icon.transform.position = buttonPositions[2].position;
    }
}
