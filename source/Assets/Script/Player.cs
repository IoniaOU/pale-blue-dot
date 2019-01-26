using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private static Player _instance;

	public static Player Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<Player> ();
			}

			return _instance;
		}
	}

	void Start () {
		GameLogic.Instance.CurrentStatus = GameLogic.Status.Shoot;
	}
	
	void Update () {
		Loop ();
	}

	private void Loop ()
	{
		if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Shoot) {
			if (CheckInput ()) {
				LevelBuilder.Instance.StopPlanets ();
				GameLogic.Instance.CurrentStatus = GameLogic.Status.Lift;
			}
		} else if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Lift) {
			float x = Mathf.Lerp (gameObject.transform.localScale.x, 0.2f, 0.1f);
			float y = Mathf.Lerp (gameObject.transform.localScale.y, 0.2f, 0.1f);
			gameObject.transform.localScale = new Vector3(x, y, 1.0f);
		}
		else if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Travel) {
			gameObject.transform.position += new Vector3(0, 0.1f, 0);
		}
	}

	private bool CheckInput()
	{
		return Input.touchCount > 0 || Input.GetKey (KeyCode.Space);
	}

	private void CheckLiftFinish ()
	{
		if (0.2f-gameObject.transform.localScale.x < 0.01) {
			Debug.Log("Lift finished.");
			GameLogic.Instance.CurrentStatus = GameLogic.Status.Travel;
		}
	}
}
