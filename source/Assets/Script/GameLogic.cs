using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public enum Status {Shoot, Zoomin, Lift, Travel, Landing, Zoomout, Gameover};

	public Status CurrentStatus;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
