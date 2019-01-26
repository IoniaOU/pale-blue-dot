using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudControl : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Shoot) {
			gameObject.transform.Rotate (0, 0, Time.deltaTime*10);
		}
	}




}
