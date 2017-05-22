using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAction : NetworkBehaviour {

	public GameObject seedPrefab;

	private GameObject obstacleHolder;
	private Vector3 castPosition;
	private List<Vector3> seeds;

	private PlayerStats playerStats;
	private NetworkInstanceId playerId;

	private float castLastStep, castCooldown = 0.1f;
	void Start () {
		obstacleHolder = GameObject.Find ("ObstacleLevel");
		seeds = new List<Vector3> ();

		playerStats = GetComponent<PlayerStats> ();
		playerId = GetComponent<NetworkIdentity>().netId;
	}

	public void CastSeed (Transform playerTransform)
	{
		if (playerStats.GetSeedNumber() <= 0) {
			return;
		}

		if(Time.time - castLastStep <= castCooldown) {
			return;
		} else {
			castLastStep = Time.time;
		}

		castPosition = MyTool.RoundPlayerPosition (playerTransform);

		//seeds.RemoveAll(i => i == null);
		foreach (Vector3 seed in seeds)
		{
			// stop duplicate casting
			if (seed == castPosition)
			{
				return;
			}
		}

		playerStats.CastSeed(1);
		CmdCastSeed(playerId, castPosition, playerStats.GetSeedRange());
		seeds.Add(castPosition);
	}

	public void RemoveSeedList(Vector3 seedPosToRemove)
	{
		seeds.RemoveAll(i => i == seedPosToRemove);
	}

	[Command]
	private void CmdCastSeed (NetworkInstanceId playerID, Vector3 castPosition, int seedRange)
	{
		GameObject castSeed = Instantiate (seedPrefab, castPosition, Quaternion.identity);
		castSeed.GetComponent<SeedController>().Initialize(playerID, seedRange);

		NetworkServer.Spawn(castSeed);
		RpcAddSeed(castSeed, playerID);
	}


	[ClientRpc]
	void RpcAddSeed(GameObject seed, NetworkInstanceId playerID)
	{
		if (playerID != GetComponent<NetworkIdentity>().netId) return;
		seed.transform.SetParent (obstacleHolder.transform);
	}

	void OnTriggerEnter(Collider other)
	{


		if (other.gameObject.tag == "Pickable")
		{
			if (!isLocalPlayer) return;
			if (Random.Range(0, 2) == 0) {
				playerStats.AddSeedCapacity(1);
			} else {
				playerStats.AddSeedRange(1);
			}
		}
	}
}
