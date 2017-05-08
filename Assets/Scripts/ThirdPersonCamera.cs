using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	public bool lockCursor;
	public float mouseSensitivity = 10;
	public float dstFromTarget = 4;
	public Vector2 pitchMinMax = new Vector2 (0, 85);

	public float rotationSmoothTime = .12f;

	private Transform target;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	float yaw;
	float pitch;

	// Use this for initialization
	void Start () {
		target = GameObject.Find("Player/CameraPivot").transform;
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;

		transform.position = target.position - transform.forward * dstFromTarget;
	}

	public void SwitchGhost () {
		target = GameObject.Find("Ghost").transform;
	}
}
