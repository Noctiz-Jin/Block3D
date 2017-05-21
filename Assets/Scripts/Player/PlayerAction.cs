using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAction : NetworkBehaviour {

	public GameObject seedPrefab;

	private GameObject obstacleHolder;
	private Vector3 castPosition;
	private List<GameObject> seeds;

	private PlayerStats playerStats;
	private NetworkInstanceId playerId;
	void Start () {
		obstacleHolder = GameObject.Find ("ObstacleLevel");
		seeds = new List<GameObject> ();

		playerStats = GetComponent<PlayerStats> ();
		playerId = GetComponent<NetworkIdentity>().netId;
	}

	public void CastSeed (Transform playerTransform)
	{
		if (playerStats.GetSeedNumber() <= 0) {
			return;
		}

		castPosition = MyTool.RoundPlayerPosition (playerTransform);

		seeds.RemoveAll(i => i == null);
		foreach (GameObject seed in seeds)
		{
			// stop duplicate casting
			if (seed == null) {

			} else {
				if (seed.transform.position == castPosition)
				return;
			}
		}

		playerStats.CastSeed(1);
		CmdCastSeed(playerId, castPosition);
	}

	[Command]
	private void CmdCastSeed (NetworkInstanceId playerID, Vector3 castPosition)
	{
		GameObject castSeed = Instantiate (seedPrefab, castPosition, Quaternion.identity);
		castSeed.GetComponent<SeedController>().Initialize(playerID, playerStats.GetSeedRange());
		seeds.Add(castSeed);
		castSeed.transform.SetParent (obstacleHolder.transform);

		NetworkServer.Spawn(castSeed);
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
