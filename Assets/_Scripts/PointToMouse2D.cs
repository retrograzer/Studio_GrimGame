using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToMouse2D : MonoBehaviour
{


	private void Update()
	{
		Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		if (Input.GetMouseButtonDown(2))
		{
			Debug.Log("Dir: " + dir);
		}
	}
}
