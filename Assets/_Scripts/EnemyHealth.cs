using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public GameObject soulDropPrefab;
    public Vector2 deathExplosionForce = new Vector2(-2, 2);
    public bool overrideSoulsDropped = true;
    public int soulsDroppedOV = 5;
    public float hitForce = 50f;

    int currentHealth = 3;
    Rigidbody2D rbd;

    void Awake()
    {
        currentHealth = maxHealth;
        rbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage (int damageTaken, Vector2 dmgSrc)
    {
        currentHealth -= damageTaken;
        rbd.AddForce(((Vector2)transform.position - dmgSrc) * hitForce, ForceMode2D.Impulse);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die ()
    {
        int soulNum = maxHealth;

        if (overrideSoulsDropped)
            soulNum = soulsDroppedOV;

        for (int i = 0; i < soulsDroppedOV; i++)
        {
            GameObject newPickup = Instantiate(soulDropPrefab, transform.position, Quaternion.identity);
            newPickup.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 2), Random.Range(-2, 2)), ForceMode2D.Impulse);
        }

        Destroy(gameObject);
    }
}
