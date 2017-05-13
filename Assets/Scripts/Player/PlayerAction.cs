using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

	public GameObject seedPrefab;

	private GameObject obstacleHolder;
	private Vector3 castPosition;
	private List<GameObject> seeds;

	private PlayerStats playStats;
	void Start () {
		obstacleHolder = GameObject.Find ("ObstacleLevel");
		seeds = new List<GameObject> ();

		playStats = GetComponent<PlayerStats> ();
	}

	public void CastSeed (Transform playerTransform)
	{
		if (playStats.GetSeedNumber() <= 0) {
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

		playStats.CastSeed(1);

		GameObject castSeed = Instantiate (seedPrefab, castPosition, Quaternion.identity);
		castSeed.GetComponent<SeedController>().damageRange = playStats.GetSeedRange();
		seeds.Add(castSeed);
		castSeed.transform.SetParent (obstacleHolder.transform);
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickable")
		{
			if (Random.Range(0, 2) == 0) {
				playStats.AddSeedCapacity(1);
			} else {
				playStats.AddSeedRange(1);
			}
		}
	}
}
