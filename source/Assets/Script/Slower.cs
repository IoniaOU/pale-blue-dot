using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log ("enter");
		if (collider.GetComponent<Planet> () != null) {
			collider.GetComponent<Planet> ().slow = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		Debug.Log ("exit");
		if (collider.GetComponent<Planet> () != null) {
			collider.GetComponent<Planet> ().slow = false;
		}
	}
}
