using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float mouseSensitivity = 100f; 
	public Transform playerBody;         
	public float moveSpeed = 5f;
	private bool isStart;
	private float _xRotation = 0f;        

	private void Start()
	{
		
		Cursor.lockState = CursorLockMode.Locked;
		
		transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
		
		playerBody.rotation = Quaternion.identity; 
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isStart = true;
		}

		if (!isStart) return;
		HandleMouseLook();
		HandleMovement();
		
	}

	private void HandleMouseLook()
	{
		
		var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		
		_xRotation -= mouseY;
		_xRotation = Mathf.Clamp(_xRotation, -90f, 90f); 
		
		transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
		
		playerBody.Rotate(Vector3.up * mouseX);
	}

	private void HandleMovement()
	{
		
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");

		
		var direction = transform.right * horizontal + transform.forward * vertical;
		
		transform.position += direction * (moveSpeed * Time.deltaTime);
	}
}
