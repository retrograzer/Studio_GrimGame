using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningEnemy : MonoBehaviour
{
    public float speed = 2f; // change speed
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }  

    void Update()
    {
        // if player comes within 100 units, enemy faces player and moves towards player
        if (Vector3.Distance(transform.position, player.position) < 100) // change aggro range
        {
            Vector3 direction = transform.position - player.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }
}
