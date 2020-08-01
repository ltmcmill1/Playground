using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodChop : MonoBehaviour
{
	private bool inUse;

	private void OnTriggerEnter(Collider other)
	{
		if (inUse && other.gameObject.CompareTag("Generated"))
		{
			Destroy(other.gameObject);
		}
	}

	public void Use()
	{
		inUse = true;
	}

	public void StopUse()
	{
		inUse = false;
	}
}