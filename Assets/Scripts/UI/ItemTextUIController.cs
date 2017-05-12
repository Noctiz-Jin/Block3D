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

	public int Value
	{
		set
		{
			textValue = value.ToString();
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
