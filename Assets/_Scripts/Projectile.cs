using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1; // change damage

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // inster damage logic here
            collision.GetComponent<PlayerHealth>().TakeDamage(damage, transform.position);

            Destroy(gameObject);
        }
    }
}
