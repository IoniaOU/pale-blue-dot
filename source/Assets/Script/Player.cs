using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


	public GameObject Target;

	private static Player _instance;

	public static Player Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<Player> ();
			}

			return _instance;
		}
	}

	void Start ()
	{
		GameLogic.Instance.CurrentStatus = GameLogic.Status.Shoot;
	}

	void Update ()
	{
		Loop ();
	}

	private void Loop ()
	{
		if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Lift) {
			float x = Mathf.Lerp (gameObject.transform.localScale.x, 0.2f, 0.1f);
			float y = Mathf.Lerp (gameObject.transform.localScale.y, 0.2f, 0.1f);
			gameObject.transform.localScale = new Vector3 (x, y, 1.0f);
			CheckLiftFinish ();
		} else if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Landing) {
			float x = Mathf.Lerp (gameObject.transform.localScale.x, 0.05f, 0.1f);
			float y = Mathf.Lerp (gameObject.transform.localScale.y, 0.05f, 0.1f);
			gameObject.transform.localScale = new Vector3 (x, y, 1.0f);

			float camX = Mathf.Lerp (Camera.main.transform.position.x, gameObject.transform.position.x, 0.1f);
			float camY = Mathf.Lerp (Camera.main.transform.position.y, gameObject.transform.position.y + 3.5f, 0.1f);
			Camera.main.transform.position = new Vector3 (camX, camY, -10.0f);

			CheckLanding ();
		} else if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Travel) {
			float x = Mathf.Lerp (gameObject.transform.position.x, Target.transform.position.x, 0.1f);
			float y = Mathf.Lerp (gameObject.transform.position.y, Target.transform.position.y, 0.1f);
			gameObject.transform.position = new Vector3 (x, y, 0.0f);
			CheckArrive ();
		}
	}

	private void CheckLiftFinish ()
	{
		if (0.2f - gameObject.transform.localScale.x < 0.01) {
			Debug.Log ("Lift finished.");
			GameLogic.Instance.CurrentStatus = GameLogic.Status.Travel;
		}
	}

	private void CheckArrive ()
	{
		if (Vector3.Distance (gameObject.transform.position, Target.transform.position) < 0.01f) {
			Debug.Log ("Arrived.");
			GameLogic.Instance.CurrentStatus = GameLogic.Status.Landing;
		}
	}

	private void CheckLanding ()
	{
		Vector3 diff = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 3.5f, -10.0f);

		if (Vector3.Distance (Camera.main.transform.position, diff) < 0.01f) {
			Debug.Log ("Landing finished.");
			LevelBuilder.Instance.FinishRound ();
		}
	}
}
