using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerHandler : MonoBehaviour
{
	[SerializeField] private TMP_Text timerText;  // Sayacın UI Text'i (isteğe bağlı)
	[SerializeField] private float startTime = 0f;  // Başlangıç zamanı
	private float currentTime;
	private bool isTimerRunning;

	private void Start()
	{
		currentTime = startTime;
		isTimerRunning = true;
	}

	private void Update()
	{
		if (isTimerRunning)
		{
			// Zamanı arttır
			currentTime += Time.deltaTime;

			// UI'ye zamanı güncelle (isteğe bağlı)
			UpdateTimerUI();
			
			if (GameIsOver())  // Burada oyunun bitip bitmediğini kontrol eden bir fonksiyon olmalı
			{
				StopTimer();
			}
		}
	}

	private void UpdateTimerUI()
	{
		// UI'deki sayacı güncelle (örneğin: "00:30")
		float minutes = Mathf.FloorToInt(currentTime / 60);
		float seconds = Mathf.FloorToInt(currentTime % 60);
		timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	private bool GameIsOver()
	{
		return false; // Oyuncu oyun bitirdiğinde burada true dönecek
	}

	public void StopTimer()
	{
		isTimerRunning = false;  // Sayaç durur
	}

	public void ResetTimer()
	{
		currentTime = 0f;
		isTimerRunning = true;
	}
}
