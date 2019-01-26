﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLine : MonoBehaviour {

	private static TargetLine _instance;

	public static TargetLine Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<TargetLine> ();
			}

			return _instance;
		}
	}
	
	// Update is called once per frame
	void Update () {
		GameObject possibleTarget = FindTarget ();

		if (possibleTarget != null && GameLogic.Instance.CurrentStatus == GameLogic.Status.Shoot) {
			gameObject.GetComponent<LineRenderer> ().enabled = true;
			this.GetComponent<LineRenderer> ().SetPosition (0, Player.Instance.gameObject.transform.position);
			this.GetComponent<LineRenderer> ().SetPosition (1, possibleTarget.transform.position);
		} else {
			gameObject.GetComponent<LineRenderer> ().enabled = false;
		}


	}

	public GameObject FindTarget()
	{
		float distance = 999.0f;
		GameObject possibleTarget = null;
		foreach (GameObject planet in LevelBuilder.Instance.PlanetList) {
			if (planet.GetComponent<Planet> ().slow) {
				if (Mathf.Abs (planet.transform.position.x) < distance) {
					distance = Mathf.Abs (planet.transform.position.x);
					possibleTarget = planet;
				}
			}
		}

		if (distance == 999.0f) {
			return null;
		}

		return possibleTarget;
	}
}