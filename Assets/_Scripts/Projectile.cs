using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    public int damage = 1; // change damage
    public bool friendlyFire = true;
    public bool isCrow = false;
    public GameObject onHitFX;

    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        if (!isCrow)
            coll.enabled = false;
    }

    private void Start()
    {
        Destroy(gameObject, 5);
        if (!isCrow)
            Invoke("EnableCollision", .5f);
    }

    public void SetDirection (Vector3 target)
    {
        var dir = target - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCrow)
        {
            if (collision.CompareTag("Player"))
            {
                // inster damage logic here
                collision.GetComponent<PlayerHealth>().TakeDamage(damage, transform.position);
                GameObject fx = Instantiate(onHitFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            if (collision.CompareTag("Enemy") && friendlyFire)
            {
                collision.GetComponent<EnemyHealth>().TakeDamage(damage, transform.position);
                GameObject fx = Instantiate(onHitFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.CompareTag("Enemy"))
            {
                //Debug.Log("HE");

                EnemyManager em = collision.GetComponent<EnemyManager>();
                em.StunEnemy(3f);
                em.FlashColor(Color.yellow, 3f);

                Instantiate(onHitFX, transform.position, Quaternion.identity);

                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Collider2D>().enabled = false;

                Destroy(gameObject, 2);
            }
        }
        
    }

    void EnableCollision ()
    {
        coll.enabled = true;
    }
}
