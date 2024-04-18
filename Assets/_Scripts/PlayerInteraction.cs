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
        SoulNumChange(0);
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
        pm.pui.soulsHeldText.text = "" + soulsHeld;
        fx.Play();
        pm.pac.pickupSFX.clip = pm.pac.rngPickupClip[Random.Range(0, pm.pac.rngPickupClip.Length)];
        pm.pac.pickupSFX.Play();
        SoulNumChange(soulsGained);
    }

    public void SoulNumChange(int change)
    {
        //if (numOfSouls + change >= 0)
        //{
        //    numOfSouls += change;
        //}
        //else
        //{
        //    numOfSouls = 0;
        //}

        int newRed = 255 - (soulsHeld * 10);
        if (newRed < 0)
        {
            newRed = 0;
        }
        pm.pui.soulOrbImg.color = new Color32((byte)newRed, 255, 255, 255);

        int newTextColorNum = soulsHeld * 20;
        if (newTextColorNum > 255)
        {
            newTextColorNum = 255;
        }
        pm.pui.soulsHeldText.color = new Color32((byte)newTextColorNum, (byte)newTextColorNum, (byte)newTextColorNum, 255);
    }

    public void DepositSoulsFX (int soulsRemoved)
    {
        soulsHeld -= soulsRemoved;
        pm.pui.soulsHeldText.text = "" + soulsHeld;
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
