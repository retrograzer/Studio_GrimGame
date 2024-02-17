using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepositDoorBehavior : MonoBehaviour
{

    public GameObject indicator;
    public ShopBehavior shop;
    public ParticleSystem depositFX;
    public TextMeshPro depositedNum;

    bool inTrigger = false;
    PlayerInteraction pi;

    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E) && pi.soulsHeld > 0)
        {
            DepositSouls();
        }
    }

    void DepositSouls ()
    {
        if (pi.soulsHeld > 0)
        {
            shop.AddDepositedSouls(pi.soulsHeld);
        }
        ParticleSystem.Burst newBurst = new ParticleSystem.Burst(0, pi.soulsHeld);
        depositFX.emission.SetBurst(0, newBurst);
        depositFX.Play();
        pi.DepositSoulsFX(pi.soulsHeld);
        indicator.SetActive(false);
        depositedNum.text = "" + shop.soulsDeposited;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = true;
            if (pi.soulsHeld > 0)
                indicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = false;
            indicator.SetActive(false);
        }
    }
}
