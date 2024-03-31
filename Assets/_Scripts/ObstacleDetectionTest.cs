using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetectionTest : MonoBehaviour
{
    public float speed = 2.0f; // move speed
    public float attackRange = 5f; // attack range (change to test with both melee and ranged)
    public float avoidanceDistance = 2f; // distance to avoid obstacles (change as needed)
    public LayerMask obstacleLayer; // obstacle layer

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            Attack();
        }
        else
        {
            Vector2 direction = (player.position - transform.position).normalized;

            // check for obstacles
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, avoidanceDistance, obstacleLayer);
            if (hit.collider != null)
            {
                // calculate perpendicular direction and choose new direction
                Vector2 perpendicularDirection = Vector2.Perpendicular(hit.normal).normalized;
                direction = (direction + perpendicularDirection).normalized;
            }

            rb.velocity = direction * speed;
        }
    }

    void Attack()
    {
        // attack logic goes here
    }
}
