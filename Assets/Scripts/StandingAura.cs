using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingAura : MonoBehaviour {

	public Transform target;

	Vector3 position;
	// Use this for initialization
	void Start () {
		//position.y = 0.52f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = MyTool.RoundPlayerPositionWithY(target, 0.52f);
	}
}
