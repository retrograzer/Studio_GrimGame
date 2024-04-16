using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform playerPos;
    Camera cam;
    public float deadZone = 2f; //Distance can move from thing
    public float dampTime = 1f;
    public float maxScreenPoint = 0.3f;
    public float ZOffSet = -10f;

    float dampAdjusted;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void LateUpdate()
    {
        dampAdjusted = dampTime * 0.01f;
        CheckThings();
    }


    private void CheckThings ()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 velocity = Vector3.zero;

        if (Vector2.Distance(playerPos.position, mousePos) > deadZone) //Exceeds the shit
        {

            mousePos = Input.mousePosition * maxScreenPoint + new Vector3(Screen.width, Screen.height, 0f) * ((1f - maxScreenPoint) * 0.5f);
            Vector3 position = (playerPos.position + GetComponent<Camera>().ScreenToWorldPoint(mousePos)) / 2f;
            Vector3 destination = new Vector3(position.x, position.y, ZOffSet);
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampAdjusted);

        }
        else //is within the shit
        {
            Vector3 position = (playerPos.position);
            Vector3 destination = new Vector3(position.x, position.y, ZOffSet);
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampAdjusted);
        }

    }
}
