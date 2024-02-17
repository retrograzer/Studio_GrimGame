using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 3;
    public Transform respawnPoint;
    public float invincibleTime = 1f;

    int currentHealth = 3;
    int currentLives = 3;

    bool isInvincible = false;
    PlayerUI pui;

    private void Awake()
    {
        pui = GetComponent<PlayerUI>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentLives = 3;
    }

    public void TakeDamage (int damageTaken)
    {
        StartCoroutine(InvincibleTimer());
        currentHealth -= damageTaken;
        pui.healthText.text = "Health: " + currentHealth + "\nLives: " + currentLives;

        if (currentHealth <= 0)
        {
            LoseLife(1);
        }
    }

    public void AddLife (int livesAdded)
    {
        currentLives += livesAdded;
        pui.healthText.text = "Health: " + currentHealth + "\nLives: " + currentLives;
    }

    public void LoseLife(int livesLost)
    {
        currentLives -= livesLost;
        currentHealth = maxHealth;
        pui.healthText.text = "Health: " + currentHealth + "\nLives: " + currentLives;

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            transform.position = respawnPoint.position;
        }
    }

    IEnumerator InvincibleTimer ()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    //TODO
    void Die()
    {
        Debug.Log("End Game");
    }
}
