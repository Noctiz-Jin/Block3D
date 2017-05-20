using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {

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

	public bool isDying = false;
	// Seed Capacity

	// Seed Range

	// Use this for initialization
	void Awake () {
		stamina.Initialize(GameObject.Find("StaminaBar").GetComponent<BarUIController>(), 100f, 100f);
		seedRange.Initialize(GameObject.Find("ItemText2").GetComponent<ItemTextUIController>(), 1, -1);
		seedCapacity.Initialize(GameObject.Find("ItemText").GetComponent<ItemTextUIController>(), 1, 1);
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
		seedCapacity.TextCapacity += value;
	}

	public int GetSeedNumber () {
		return seedCapacity.TextValue;
	}

	public void CastSeed (int value)
	{
		seedCapacity.TextValue -= value;
	}

	public void SeedBoomRetrieve (int value) {
		if (isLocalPlayer) {
			seedCapacity.TextValue += value;
		}
	}
}
