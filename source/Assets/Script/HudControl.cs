using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudControl : MonoBehaviour {



	void Start () {
		
	}
	
	void Update () {
		if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Shoot) {
			gameObject.transform.Rotate (0, 0, Time.deltaTime*10);
			GameObject target = Player.Instance.Target;
			if (target != null&& TargetLine.Instance.haveChance) {
				gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.GetComponent<SpriteRenderer> ().color, Color.red, target.GetComponent<Planet> ()._colorSpeed);
				if (gameObject.GetComponent<SpriteRenderer> ().color.r > 0.9f&&gameObject.GetComponent<SpriteRenderer> ().color.b < 0.1f) {
					GameLogic.Instance.ShowGameOver ("Homo sapiens have ruined their blue dot.");
					GameLogic.Instance.CurrentStatus = GameLogic.Status.Gameover;
				}
			}
		}
		else if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Lift || GameLogic.Instance.CurrentStatus == GameLogic.Status.Travel) {

			gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.GetComponent<SpriteRenderer> ().color, new Color (1.0f, 1.0f, 1.0f, 0.0f), 0.1f);
		}
		else if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Landing) {

			gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.GetComponent<SpriteRenderer> ().color, new Color (1.0f, 1.0f, 1.0f, 1.0f), 0.1f);
		}
		gameObject.transform.position = Player.Instance.gameObject.transform.position;
	}




}
