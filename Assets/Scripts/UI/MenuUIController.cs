using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour {

	public Canvas FirstCanvas;
	public Canvas SecondCanvas;

	[SerializeField]
	private Camera playerCamera;

	void Awake () 
	{
		FirstCanvasOn ();
		SetupCamera();
	}

	public void FirstCanvasOn () 
	{
		FirstCanvas.gameObject.SetActive (true);
		SecondCanvas.gameObject.SetActive (false);
	}

	public void SecondCanvasOn () 
	{
		FirstCanvas.gameObject.SetActive (false);
		SecondCanvas.gameObject.SetActive (true);
	}

	public void LoadGameScene (int value)
	{
		Debug.Log("--- Load Game ---");

		SceneManager.LoadScene (value);
	}

	void SetupCamera () {
		if (playerCamera == null) return;
		Instantiate(playerCamera, new Vector3(0, 22, -37), Quaternion.identity).name = "PlayerCamera";
	}
}
