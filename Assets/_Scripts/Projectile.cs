using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1; // change damage
    public bool friendlyFire = true;

    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
    }

    private void Start()
    {
        Destroy(gameObject, 5);
        Invoke("EnableCollision", 1f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // inster damage logic here
            collision.GetComponent<PlayerHealth>().TakeDamage(damage, transform.position);

            Destroy(gameObject);
        }

        if (collision.CompareTag("Enemy") && friendlyFire)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage, transform.position);

            Destroy(gameObject);
        }
    }

    void EnableCollision ()
    {
        coll.enabled = true;
    }
}
