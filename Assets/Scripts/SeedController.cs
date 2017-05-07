using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour {

	private BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider> ();
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			boxCollider.isTrigger = false;
		}
	}
}
