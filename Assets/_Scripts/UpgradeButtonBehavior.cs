using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeButtonBehavior : MonoBehaviour
{
    public enum UpgradeType
    {
        speed,
        lives,
        crows,
        dmg
    }

    public UpgradeType upgrade;
    public int initCost = 10;

    ShopBehavior shopParent;
    GameObject player;
    PlayerHealth ph;
    PlayerAttack pa;
    PlayerMovement pm;
    PlayerCrowAttack pca;
    TextMeshProUGUI buttonText;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ph = player.GetComponent<PlayerHealth>();
        pa = player.GetComponent<PlayerAttack>();
        pm = player.GetComponent<PlayerMovement>();
        pca = player.GetComponent<PlayerCrowAttack>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        AssignCost(initCost);
    }

    public void AssignParent (ShopBehavior newParent)
    {
        shopParent = newParent;
    }

    public void AssignCost (int cost)
    {
        buttonText.text += " (" + cost + ")";
    }

    public void OnButtonClick ()
    {
        if (shopParent.CanBuyUpgrade(initCost))
        {
            shopParent.AddDepositedSouls(-initCost);
            
            switch (upgrade)
            {
                case UpgradeType.speed:
                    pm.moveSpeed *= 1.3f;
                    break;
                case UpgradeType.lives:
                    ph.AddLife(1);
                    break;
                case UpgradeType.crows:
                    pca.AddCrows(1);
                    break;
                case UpgradeType.dmg:
                    pa.attackDamage++;
                    GameObject.Find("ScytheGraphic").GetComponent<Player_ScytheDamage>().damage++;
                    break;
            }

            shopParent.BuyUpgrade();
            //GameObject possibleCanvas = transform.parent.parent.parent.gameObject;
            //if (possibleCanvas.GetComponent<Canvas>())
            //    possibleCanvas.SetActive(false);
        }
        else
        {
            StartCoroutine(shopParent.FlashWarningText("NOT ENOUGH SOULS", 2f));
        }
    }
}
