using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	private BoxRoomNoLid BRNL;

	public void BluePrint() {
		BRNL = GetComponent<BoxRoomNoLid> ();
	}

	public void SetupScene() {
		BRNL.SetupScene ();
	}

//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
}
