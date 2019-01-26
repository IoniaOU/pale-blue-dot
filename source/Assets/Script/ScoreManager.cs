using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	
	private static ScoreManager _instance;

	public static ScoreManager Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<ScoreManager> ();
			}

			return _instance;
		}
	}

	public Text scoreText;

	public GameObject hud;

	private int score = 0;

	public void IncreaseScore ()
	{
		score++;

	}

	public void Animate ()
	{
		hud.transform.GetComponent<Animator> ().SetTrigger ("Play");
	}

	void Update ()
	{
		scoreText.text = score.ToString ();
	}


}
