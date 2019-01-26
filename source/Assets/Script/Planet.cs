using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

	public float RotateSpeed;

	public float Radius;
	public float Delay;

	public float health;
	public float reduceRate;

	public Sprite continent;

	private Vector2 _centre;
	public float Angle;
	private bool _run = true;

	private float _currentSpeed;
	private float _slowSpeed;

	public bool slow;
	public bool stop = false;


	void Start ()
	{
		_currentSpeed = RotateSpeed;
		gameObject.transform.GetChild (3).GetComponent<SpriteRenderer> ().sprite = continent;
		gameObject.transform.GetChild (4).GetComponent<SpriteRenderer> ().sprite = continent;
		_centre = transform.position;
		if (RotateSpeed > 0) {
			_slowSpeed = 0.1f;
		} else {
			_slowSpeed = -0.1f;
		}
	}

	void Update ()
	{
		
		if (_run) {
			ChangeSpeed ();
			ChangePosition ();
			ReduceHealth ();
		}

		FadeOut ();
	}

	void OnMouseDown ()
	{
		if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Shoot) {
			Player.Instance.Target = this.gameObject;
			LevelBuilder.Instance.StopPlanets ();
			GameLogic.Instance.CurrentStatus = GameLogic.Status.Lift;
			LevelBuilder.Instance.PlanetList.Remove (gameObject);
			gameObject.GetComponent<Planet> ().enabled = false;

		}
	}

	public void Stop ()
	{
		stop = true;
	}

	private void ChangePosition ()
	{
		if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Shoot) {

			Angle += _currentSpeed * Time.deltaTime;
			var offset = new Vector2 (Mathf.Sin (Angle), Mathf.Cos (Angle)) * Radius;
			transform.position = _centre + offset;
		}
	}

	private void ReduceHealth ()
	{
		health -= Time.deltaTime * reduceRate;
		ChangeColor ();
	}

	private void ChangeColor ()
	{
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (35 / 255, 110 / 255, ((health + 65) + 27) / 255);
	}

	private void ChangeSpeed ()
	{
		
		if (slow) {
			_currentSpeed = Mathf.Lerp (_currentSpeed, _slowSpeed, 0.1f);
		} else if (stop) {
			_currentSpeed = Mathf.Lerp (_currentSpeed, 0, 0.1f);
		} else {
			_currentSpeed = Mathf.Lerp (_currentSpeed, RotateSpeed, 0.1f);
		}
	}

	private void FadeOut ()
	{
		if (GameLogic.Instance.CurrentStatus == GameLogic.Status.Lift || GameLogic.Instance.CurrentStatus == GameLogic.Status.Travel || GameLogic.Instance.CurrentStatus == GameLogic.Status.Landing) {
			if (Player.Instance.Target.transform != gameObject.transform) {
				gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.GetComponent<SpriteRenderer> ().color, new Color (1, 1, 1, 0), 0.1f);
				gameObject.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (0).GetComponent<SpriteRenderer> ().color, new Color (1, 1, 1, 0), 0.1f);
				gameObject.transform.GetChild (1).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (1).GetComponent<SpriteRenderer> ().color, new Color (1, 1, 1, 0), 0.1f);
				gameObject.transform.GetChild (2).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (2).GetComponent<SpriteRenderer> ().color, new Color (1, 1, 1, 0), 0.1f);
				gameObject.transform.GetChild (3).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (3).GetComponent<SpriteRenderer> ().color, new Color (1, 1, 1, 0), 0.1f);
				gameObject.transform.GetChild (4).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (4).GetComponent<SpriteRenderer> ().color, new Color (1, 1, 1, 0), 0.1f);
				gameObject.transform.GetChild (5).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (5).GetComponent<SpriteRenderer> ().color, new Color (1, 1, 1, 0), 0.1f);
			}
		}
	}
}
