using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	// Stamima
	[SerializeField]
	private BarStats stamina;
	[SerializeField]
	private ItemStats seedCapacity;
	[SerializeField]
	private ItemStats seedRange;
	[SerializeField]
	private float staminaDrainSpeed;
	[SerializeField]
	private float staminaRecoverSpeed;

	// Seed Capacity

	// Seed Range

	// Use this for initialization
	void Awake () {
		stamina.Initialize();
		seedRange.Initialize();
		seedCapacity.Initialize();
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

	public int GetSeedRange () {
		return seedRange.TextValue;
	}

	public void AddSeedRange (int value) {
		seedRange.TextValue += value;
	}

	public void AddSeedCapacity (int value) {
		seedCapacity.TextValue += value;
	}
}
