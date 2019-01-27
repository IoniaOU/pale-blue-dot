using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

	private static MenuManager _instance;

	public static MenuManager Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<MenuManager> ();
			}

			return _instance;
		}
	}


	public GameObject title;
	public GameObject paleBlueDot;

	void Start ()
	{
		title.gameObject.GetComponent<Animator> ().SetTrigger ("Start");
	}

	void Update ()
	{
		if ((Input.touchCount > 0 || Input.GetKeyDown (KeyCode.Space)) && (paleBlueDot.activeSelf)) {
			Debug.Log ("Anim started");
			paleBlueDot.GetComponent<Animator> ().SetTrigger ("PlayLast");
		}
	}

	public void StartPaleBlueDot ()
	{
		paleBlueDot.SetActive (true);
	}

	public void StartGame ()
	{
		SceneManager.LoadScene ("Game");
	}
}
