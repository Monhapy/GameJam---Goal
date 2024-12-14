using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
	[SerializeField] private float speedBoostAmount = 7f; 
	[SerializeField] private float boostDuration = 2f;      

	public void Damage()
	{
		gameObject.SetActive(false);
		PlayerMovement.OnSpeedBoost?.Invoke(speedBoostAmount, boostDuration);
	}
}
