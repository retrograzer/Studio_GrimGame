using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsBehavior : MonoBehaviour
{
    Transform playerGO;

    public bool magnetized = false;
    public bool magnetizedByDistance = false;
    public float magnetSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, playerGO.transform.position) < 6f && !magnetized && magnetizedByDistance)
        {
            magnetized = true;
        }

        if (magnetized)
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), playerGO.transform.position, magnetSpeed * Time.deltaTime);
    }

    public void DelayMagnetization ()
    {

    }
}
