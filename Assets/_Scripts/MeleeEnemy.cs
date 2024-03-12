using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{ 
    [Header("Movement")]
    public float speed = 2.5f; // change speed
    public float attackRange = 2f; // change attack range
    public float aggroRange = 100f;

    [Header("Attacking")]
    public int attackDamage = 1;
    public float attackRate = 0.5f;

    private Transform player;
    private PlayerHealth ph;
    private float attackCooldown = 0f;
    private bool isAggroed = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ph = player.GetComponent<PlayerHealth>();
    }

    void FixedUpdate()
    {
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        float dist = Vector3.Distance(transform.position, player.position);

        // if player comes within 100 units, enemy faces player and moves towards player and call attack method
        if (dist < aggroRange || isAggroed) // change aggro range
        {
            isAggroed = true;
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if (dist < attackRange && attackCooldown <= 0)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        // attack animation trigger goes here

        // if player within attack range, decrease player health
        ph.TakeDamage(attackDamage);

        // reset attack cooldown
        attackCooldown = attackRate;
    }
}
