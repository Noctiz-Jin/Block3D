using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUIController : MonoBehaviour {

	private float fillAmount;
	[SerializeField]
	private float lerpSpeed;
	[SerializeField]
	private Image content;
	[SerializeField]
	private Color fullColor;
	[SerializeField]
	private Color lowColor;

	public float MaxValue { get; set; }

	public float Value
	{
		set
		{
			fillAmount = MyTool.MapNumber(value, 0, MaxValue, 0, 1);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateBar ();
	}

	void UpdateBar () {
		if (fillAmount != content.fillAmount) {
			content.fillAmount = Mathf.Lerp (content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
			content.color = Color.Lerp (lowColor, fullColor, fillAmount * 3);
		}
	}
}
