using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLine : MonoBehaviour {

	public bool haveChance = false;

	private static TargetLine _instance;

	public static TargetLine Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<TargetLine> ();
			}

			return _instance;
		}
	}

	private Vector3 virtualTarget;

	private void LerpToTarget()
	{
		
	}
	
	void Update () {
		GameObject possibleTarget = FindTarget ();
		if (possibleTarget != null && GameLogic.Instance.CurrentStatus == GameLogic.Status.Shoot) {
			gameObject.GetComponent<LineRenderer> ().enabled = true;
			this.GetComponent<LineRenderer> ().SetPosition (0, Player.Instance.gameObject.transform.position);
			virtualTarget = Vector3.Lerp (virtualTarget, possibleTarget.transform.position, 0.2f);
			this.GetComponent<LineRenderer> ().SetPosition (1, virtualTarget);
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
					if (planet.GetComponent<Planet> ().CurrentType == Planet.PlanetType.Life) {
						haveChance = true;
					}
				}
			}
		}

		if (distance == 999.0f) {
			return null;
		}

		return possibleTarget;
	}
}
