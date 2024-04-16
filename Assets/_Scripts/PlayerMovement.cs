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
    public Sprite[] moveSprites;

    Vector2 movement;
    PlayerManager pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (canMove && (movement.x > 0.2 || movement.y > 0.2 || movement.y < -0.2 || movement.x < -0.2))
        {
            if (!pm.pac.runSFX.isPlaying)
                pm.pac.runSFX.Play();
        }
        else
        {
            if (pm.pac.runSFX.isPlaying)
                pm.pac.runSFX.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            pm.rbd.MovePosition(pm.rbd.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        if (canMove)
        {
            if (movement.x > 0.1f)
            {
                pm.rend.sprite = moveSprites[3]; //Right
                pm.rend.flipX = false;
            }
            else
            {
                pm.rend.sprite = moveSprites[1]; //Left
                pm.rend.flipX = true;
            }
            if (movement.y > 0.1f)
            {
                pm.rend.sprite = moveSprites[2]; //Up
                pm.rend.flipX = false;
            }
            else if (movement.y < 0.1f && (movement.x > -0.1f && movement.x < 0.1f))
            {
                pm.rend.sprite = moveSprites[0]; //Down
                pm.rend.flipX = false;
            }
        }
    }
}
