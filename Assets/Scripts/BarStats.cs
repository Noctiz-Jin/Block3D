using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BarStats
{
	[SerializeField]
	private BarUIController bar;
	[SerializeField]
	private float maxVal;
	[SerializeField]
	private float currentVal;

	public float CurrentVal
	{
		get
		{
			return currentVal;
		}

		set
		{
			this.currentVal = Mathf.Clamp(value, 0, MaxVal);
			bar.Value = currentVal;
		}
	}

	public float MaxVal
	{
		get
		{
			return maxVal;
		}

		set
		{
			this.maxVal = value;
			bar.MaxValue = maxVal;
		}
	}

	public void Initialize(BarUIController barUIController, float maxValFloat, float currentValFloat)
	{
		bar = barUIController;
		maxVal = maxValFloat;
		currentVal = currentValFloat;

		this.MaxVal = maxVal;
		this.CurrentVal = currentVal;
	}
}
