using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObstacleController : MonoBehaviour {

	public ParticleSystem blockDestroy;
	public float offsetY;
	[SerializeField]
	private GameObject pickable;
	// Use this for initialization
	void Start () {
		
	}
	
	public void HitDamage() {
		
		blockDestroyEffect ();
		
		CmdLotteryDropPickable();
		
		Destroy(gameObject);

	}

	void blockDestroyEffect () {
		Instantiate (blockDestroy, transform.position, Quaternion.Euler(-90 ,0 ,0));
	}


	void CmdLotteryDropPickable () {

		if (Random.Range(0, 2) == 1)
		{
			Instantiate(pickable, transform.position, Quaternion.identity);
		}
	}
}
