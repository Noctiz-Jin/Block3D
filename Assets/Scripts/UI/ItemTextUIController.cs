using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTextUIController : MonoBehaviour {

	private string textValue;
	[SerializeField]
	private Image contentFrame;
	[SerializeField]
	private Image content;
	[SerializeField]
	private Text displayText;
	[SerializeField]
	private Color fullColor;
	[SerializeField]
	private Color lowColor;

	private int capacityValue = -1;
	private int contentValue = -1;

	public int CapacityValue 
	{
		get
		{
			return capacityValue;
		}
		set
		{
			this.capacityValue = value;
			if (value == -1)
			{
				textValue = Value.ToString();
			} else {
				displayText.color = Value == 0 ? lowColor : fullColor;
				textValue = Value.ToString() + "/" + value.ToString();
			}
		}
	}

	public int Value
	{
		get
		{
			return contentValue;
		}
		set
		{
			this.contentValue = value;
			if (CapacityValue == -1)
			{
				textValue = value.ToString();
			} else {
				displayText.color = value == 0 ? lowColor : fullColor;
				textValue = value.ToString() + "/" + CapacityValue.ToString();
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateItemText();
	}

	void UpdateItemText ()
	{
		if (textValue != displayText.text)
		{
			displayText.text = textValue;
		}
	}
}
