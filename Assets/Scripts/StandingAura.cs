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
		player = GameObject.Find("Player").transform;
		transform.position = MyTool.RoundPlayerPositionWithY(player, 0.52f);
	}
}
