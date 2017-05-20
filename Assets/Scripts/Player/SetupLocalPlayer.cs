using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if (isLocalPlayer)
		{

		}
	}

	public override void OnStartLocalPlayer()
	{
		SetEveryParameterAutoSend();
	}

	public override void PreStartClient ()
	{
		SetEveryParameterAutoSend();
	}

	private void SetEveryParameterAutoSend ()
	{
		GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
		GetComponent<NetworkAnimator>().SetParameterAutoSend(1, true);
		GetComponent<NetworkAnimator>().SetParameterAutoSend(2, true);
		GetComponent<NetworkAnimator>().SetParameterAutoSend(3, true);
	}
	
}
