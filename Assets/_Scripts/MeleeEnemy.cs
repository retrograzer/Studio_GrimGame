using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public float speed = 2.5f; // change speed
    public float attackRange = 2f; // change attack range
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // if player comes within 100 units, enemy faces player and moves towards player and call attack method
        if (Vector3.Distance(transform.position, player.position) < 100) // change aggro range
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        // attack animation trigger goes here

        // if player within attack range, decrease player health
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            // player damage mechanic would go here
        }
    }
}
