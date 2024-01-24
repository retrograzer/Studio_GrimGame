using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic Player Movement in Molly Minigun.
/// If you wanna use this script for other 2D top down games,
/// Set the Interpolation on the Rigidbody to Interpolate,
/// and take out the 3rd RequireComponent.
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    public bool canMove = true;

    Rigidbody2D rb;

    Vector2 movement;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (canMove)
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
}
