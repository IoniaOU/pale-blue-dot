using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
	
	private static LevelBuilder _instance;

	public static LevelBuilder Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<LevelBuilder> ();
			}

			return _instance;
		}
	}

	public GameObject Planet;
	public GameObject HotPlanet;
	public Sprite[] PlanetContinent;
	public List<GameObject> PlanetList = new List<GameObject> ();

	void Start ()
	{
		StartRound ();
	}

	void Update ()
	{
		
	}

	public void StopPlanets ()
	{
		foreach (GameObject planet in PlanetList) {
			planet.GetComponent<Planet> ().Stop ();
		}
	}

	private void StartRound ()
	{
		GameLogic.Instance.CurrentStatus = GameLogic.Status.Shoot;
		for (int i = 0; i < getPlanetCount (5,15); i++) {
			GameObject planet = GeneratePlanet (global::Planet.PlanetType.Life, getPlanetSpeed () * getOrbit (), getPlanetRadius (), getPlanetPosition (), getPlanetSize (0.1f, 0.2f), getPlanetDelay (), getPlanetHealth (), getPlanetReduceRate (), GetPlanetAngle (), SelectContinent (PlanetContinent));
			PlanetList.Add (planet);
		}
		for (int i = 0; i < getPlanetCount (5,10); i++) {
			GameObject planet = GeneratePlanet (global::Planet.PlanetType.Hot, getPlanetSpeed () * getOrbit (), getPlanetRadius (), getPlanetPosition (), getPlanetSize (0.05f, 0.15f), getPlanetDelay (), getPlanetHealth (), getPlanetReduceRate (), GetPlanetAngle (), SelectContinent (PlanetContinent));
			PlanetList.Add (planet);
		}
	}

	public void FinishRound ()
	{
		TargetLine.Instance.haveChance = false;
		foreach (GameObject planet in PlanetList) {
			if (Player.Instance.Target.transform != planet.transform) {
				Destroy (planet);
			}
		}
		ScoreManager.Instance.IncreaseScore ();
		PlanetList.Clear ();
		StartRound ();
	}

	private GameObject GeneratePlanet (Planet.PlanetType type, float speed, float radius, float position, float size, float delay, float health, float reduceRate, float angle, Sprite continent)
	{
		float x = Player.Instance.transform.position.x + getHorizantalChange ();
		float y = Player.Instance.transform.position.y + (getOrbit () * radius) + position + 3;

		GameObject planet = null;
		if (type == global::Planet.PlanetType.Life) {
			planet = Instantiate (Planet, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
		} else if (type == global::Planet.PlanetType.Hot) {
			planet = Instantiate (HotPlanet, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
		}

		planet.transform.localScale = new Vector3 (size, size, 0);
		planet.GetComponent<Planet> ().RotateSpeed = speed;
		planet.GetComponent<Planet> ().Radius = radius;
		planet.GetComponent<Planet> ().Delay = delay;
		planet.GetComponent<Planet> ().health = health;
		planet.GetComponent<Planet> ().reduceRate = reduceRate;
		planet.GetComponent<Planet> ().continent = continent;
		planet.GetComponent<Planet> ().Angle = angle;
		planet.GetComponent<Planet> ().CurrentType = type;
		return planet;
	}

	private int getPlanetCount (int min, int max)
	{
		return Random.Range (min, max);
	}

	private float getPlanetSpeed ()
	{
		return Random.Range (0.2f, 0.8f);
	}

	private float getPlanetRadius ()
	{
		return Random.Range (10.0f, 20.0f);
	}

	private float getPlanetPosition ()
	{
		return Random.Range (-2.0f, 4.0f);
	}

	private float getPlanetSize (float min, float max)
	{
		return Random.Range (min, max);
	}

	private float getPlanetDelay ()
	{
		return Random.Range (0.1f, 5.0f);
	}

	private float getHorizantalChange ()
	{
		return Random.Range (-1.0f, 1.0f);
	}

	private float getPlanetHealth ()
	{
		return Random.Range (50.0f, 130.0f);
	}

	private float GetPlanetAngle ()
	{
		return Random.Range (-1.0f, 1.0f);
	}

	private float getPlanetReduceRate ()
	{
		return Random.Range (10.0f, 20.0f);
	}

	private float getOrbit ()
	{
		return Random.Range (0, 100) > 50 ? -1 : 1;
	}

	private Sprite SelectContinent (Sprite[] continentList)
	{
		return continentList [Random.Range (0, continentList.Length - 1)];
	}

}
