using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconScript : MonoBehaviour
{
    public GameObject icon;
    public GameObject empty1;
    public GameObject empty2;
    public GameObject empty3;

    // Start is called before the first frame update
    void Start()
    {
        icon.transform.position = empty1.transform.position;
    }

    public void button1Pos()
    {
        icon.transform.position = empty1.transform.position;
    }

    public void button2Pos()
    {
        icon.transform.position = empty2.transform.position;
    }

    public void button3Pos()
    {
        icon.transform.position = empty3.transform.position;
    }
}
