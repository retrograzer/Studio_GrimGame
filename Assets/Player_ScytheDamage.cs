using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ScytheDamage : MonoBehaviour
{
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyManager em = collision.GetComponent<EnemyManager>();
            em.eh.TakeDamage(damage, transform.position);
        }
    }
}