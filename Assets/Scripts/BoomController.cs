using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("KillSelf", 1f);
	}
	
	void KillSelf ()
	{
		Destroy(gameObject);
	}
}
