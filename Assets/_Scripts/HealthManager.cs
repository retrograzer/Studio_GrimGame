using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    public int bladeToStickSpace = 60;
    public int stickToBladeSpace = 50;

    public GameObject stickImage;
    public GameObject bladeImage;

    public GameObject healthCanvas;
    public Transform currentCanvasPos;

    private List<GameObject> imageList;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        InstantiateHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeHealth(1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeHealth(-1);
        }
    }

    public void InstantiateHealth(int start)
    {
        imageList = new List<GameObject>();
        for (int i = 1; i <= maxHealth; i++)
        {
            PlaceImage(i);
        }
    }

    public void ChangeHealth(int change)
    {
        int healthToReach = currentHealth + change;
        if (change > 0)
        {
            while (currentHealth < healthToReach && currentHealth < maxHealth)
            {
                currentHealth++;
                PlaceImage(currentHealth);
            }
        }
        else
        {
            while (currentHealth > healthToReach && currentHealth > 0)
            {
                if (currentHealth % 2 == 0)
                {
                    currentCanvasPos.position = new Vector2(currentCanvasPos.position.x - stickToBladeSpace, currentCanvasPos.position.y);
                }
                else
                {
                    currentCanvasPos.position = new Vector2(currentCanvasPos.position.x - bladeToStickSpace, currentCanvasPos.position.y);
                }
                Destroy(imageList[imageList.Count-1]);
                imageList.RemoveAt(imageList.Count-1);
                currentHealth--;
            }
        }
    }

    public void PlaceImage(int healthNum)
    {
        if (healthNum % 2 == 1)
        {
            currentCanvasPos.position = new Vector2(currentCanvasPos.position.x + bladeToStickSpace, currentCanvasPos.position.y);
            GameObject image = Instantiate(stickImage);
            image.transform.SetParent(healthCanvas.transform, false);
            image.transform.position = new Vector2(currentCanvasPos.position.x, currentCanvasPos.position.y);
            imageList.Add(image);
        }
        else
        {
            currentCanvasPos.position = new Vector2(currentCanvasPos.position.x + stickToBladeSpace, currentCanvasPos.position.y);
            GameObject image = Instantiate(bladeImage);
            image.transform.SetParent(healthCanvas.transform, false);
            image.transform.position = new Vector2(currentCanvasPos.position.x, currentCanvasPos.position.y);
            imageList.Add(image);
        }
    }
}
