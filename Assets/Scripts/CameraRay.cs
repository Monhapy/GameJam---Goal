using UnityEngine;

public class CameraRay : MonoBehaviour
{
	public Camera playerCamera; 
	public float rayLength = 100f; 
	private bool _isDrawingRay;
	private Vector3 _rayStart;
	private Vector3 _rayEnd;   
	private RaycastHit _hit;
	public Animator singAnimator;
	private void Update()
	{
		HandleRaycast();
	}

	private void HandleRaycast()
	{
		
		if (Input.GetMouseButton(0))
		{
			_isDrawingRay = true;

			singAnimator.SetBool("isShoot",true);
			var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
			_rayStart = ray.origin;

			if (Physics.Raycast(ray, out _hit, rayLength))
			{
				_rayEnd = _hit.point;

				
				var damageable = _hit.collider.GetComponent<IDamageable>();
				if (damageable != null)
				{
					damageable.Damage();
				}
			}
			else
			{
				_rayEnd = ray.origin + ray.direction * rayLength;
			}
		}
		else
		{
			_isDrawingRay = false;
			singAnimator.SetBool("isShoot",false);
		}
	}

	private void OnDrawGizmos()
	{
		if (!_isDrawingRay) return;

		Gizmos.color = Color.red;
		Gizmos.DrawLine(_rayStart, _rayEnd);
		Gizmos.DrawSphere(_rayEnd, 0.1f);
	}
}
