using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

	Transform cameraT;

	public float turnSmoothTime = 0.05f;
	float turnSmoothVelocity;
	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;

	public float ghostSpeed = 6;

	private float currentSpeed;
	// Use this for initialization
	void Start () {
		cameraT = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;
		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}
			
		float targetSpeed = ghostSpeed * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

		Vector3 velocity = transform.forward * currentSpeed;

		transform.Translate (velocity * Time.deltaTime, Space.World);
	}
}
