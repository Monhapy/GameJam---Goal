using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public TimerHandler timer;  // Timer referansı
	public PlayerMovement movement;
	public static bool CheckGameOver;

	private void Start()
	{
		CheckGameOver = false;
	}

	private void Update()
	{
		if (CheckGameOver) 
		{
			timer.StopTimer();
			movement.enabled = false;
		}
	}

	
}
