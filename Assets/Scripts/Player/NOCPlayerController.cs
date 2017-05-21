using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NOCPlayerController : NetworkBehaviour {

	public ParticleSystem bubbleTrap;
	public GameObject ghost;
	public GameObject standingAura;

	////// Movement Assets //////
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
	////// ------ //////

	////// Components //////
	private Animator animator;
	private Transform cameraT;
	private CharacterController controller;
	private PlayerStats playerStats;
	private PlayerAction playerAction;
	AnimatorStateInfo currentArmHandState;
	static int castState = Animator.StringToHash("Arm Hand Layer.Cast");
	////// ------ //////

	void Awake () {
		GameObject.Find("MainLight").GetComponent<MenuUIController>().SecondCanvasOn();
		gameObject.name = "Player";
	}

	void Start () {
		animator = GetComponent<Animator> ();
		controller = GetComponent<CharacterController> ();
		playerAction = GetComponent<PlayerAction> ();
		playerStats = GetComponent<PlayerStats> ();

		if (!isLocalPlayer) {
			gameObject.name = "OtherPlayer";
		} else {
			SetupCamera();
			SetupStandingAura();
			cameraT = Camera.main.transform;
		}
	}

	void Update () {
		if (!isLocalPlayer) return;

		if (playerStats.isDying) {

		} else {
			// Parsing Input
			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			Vector2 inputDir = input.normalized;
			bool running = Input.GetKey (KeyCode.LeftShift);

			if (running && (inputDir.x != 0 || inputDir.y != 0)) {
				running = playerStats.Run();
			}

			// Move Character
			Move (inputDir, running);

			currentArmHandState = animator.GetCurrentAnimatorStateInfo(1);
			if (Input.GetButton("Jump")) {
				playerAction.CastSeed (transform);
				animator.SetBool("isCasting", true);
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

//		if (controller.isGrounded) {
//			float jumpVelocity = Mathf.Sqrt (-2 * gravity * jumpHeight);
//			velocityY = jumpVelocity;
//		}

	public void Dying() {

		if (playerStats.isDying) return;

		Transform bodyTransform = transform.Find("BodyCenter");
		Instantiate (bubbleTrap, bodyTransform.position, Quaternion.identity);
		playerStats.isDying = true;
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

		if (!isLocalPlayer) 
		{
			Destroy(gameObject);
			return;
		}

		GameObject ghostGO = Instantiate(ghost, transform.Find("BodyCenter").position, Quaternion.identity) as GameObject;

		ghostGO.name = "Ghost";

		GameObject.Find("MainLight").GetComponent<MenuUIController>().FirstCanvasOn();

		cameraT.GetComponent<ThirdPersonCamera> ().SwitchGhost();

		gameObject.SetActive(false);
	}

	void SetupStandingAura () {
		GameObject aura = Instantiate(standingAura, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0));
		aura.name = "StandingAura";
		aura.transform.SetParent(gameObject.transform);
	}

	void SetupCamera () {
		GameObject.Find("PlayerCamera").GetComponent<ThirdPersonCamera>().SwitchPlayer();
	}
}
