using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

	public GameObject seedPrefab;

	private GameObject obstacleHolder;
	private Vector3 castPosition;
	private List<GameObject> seeds;

	void Start () {
		obstacleHolder = GameObject.Find ("ObstacleLevel");
		seeds = new List<GameObject> ();
	}

	public void CastSeed (Transform playerTransform) {
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

		GameObject castSeed = Instantiate (seedPrefab, castPosition, Quaternion.identity);
		seeds.Add(castSeed);
		castSeed.transform.SetParent (obstacleHolder.transform);
	}

}
