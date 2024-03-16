using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public int soulsHeld = 0;
    public ParticleSystem fx;
    public GameObject soulPrefab;

    PlayerHealth ph;
    PlayerAttack pa;
    PlayerMovement pm;
    PlayerUI pui;

    private void Awake()
    {
        ph = GetComponent<PlayerHealth>();
        pa = GetComponent<PlayerAttack>();
        pm = GetComponent<PlayerMovement>();
        pui = GetComponent<PlayerUI>();
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
        }
    }

    public void PickupSoul (int soulsGained)
    {
        soulsHeld += soulsGained;
        pui.soulsHeldText.text = "Souls: " + soulsHeld;
        fx.Play();
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
        ph.TakeDamage(1, transform.position);
    }
}
