using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour {

	public Canvas EntryCanvas;
	public Canvas CreditsCanvas;
	
	void Awake () 
	{
		EntryCanvasOn ();
	}

	public void CreditsCanvasOn () 
	{
		CreditsCanvas.gameObject.SetActive (true);
		EntryCanvas.gameObject.SetActive (false);
	}

	public void EntryCanvasOn () 
	{
		CreditsCanvas.gameObject.SetActive (false);
		EntryCanvas.gameObject.SetActive (true);
	}

	public void LoadGameScene ()
	{
		Debug.Log("--- Load Game ---");

		SceneManager.LoadScene (1);
	}
}
