using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour {

	public float fuseTime = 2.42f;
	[Range(0, 1)]
	public float transparency = 0.5f;
	public int damageRange = 5;
	public ParticleSystem boom;

	private Transform player;
	private GameObject obstacleHolder;
	private BoxCollider boxCollider;
	private GameObject body;
	private bool onExternTrigger = false;
	// Use this for initialization
	void Start () {
		obstacleHolder = GameObject.Find ("ObstacleLevel");
		player = GameObject.Find ("Player").transform;
		boxCollider = GetComponent<BoxCollider> ();
		body = transform.Find("Cube").gameObject;
		SetupTransparency(transparency);
		StartCoroutine (BoomCoroutine () );
	}

	public void TriggerBoom () {
		BoomDamage();
	}

	void SetupTransparency(float transparency)
	{
		Color color = body.GetComponent<Renderer>().material.color;
		color.a = transparency;
		body.GetComponent<Renderer>().material.color = color;
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			SetupTransparency(1f);
			Invoke ("TurnOnCollider", 0.1f);
		}
	}

	void TurnOnCollider()
	{
		boxCollider.isTrigger = false;
	}

	IEnumerator BoomCoroutine ()
	{
		yield return new WaitForSeconds (fuseTime);

		BoomDamage ();
	}

	void BoomEffect (Vector3 position)
	{
		Instantiate (boom, position, Quaternion.identity);
	}

	void BoomDamage ()
	{
		// Prevent Infi Looping UNITY WILL CRASH!
		if (onExternTrigger) {
			return;
		}

		onExternTrigger = true;
		Vector3 damagePosition = transform.position;

		HitPlayer(damagePosition);


		bool xp = true;
		bool xn = true;
		bool zp = true;
		bool zn = true;

		// spread damage
		for (int i = 1; i <= damageRange; i++)
		{
			if (xp) {
				if (HitBlockSeed(new Vector3(damagePosition.x + i, damagePosition.y, damagePosition.z))) {
					xp = false;
				}
				if (xp) {
					HitPlayer(new Vector3(damagePosition.x + i, damagePosition.y, damagePosition.z));
				}
			}

			if (xn) {
				if (HitBlockSeed(new Vector3(damagePosition.x - i, damagePosition.y, damagePosition.z))) {
					xn = false;
				}
				if (xn) {
					HitPlayer(new Vector3(damagePosition.x - i, damagePosition.y, damagePosition.z));
				}
			}

			if (zp) {
				if (HitBlockSeed(new Vector3(damagePosition.x, damagePosition.y, damagePosition.z + i))) {
					zp = false;
				}
				if (zp) {
					HitPlayer(new Vector3(damagePosition.x, damagePosition.y, damagePosition.z + i));
				}
			}

			if (zn) {
				if (HitBlockSeed(new Vector3(damagePosition.x, damagePosition.y, damagePosition.z - i))) {
					zn = false;
				}
				if (zn) {
					HitPlayer(new Vector3(damagePosition.x, damagePosition.y, damagePosition.z - i));
				}
			}
		}

		Destroy(gameObject);
	}

	bool HitBlockSeed(Vector3 damagePosition)
	{
		foreach (Transform block in obstacleHolder.transform)
		{
			if (block == null || block.gameObject == null) {

			} else {
				if (block.position == damagePosition)
				{
					if (block.gameObject.tag == "Static") {
						return true;
					} else if (block.gameObject.tag == "Seed") {
						block.gameObject.GetComponent<SeedController>().TriggerBoom();
					} else if (block.gameObject.tag == "Obstacle") {
						block.gameObject.GetComponent<ObstacleController>().HitDamage();
					} else {
						Debug.Log("Error: !!! Unknown GameObject in obstacleHolder !!!");
					}

					return true;
				}
			}
		}

		return false;
	}

	bool HitPlayer(Vector3 damagePosition)
	{
		Vector3 playerRoundPosition = MyTool.RoundPlayerPosition (player);

		BoomEffect (damagePosition);

		if (damagePosition == playerRoundPosition) {
			player.GetComponent<PlayerController> ().Dying();
			return true;
		} else {
			return false;
		}
	}
}
