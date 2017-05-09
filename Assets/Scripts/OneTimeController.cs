using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeController : MonoBehaviour {

	public float timer;

	// Use this for initialization
	void Start () {
		Invoke("KillSelf", timer);
	}
	
	void KillSelf ()
	{
		Destroy(gameObject);
	}
}
