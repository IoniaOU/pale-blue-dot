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
		
	}
	
	void Update () {
		Loop ();
	}

	private void Loop ()
	{
		if (CheckInput ()) {
			
		}
	}

	private bool CheckInput()
	{
		return Input.touchCount > 0 || Input.GetKey (KeyCode.Space);
	}
}
