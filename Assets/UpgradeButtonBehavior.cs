using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonBehavior : MonoBehaviour
{
    public enum UpgradeType
    {
        speed,
        lives,
        crows
    }

    public UpgradeType upgrade;
    ShopBehavior shopParent;
    GameObject player;
    PlayerHealth ph;
    PlayerAttack pa;
    PlayerMovement pm;
    PlayerCrowAttack pca;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ph = player.GetComponent<PlayerHealth>();
        pa = player.GetComponent<PlayerAttack>();
        pm = player.GetComponent<PlayerMovement>();
        pca = player.GetComponent<PlayerCrowAttack>();
    }

    public void AssignParent (ShopBehavior newParent)
    {
        shopParent = newParent;
    }

    public void OnButtonClick ()
    {
        switch (upgrade)
        {
            case UpgradeType.speed:
                pm.moveSpeed *= 1.1f;
                break;
            case UpgradeType.lives:
                ph.AddLife(1);
                break;
            case UpgradeType.crows:
                pca.AddCrows(1);
                break;
        }
    }
}
