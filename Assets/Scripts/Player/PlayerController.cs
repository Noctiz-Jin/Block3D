using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public ParticleSystem bubbleTrap;
	public GameObject ghost;
	public Camera playerCamera;
	public GameObject standingAura;

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

	bool isDying = false;

	AnimatorStateInfo currentArmHandState;
	static int castState = Animator.StringToHash("Arm Hand Layer.Cast");

	Animator animator;
	Transform cameraT;
	CharacterController controller;
	PlayerStats playerStats;

	private PlayerAction playerAction;

	void Start () {
		animator = GetComponent<Animator> ();
		controller = GetComponent<CharacterController> ();
		playerAction = GetComponent<PlayerAction> ();
		playerStats = GetComponent<PlayerStats> ();

		SetupCamera();
		SetupStandingAura();
		cameraT = Camera.main.transform;
	}

	void Update () {
		if (isDying) {

		} else {
			// input
			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			Vector2 inputDir = input.normalized;
			bool running = Input.GetKey (KeyCode.LeftShift);

			if (running && (inputDir.x != 0 || inputDir.y != 0)) {
				running = playerStats.Run();
			}

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

			// update all player stats
			playerStats.UpdatePlayerStats();
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

	public void Dying() {

		if (isDying) return;

		Transform bodyTransform = transform.Find("BodyCenter");
		Instantiate (bubbleTrap, bodyTransform.position, Quaternion.identity);
		isDying = true;
		animator.SetBool("isDying", true);
		animator.SetBool("isCasting", false);
		Invoke("Dead", 7f);
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

	void Dead() {

		GameObject ghostGO = Instantiate(ghost, transform.Find("BodyCenter").position, Quaternion.identity) as GameObject;

		ghostGO.name = "Ghost";

		cameraT.GetComponent<ThirdPersonCamera> ().SwitchGhost();

		gameObject.SetActive(false);
	}

	void SetupCamera () {
		Instantiate(playerCamera, new Vector3(0, 0, 0), Quaternion.identity).name = "PlayerCamera";
	}

	void SetupStandingAura () {
		Instantiate(standingAura, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0)).name = "StandingAura";
	}
}
