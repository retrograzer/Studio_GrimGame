using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f; // change speed
    public float attackRange = 1f; // change attack range
    public float aggroRange = 100f;
    public float maxSpeed = 4f;
    public float avoidanceForceMultiplier = 10f;
    public float raySpacing = 1f;

    private LayerMask obstacleLayerMask;
    private Rigidbody2D rb;

    [Header("Attacking")]
    public int attackDamage = 1;
    public float attackRate = 0.5f;

    private Transform Player;
    private PlayerHealth ph;
    private float attackCooldown = 0f;
    private bool isAggroed = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        ph = Player.GetComponent<PlayerHealth>();
        obstacleLayerMask = LayerMask.GetMask("Obstacles");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        float dist = Vector3.Distance(transform.position, Player.position);

        // if player comes within 100 units, enemy faces player and moves towards player and call attack method
        if (dist < aggroRange || isAggroed) // change aggro range
        {
            isAggroed = true;
            Vector2 playerDirection = (Player.position - (Vector3)transform.position).normalized;

            RaycastHit2D[] hits = new RaycastHit2D[3];
            Vector2 rayStart = (Vector2)transform.position + playerDirection * rb.velocity.magnitude * Time.deltaTime;

            for (int i = 0; i < 3; i++)
            {
                Vector2 rayDirection = Quaternion.AngleAxis((i - 1) * 30f, Vector3.forward) * playerDirection;
                hits[i] = Physics2D.Raycast(rayStart, rayDirection, raySpacing, obstacleLayerMask);
                Debug.DrawRay(rayStart, rayDirection * raySpacing, Color.red);
            }

            Vector2 avoidanceForce = Vector2.zero;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    float distanceToObstacle = Vector2.Distance(transform.position, hit.collider.transform.position);
                    float distanceToRay = Vector2.Distance(rayStart, hit.point);
                    avoidanceForce += Vector2.Lerp(playerDirection, hit.normal, distanceToRay / distanceToObstacle) * avoidanceForceMultiplier;
                }
            }

            rb.AddForce(avoidanceForce);

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            rb.velocity += playerDirection * speed * Time.deltaTime;
        }

        if (dist < attackRange && attackCooldown <= 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        // attack animation trigger goes here

        // if player within attack range, decrease player health
        ph.TakeDamage(attackDamage, transform.position);

        // reset attack cooldown
        attackCooldown = attackRate;
    }
}