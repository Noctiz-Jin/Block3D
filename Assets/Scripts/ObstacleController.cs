using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

	public ParticleSystem blockDestroy;
	public float offsetY;
	// Use this for initialization
	void Start () {
		
	}
	
	public void HitDamage() {
		
		blockDestroyEffect ();

		Destroy(gameObject);
	}

	void blockDestroyEffect () {
		Instantiate (blockDestroy, transform.position, Quaternion.Euler(-90 ,0 ,0));
	}
}
