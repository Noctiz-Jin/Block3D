using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour {

	[SerializeField]
	private ParticleSystem pickUp;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PickUpEffect();
			Destroy(gameObject);
		}
	}

	void PickUpEffect()
	{
		Instantiate(pickUp, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
	}
}
