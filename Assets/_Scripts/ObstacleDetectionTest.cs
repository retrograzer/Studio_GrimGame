using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetectionTest : MonoBehaviour
{
    public Transform Player;
    public float speed = 1f;
    public float maxSpeed = 2f;
    public float avoidanceForceMultiplier = 5f;
    public float raySpacing = 0.5f;
    public LayerMask obstacleLayerMask;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Player == null)
        {
            Debug.LogError("Player reference is not set in ObstacleDetectionTest script.");
            return;
        }

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
}
