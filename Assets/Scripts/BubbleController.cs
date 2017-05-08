using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("KillSelf", 7f);
	}
	
	void KillSelf ()
	{
		Destroy(gameObject);
	}
}
