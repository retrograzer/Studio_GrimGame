using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float speed = 2.0f; // change movespeed
    public float attackRange = 50f; // change attack range
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 5f; // change projectile speed

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 200) // aggro range here - adjust as needed
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
        // spawn projectile and set velocity/direction
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector3 direction = (player.position - firePoint.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        // attack animation trigger goes here
    }
}
