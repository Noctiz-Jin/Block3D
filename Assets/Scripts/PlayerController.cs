using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 2;
	public float runSpeed = 6;
	const float defaultWalkSpeed = 2;
	const float defaultRunSpeed = 8;
	public float gravity = -12;
//	public float jumpHeight = 1;
	[Range(0,1)]
	public float airControlPercent;

	public float turnSmoothTime = 0.05f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;

	AnimatorStateInfo currentArmHandState;
	static int castState = Animator.StringToHash("Arm Hand Layer.Cast");

	Animator animator;
	Transform cameraT;
	CharacterController controller;

	private PlayerAction playerAction;

	void Start () {
		animator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController> ();
		playerAction = GetComponent<PlayerAction> ();
	}

	void Update () {
		// input
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;
		bool running = Input.GetKey (KeyCode.LeftShift);

		Move (inputDir, running);
//Input.GetKeyDown (KeyCode.Space)
		currentArmHandState = animator.GetCurrentAnimatorStateInfo(1);
		if (Input.GetButton("Jump")) {
			playerAction.CastSeed (transform);
			Cast ();
		}
		// animator
		float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
		animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
		animator.SetFloat ("animationSpeed", walkSpeed/defaultWalkSpeed);

		if(currentArmHandState.fullPathHash == castState && Input.GetButtonUp("Jump"))
		{
			animator.SetBool("isCasting", false);
		}
	}

//	void LateUpdate () {
//		if(currentArmHandState.fullPathHash == castState && Input.GetButtonUp("Jump"))
//		{
//			animator.SetBool("isCasting", false);
//		}
//	}

	void Move(Vector2 inputDir, bool running) {
		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
		}
			
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

		velocityY += Time.deltaTime * gravity;
		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

		controller.Move (velocity * Time.deltaTime);
		currentSpeed = new Vector2 (controller.velocity.x, controller.velocity.z).magnitude;

		if (controller.isGrounded) {
			velocityY = 0;
		}

	}

	void Cast() {
//		if (controller.isGrounded) {
//			float jumpVelocity = Mathf.Sqrt (-2 * gravity * jumpHeight);
//			velocityY = jumpVelocity;
//		}
		animator.SetBool("isCasting", true);

	}

	float GetModifiedSmoothTime(float smoothTime) {
		if (controller.isGrounded) {
			return smoothTime;
		}

		if (airControlPercent == 0) {
			return float.MaxValue;
		}
		return smoothTime / airControlPercent;
	}
}
