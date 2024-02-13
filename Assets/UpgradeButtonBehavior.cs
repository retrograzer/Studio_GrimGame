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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ph = player.GetComponent<PlayerHealth>();
        pa = player.GetComponent<PlayerAttack>();
        pm = player.GetComponent<PlayerMovement>();
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
                break;
            case UpgradeType.lives:
                break;
            case UpgradeType.crows:
                break;
        }
    }
}
