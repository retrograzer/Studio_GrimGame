using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public int soulsHeld = 0;
    public ParticleSystem fx;
    public GameObject soulPrefab;

    PlayerManager pm;

    private void Awake()
    {
        pm = GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SoulPickup")
        {
            Destroy(collision.gameObject);
            PickupSoul(1);
        }
    }

    public void DropAllSouls ()
    {
        for (int i = 0; i < soulsHeld; i++)
        {
            //TODO
            GameObject newDrop = Instantiate(soulPrefab, transform.position, Quaternion.identity);
        }
    }

    public void PickupSoul (int soulsGained)
    {
        soulsHeld += soulsGained;
        pm.pui.soulsHeldText.text = "Souls: " + soulsHeld;
        fx.Play();
        pm.pac.pickupSFX.clip = pm.pac.rngPickupClip[Random.Range(0, pm.pac.rngPickupClip.Length)];
        pm.pac.pickupSFX.Play();
    }

    public void DepositSoulsFX (int soulsRemoved)
    {
        soulsHeld -= soulsRemoved;
    }

    public void ApplySlowEffect ()
    {

    }

    public void ApplyCrippleEffect ()
    {

    }

    public void ApplyPoisonEffect ()
    {

    }

    public void ApplyWaterEffect ()
    {

    }

    public void ApplyFireEffect ()
    {
        pm.ph.TakeDamage(1, transform.position);
    }
}
