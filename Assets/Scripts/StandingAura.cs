using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingAura : MonoBehaviour {
	
	private Transform player;

	Vector3 position;
	// Use this for initialization
	void Start () {
		//position.y = 0.52f;
	}
	
	// Update is called once per frame
	void Update () {

		GameObject player = GameObject.Find("Player");
		if (player == null) {
			gameObject.SetActive (false);
		} else {
			transform.position = MyTool.RoundPlayerPositionWithY(player.transform, 0.52f);
		}
	}
}
