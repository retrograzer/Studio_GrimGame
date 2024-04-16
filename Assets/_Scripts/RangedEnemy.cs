using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2.0f; // change movespeed
    public float attackRange = 50f; // change attack range
    public float aggroRange = 10f;

    [Header("Projectile")]
    public float attackRate = .5f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 5f; // change projectile speed

    private Transform player;
    private float attackCooldown = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        if (Vector3.Distance(transform.position, player.position) < aggroRange) // aggro range here - adjust as needed
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, player.position) < attackRange && attackCooldown <= 0)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        // spawn projectile and set velocity/direction
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector3 direction = (player.position - firePoint.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        projectile.GetComponent<Projectile>().SetDirection(player.position);
        // attack animation trigger goes here

        // reset attack cooldown
        attackCooldown = attackRate;
    }
}
