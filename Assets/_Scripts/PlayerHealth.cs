using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 3;
    public Transform respawnPoint;
    public float invincibleTime = 1f;
    public float hitForce = 2;

    int currentHealth = 3;
    int currentLives = 3;

    bool isInvincible = false;
    EM_Manager manager;
    PlayerManager pm;

    private void Awake()
    {
        pm = GetComponent<PlayerManager>();
    }

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EM_Manager>();
        currentHealth = maxHealth;
        currentLives = 3;
    }

    public void TakeDamage (int damageTaken, Vector2 dmgSrc)
    {
        StartCoroutine(InvincibleTimer());
        if (!isInvincible)
        {
            currentHealth -= damageTaken;
            pm.pui.healthText.text = "Health: " + currentHealth + "\nLives: " + currentLives;
            pm.pac.dmgSFX.Play();
            pm.FlashColor(Color.red, invincibleTime);

            //pm.rbd.AddForce(((Vector2)transform.position - dmgSrc) * hitForce, ForceMode2D.Impulse);

            if (currentHealth <= 0)
            {
                LoseLife(1);
            }
        }
    }

    public void AddLife (int livesAdded)
    {
        currentLives += livesAdded;
        pm.pui.healthText.text = "Health: " + currentHealth + "\nLives: " + currentLives;
    }

    public void LoseLife(int livesLost)
    {
        currentLives -= livesLost;
        currentHealth = maxHealth;
        pm.pui.healthText.text = "Health: " + currentHealth + "\nLives: " + currentLives;

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
        manager.DestroyAllEnemies();
        GetComponent<ComponentToggler>().ToggleComponents(false);
        pm.pui.gameOverCanvas.SetActive(true);
        Destroy(gameObject);
        Debug.Log("End Game");
    }
}
