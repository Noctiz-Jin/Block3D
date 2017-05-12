using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// singleton self
	public static GameManager instance = null;
	// WorldManager instance
	private WorldManager worldManager;


	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
		Debug.Log("--GameManager Loaded--");

		worldManager = GetComponent<WorldManager> ();
		worldManager.BluePrint ();
		worldManager.SetupScene ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
