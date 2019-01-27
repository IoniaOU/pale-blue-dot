using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	private static GameLogic _instance;

	public static GameLogic Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<GameLogic> ();
			}

			return _instance;
		}
	}

	public enum Status {Shoot, Lift, Travel, Landing, RoundOver, StartAgain, Gameover};

	public Status CurrentStatus;

	public GameObject gameoverPanel;
	public Text gameoverMessage;
	public AudioClip gameOverSound;


	public enum GameOverPhase {VolumeOff, VolumeOn};

	public GameOverPhase CurrentPhase;

	void Start () {
		
	}
	
	void Update () {
		if (GameLogic.Instance.CurrentStatus == Status.Shoot) {
			Camera.main.GetComponent<AudioSource> ().volume = Mathf.Lerp (Camera.main.GetComponent<AudioSource> ().volume, 0.5f, 0.01f);
		}
		else if (GameLogic.Instance.CurrentStatus == Status.Gameover) {
			if (CurrentPhase == GameOverPhase.VolumeOff) {
				Camera.main.GetComponent<AudioSource> ().volume = Mathf.Lerp (Camera.main.GetComponent<AudioSource> ().volume, 0.0f, 0.05f);
				if (Camera.main.GetComponent<AudioSource> ().volume < 0.01f) {
					Camera.main.transform.GetComponent<AudioSource> ().clip = gameOverSound;
					CurrentPhase = GameOverPhase.VolumeOn;
					Camera.main.transform.GetComponent<AudioSource> ().Play ();
				}
			} else {
				Camera.main.GetComponent<AudioSource> ().volume = Mathf.Lerp (Camera.main.GetComponent<AudioSource> ().volume, 1.0f, 0.05f);
			}
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene ("Game");
	}

	public void GoMenu()
	{
		SceneManager.LoadScene ("Menu");
	}

	public void ShowGameOver(string message)
	{
		gameoverMessage.text = message;
		gameoverPanel.SetActive (true);
		CurrentPhase = GameOverPhase.VolumeOff;
	}
}
