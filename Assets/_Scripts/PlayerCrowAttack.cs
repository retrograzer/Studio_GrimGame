using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrowAttack : MonoBehaviour
{
    public int crowsHeld = 0;
    public int maxCrowsHeld = 3;
    public GameObject crowPrefab;
    public float crowSpeed = 5f;

    PlayerManager pm;

    void Start()
    {
        pm = GetComponent<PlayerManager>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && crowsHeld > 0)
        {
            ThrowCrow();
        }
    }

    void ThrowCrow ()
    {
        crowsHeld--;

        // Convert mouse position from screen space to world space
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Ensure the z-coordinate is set to 0 for 2D
        mouseWorldPosition.z = 0;

        GameObject newCrowProj = Instantiate(crowPrefab, transform.position, Quaternion.identity);

        Vector3 direction = (mouseWorldPosition - transform.position).normalized;
        newCrowProj.GetComponent<Rigidbody2D>().velocity = direction * crowSpeed;
        newCrowProj.GetComponent<Projectile>().SetDirection(mouseWorldPosition);

        pm.pui.crowNumText.text = "x " + crowsHeld;
    }

    public void AddCrows (int crowsAdded)
    {
        crowsHeld += crowsAdded;

        Mathf.Clamp(crowsHeld, 0, maxCrowsHeld);
        pm.pui.crowNumText.text = "x " + crowsHeld;
    }
}
