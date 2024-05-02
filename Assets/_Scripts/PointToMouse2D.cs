using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToMouse2D : MonoBehaviour
{
	public bool targetSelf = true;
	public Transform target;

	private void Update()
	{
        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (targetSelf)
		{
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
		else
		{
			target.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }


		//if (Input.GetMouseButtonDown(2))
		//{
		//	Debug.DrawLine(transform.position, dir, Color.white, 2f);
		//}
	}
}
