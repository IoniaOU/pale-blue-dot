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

	public enum PlanetType {Life, Hot};

	public PlanetType CurrentType;

	private Color _desertGrass = new Color (223.0f / 255.0f, 112.0f / 255.0f, 30.0f / 255.0f, 1.0f);
	private Color _desertSoil = new Color (197.0f / 255.0f, 99.0f / 255.0f, 25.0f / 255.0f, 1.0f);
	private Color _desertSea = new Color (172.0f / 255.0f, 87.0f / 255.0f, 21.0f / 255.0f, 1.0f);
	private Color _fadeOut = new Color (1.0f, 1.0f, 1.0f, 0.0f);

	public float _colorSpeed = 0.01f;

	void Start ()
	{
		_currentSpeed = RotateSpeed;

		if (CurrentType == PlanetType.Life) {
			gameObject.transform.GetChild (3).GetComponent<SpriteRenderer> ().sprite = continent;
			gameObject.transform.GetChild (4).GetComponent<SpriteRenderer> ().sprite = continent;
			gameObject.transform.GetChild (5).transform.localPosition = GenerateCloudPosition ();
			gameObject.transform.GetChild (6).transform.localPosition = GenerateCloudPosition ();
		}

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
		health -= Time.deltaTime;
	}

	public void ChangeColor ()
	{
		gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.GetComponent<SpriteRenderer> ().color, _desertSea, 0.01f);

		if (CurrentType == PlanetType.Life) {
			gameObject.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (0).GetComponent<SpriteRenderer> ().color, _fadeOut, _colorSpeed);
			gameObject.transform.GetChild (1).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (1).GetComponent<SpriteRenderer> ().color, _fadeOut, _colorSpeed);
			gameObject.transform.GetChild (2).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (2).GetComponent<SpriteRenderer> ().color, _fadeOut, _colorSpeed);

			gameObject.transform.GetChild (3).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (3).GetComponent<SpriteRenderer> ().color, _desertSoil, _colorSpeed);
			gameObject.transform.GetChild (4).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (4).GetComponent<SpriteRenderer> ().color, _desertGrass, _colorSpeed);

			gameObject.transform.GetChild (5).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (5).GetComponent<SpriteRenderer> ().color, _fadeOut, _colorSpeed);
			gameObject.transform.GetChild (6).GetComponent<SpriteRenderer> ().color = Color.Lerp (gameObject.transform.GetChild (6).GetComponent<SpriteRenderer> ().color, _fadeOut, _colorSpeed);
		}
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

				if (CurrentType == PlanetType.Life) {
					foreach (SpriteRenderer renderer in gameObject.transform.GetComponentsInChildren<SpriteRenderer>()) {
						renderer.color = Color.Lerp (renderer.color, _fadeOut, 0.5f);
					}
				}
			}
		}
	}

	private Vector3 GenerateCloudPosition ()
	{
		return new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-2.0f, 2.0f), 0);
	}
}
