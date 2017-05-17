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
	private int textCapacity;
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

	public int TextCapacity
	{
		get
		{
			return textCapacity;
		}

		set
		{
			this.textCapacity = value;
			itemText.CapacityValue = textCapacity;
		}
	}

	public void Initialize(ItemTextUIController itemTextUIController, int textValueInt, int textCapacityInt)
	{
		itemText = itemTextUIController;
		textValue = textValueInt;
		textCapacity = textCapacityInt;

		this.TextValue = textValue;
		this.TextCapacity = textCapacity;
	}
}
