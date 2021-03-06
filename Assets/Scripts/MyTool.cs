﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTool {

	public static Vector3 RoundPlayerPosition (Transform player) {
		return RoundPlayerPositionWithY(player, 1f);
	}

	public static Vector3 RoundPlayerPositionWithY (Transform player, float y) {
		Vector3 playerRoundPosition = player.position;
		playerRoundPosition.y = y;
		playerRoundPosition.x = Mathf.Round(player.position.x);
		playerRoundPosition.z = Mathf.Round(player.position.z);
		return playerRoundPosition;
	}

	public static float MapNumber (float value, float inMin, float inMax, float outMin, float outMax)
	{
		if ((inMax - inMin) == 0) return -1f;
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
