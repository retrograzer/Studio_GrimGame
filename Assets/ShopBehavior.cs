using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ShopBehavior : MonoBehaviour
{
    public GameObject shopMenu;
    public GameObject hud;
    public GameObject indicator;

    bool inTrigger = false;
    bool menuActive = false;
    ComponentToggler ct;

    void Start()
    {
        ct = GameObject.FindGameObjectWithTag("Player").GetComponent<ComponentToggler>();
    }

    void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu ()
    {
        menuActive = !menuActive;
        ct.ToggleComponents(!menuActive);
        shopMenu.SetActive(menuActive);
        hud.SetActive(!menuActive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = true;
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
