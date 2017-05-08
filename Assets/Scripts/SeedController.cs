using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour {

	public float fuseTime = 2.42f;
	[Range(0, 1)]
	public float transparency = 0.5f;
	public int damageRange = 5;
	public  ParticleSystem boom;

	private Transform player;
	private GameObject obstacleHolder;
	private BoxCollider boxCollider;
	private GameObject body;
	// Use this for initialization
	void Start () {
		obstacleHolder = GameObject.Find ("ObstacleLevel");
		player = GameObject.Find ("Player").transform;
		boxCollider = GetComponent<BoxCollider> ();
		body = transform.Find("Cube").gameObject;
		SetupTransparency(transparency);
		StartCoroutine (BoomCoroutine () );
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
			Invoke ("TurnOffTrigger", 0.1f);
		}
	}

	void TurnOffTrigger()
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
		Vector3 damagePosition = transform.position;

		HitPlayer(damagePosition);

		// spread damage
		for (int i = 1; i <= damageRange; i++)
		{
			HitPlayer(new Vector3(damagePosition.x + i, damagePosition.y, damagePosition.z));
			HitPlayer(new Vector3(damagePosition.x - i, damagePosition.y, damagePosition.z));
			HitPlayer(new Vector3(damagePosition.x, damagePosition.y, damagePosition.z + i));
			HitPlayer(new Vector3(damagePosition.x, damagePosition.y, damagePosition.z - i));
		}

		Destroy(gameObject);
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
