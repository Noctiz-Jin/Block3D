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
		castPosition.y = 1;
		seeds = new List<GameObject> ();
	}

	public void CastSeed (Transform playerTransform) {
		castPosition.x = Mathf.Round(playerTransform.position.x);
		castPosition.z = Mathf.Round(playerTransform.position.z);

		foreach (GameObject seed in seeds)
		{
			// stop duplicate casting
			if (seed.transform.position == castPosition)
				return;
		}

		GameObject castSeed = Instantiate (seedPrefab, castPosition, Quaternion.identity);
		seeds.Add(castSeed);
		castSeed.transform.SetParent (obstacleHolder.transform);
	}

}
