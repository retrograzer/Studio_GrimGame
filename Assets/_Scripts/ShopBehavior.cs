using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ShopBehavior : MonoBehaviour
{
    public GameObject shopMenu;
    public GameObject hud;
    public GameObject indicator;
    public Transform buttonLayoutGroup;
    public int maxSpawnedUpgrades = 2;

    [HideInInspector] public int soulsDeposited = 0;
    bool inTrigger = false;
    bool menuActive = false;
    ComponentToggler ct;
    Object[] allUpgrades;
    List<GameObject> spawnedButtons = new List<GameObject>();

    private void Awake()
    {
        allUpgrades = Resources.LoadAll("_Upgrades", typeof(GameObject));
    }

    void Start()
    {
        ct = GameObject.FindGameObjectWithTag("Player").GetComponent<ComponentToggler>();
        RefreshShopOptions();
    }

    void Update()
    {
        if (inTrigger && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            ToggleMenu();
        }
        if (Input.GetKeyDown(KeyCode.I))
            RefreshShopOptions();
    }

    void ToggleMenu ()
    {
        menuActive = !menuActive;
        ct.ToggleComponents(!menuActive);
        shopMenu.SetActive(menuActive);
        hud.SetActive(!menuActive);
    }

    void RefreshShopOptions ()
    {
        foreach (GameObject index in spawnedButtons)
            Destroy(index);

        spawnedButtons.Clear();

        for (int i = 0; i < maxSpawnedUpgrades; i++)
        {
            GameObject newButton = Instantiate(allUpgrades[Random.Range(0, allUpgrades.Length)], Vector3.zero, Quaternion.identity) as GameObject;
            spawnedButtons.Add(newButton);
            newButton.transform.SetParent(buttonLayoutGroup);
            newButton.GetComponent<UpgradeButtonBehavior>().AssignParent(this);
        }
    }

    public void BuyUpgrade ()
    {
        RefreshShopOptions();
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

    public void AddDepositedSouls (int soulsToAdd)
    {
        soulsDeposited += soulsToAdd;
    }
}
