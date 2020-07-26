using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListener : MonoBehaviour
{
	public Camera camera;

	void Update()
    {
		RaycastHit hit;
		if (Input.GetMouseButton(0)) {
			bool hitOccurred = Physics.Raycast(camera.transform.position, camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)) - camera.transform.position, out hit);
			if (hitOccurred) {
				hit.transform.position = new Vector3(hit.point.x, hit.transform.position.y, hit.point.z);
			}
		}
    }
}
