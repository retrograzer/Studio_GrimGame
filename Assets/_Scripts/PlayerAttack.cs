using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackStartingDelay = 0.25f;
    public float attackSwingDuration = 0.15f;
    public float attackDistance = 2f;
    public LayerMask attackLayer;

    LineRenderer lr;
    bool canAttack = true;

    private void Awake()
    {
        lr = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            canAttack = false;
            StartCoroutine(SwingScythe());
        }
    }

    IEnumerator SwingScythe ()
    {
        yield return new WaitForSeconds(attackStartingDelay);

        // Convert mouse position from screen space to world space
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Ensure the z-coordinate is set to 0 for 2D
        mouseWorldPosition.z = 0;

        // Calculate the direction vector in world space from the player to the mouse position
        Vector3 dir = mouseWorldPosition - transform.position;

        // Now 'dir' is in world space, and you can normalize it if you need a unit vector for direction
        Vector3 normalizedDir = dir.normalized;

        //Useful for seeing the end of the line
        Vector3 rayTip = transform.position + normalizedDir * attackDistance;

        //Cast a ray for viewing
        //Debug.DrawRay(transform.position, rayTip, Color.white, 2f);

        //Cast a ray and check collisions on that layer
        RaycastHit2D scytheRay = Physics2D.Raycast(transform.position, normalizedDir, attackDistance, attackLayer);
        if (scytheRay.collider)
        {
            Debug.Log("Hit " + scytheRay.collider);
        }

        //Set the line renderers for debugging
        Vector3[] positions = { transform.position, rayTip };
        lr.SetPositions(positions);

        yield return new WaitForSeconds(attackSwingDuration);

        //Kill the line renderers
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.zero);

        canAttack = true;
    }
}
