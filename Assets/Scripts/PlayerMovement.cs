using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 3f;
	private float _moveVectorOffset;
	public Camera mainCam;
	public static Action<float, float> OnSpeedBoost;

	private Coroutine _speedBoostCoroutine;
	public float _temporarySpeed; // Geçici hız değişkeni

	private void Start()
	{
		_moveVectorOffset = 1;
		_temporarySpeed = moveSpeed;  // Başlangıçta hızımız normal
	}

	private void OnEnable()
	{
		OnSpeedBoost += SetMoveSpeed;
	}

	private void OnDisable()
	{
		OnSpeedBoost -= SetMoveSpeed;
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		transform.position += new Vector3(0f, 0f, _moveVectorOffset) * (_temporarySpeed * Time.deltaTime);
	}

	private void SetMoveSpeed(float newSpeed, float duration)
	{
		// Eğer bir hız artışı Coroutine'i zaten çalışıyorsa durdur
		if (_speedBoostCoroutine != null)
		{
			StopCoroutine(_speedBoostCoroutine);
		}

		// Yeni hız artışı Coroutine'ini başlat
		_speedBoostCoroutine = StartCoroutine(TemporarySpeedBoost(newSpeed, duration));
	}

	private IEnumerator TemporarySpeedBoost(float newSpeed, float duration)
	{
		float originalSpeed = _temporarySpeed;  // Geçici hız, hareket hızımız
		_temporarySpeed += newSpeed;  // Hız artışı

		// Başlangıç FOV değeri
		float startFOV = mainCam.fieldOfView;
		float endFOV = 90;  // Yeni FOV değeri

		float elapsedTime = 0f;  // Geçen süre

		// Hız artışında FOV geçişi (smooth)
		while (elapsedTime < duration)
		{
			mainCam.fieldOfView = Mathf.Lerp(startFOV, endFOV, elapsedTime / duration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// Süre bitiminde kesin hedef FOV değerine ulaşalım
		mainCam.fieldOfView = endFOV;

		// Süre bitiminde hız geri dönüyor
		yield return new WaitForSeconds(duration);

		// Eski hız ve FOV değerine dönüyoruz
		_temporarySpeed = moveSpeed;  // Eski hıza dön
		startFOV = mainCam.fieldOfView;
		endFOV = 60;  // Eski FOV değeri
		elapsedTime = 0f;

		// FOV geri dönüşü
		while (elapsedTime < duration)
		{
			mainCam.fieldOfView = Mathf.Lerp(startFOV, endFOV, elapsedTime / duration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// Süre bitiminde kesin hedef FOV değerine ulaşalım
		mainCam.fieldOfView = endFOV;
	}
}
