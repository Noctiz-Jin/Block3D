using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	[SerializeField]
	private BarStats stamina;
	[SerializeField]
	private float staminaDrainSpeed;
	[SerializeField]
	private float staminaRecoverSpeed;
	// Use this for initialization
	void Awake () {
		stamina.Initialize();
	}

	// MAIN update every player stats
	public void UpdatePlayerStats ()
	{
		stamina.CurrentVal += staminaRecoverSpeed * Time.deltaTime;
	}

	
	public bool Run ()
	{
		if (stamina.CurrentVal < 1) {
			return false;
		} else {
			stamina.CurrentVal -= staminaDrainSpeed * Time.deltaTime;
			return true;
		}
	}
}
