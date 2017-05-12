using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemStats
{
	[SerializeField]
	private int textValue;
	[SerializeField]
	private ItemTextUIController itemText;

	public int TextValue
	{
		get
		{
			return textValue;
		}

		set
		{
			this.textValue = value;
			itemText.Value = textValue;
		}
	}

	public void Initialize()
	{
		this.TextValue = textValue;
	}
}
