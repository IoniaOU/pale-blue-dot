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
	private float _angle;
	private bool _run = true;


	void Start ()
	{
		gameObject.transform.GetChild (3).GetComponent<SpriteRenderer> ().sprite = continent;
		gameObject.transform.GetChild (4).GetComponent<SpriteRenderer> ().sprite = continent;
		_centre = transform.position;
	}

	void Update ()
	{
		if (_run) {
			ChangePosition ();
			ReduceHealth ();
		}
	}

	public void Stop ()
	{
		_run = false; 
	}

	private void ChangePosition ()
	{
		_angle += RotateSpeed * Time.deltaTime;
		var offset = new Vector2 (Mathf.Sin (_angle), Mathf.Cos (_angle)) * Radius;
		transform.position = _centre + offset;
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
}
