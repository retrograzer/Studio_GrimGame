using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 3;
    public Transform respawnPoint;
    public float invincibleTime = 1f;
    public float hitForce = 2;
    public GameObject soulPickupPrefab;
    public EM_AudioManager worldAudio;

    int currentHealth = 3;
    int currentLives = 3;

    bool isInvincible = false;
    EM_Manager manager;
    PlayerManager pm;
    List<GameObject> prevDroppedSouls = new List<GameObject>();

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
        if (!isInvincible)
        {
            currentHealth -= damageTaken;
            //pm.pui.healthText.text = "Health: " + currentHealth + "\nLives: " + currentLives;
            pm.pac.dmgSFX.Play();
            pm.FlashColor(Color.red, invincibleTime);
            pm.pui.healthIcons[currentHealth].SetActive(false);

            //pm.rbd.AddForce(((Vector2)transform.position - dmgSrc) * hitForce, ForceMode2D.Impulse);

            if (currentHealth <= 0)
            {
                LoseLife(1);
            }
            StartCoroutine(InvincibleTimer());
        }
    }

    public void AddLife (int livesAdded)
    {
        currentLives += livesAdded;
        pm.pui.healthText.text = "x " + currentLives;
    }

    public void LoseLife(int livesLost)
    {
        currentLives -= livesLost;
        currentHealth = maxHealth;
        foreach (GameObject index in pm.pui.healthIcons)
            index.SetActive(true);

        pm.pui.healthText.text = "x " + currentLives;
        pm.pui.soulsHeldText.text = "" + 0;

        foreach (GameObject index in prevDroppedSouls)
            Destroy(index);

        prevDroppedSouls.Clear();
        for (int i = 0; i < pm.pi.soulsHeld; i++)
        {
            GameObject droppedSoul = Instantiate(soulPickupPrefab, transform.position, Quaternion.identity);
            prevDroppedSouls.Add(droppedSoul);
            droppedSoul.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)), ForceMode2D.Impulse);
        }
        pm.pi.soulsHeld = 0;

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
        pm.pui.finalSoulText.text = "Souls Collected: " + PlayerPrefs.GetInt("CurrentSoulNum") + "\nBest Souls Collected: " + PlayerPrefs.GetInt("BestSoulNum");
        PlayerPrefs.SetInt("CurrentSoulNum", 0);
        worldAudio.FadeToMM(2f);
        Destroy(gameObject);
        Debug.Log("End Game");
    }
}
