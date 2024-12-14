using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other != null && other.gameObject.GetComponent<FinishLine>()!= null)
		{
			GameManager.CheckGameOver = true;
		}
		
	}
}
