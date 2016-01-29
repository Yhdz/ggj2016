using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DanceMoveTest : MonoBehaviour {
	public AnimationCurve MovementCurve = new AnimationCurve ();
	public float MoveSpeed = 4.0f;

	private bool moving = false;
	private float time = 0.0f;
	private Vector3 oldPosition = Vector3.zero;
	private Vector3 direction = Vector3.zero;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!moving) {
			if (Input.GetKey(KeyCode.RightArrow)) {
				moving = true;
				time = 0.0f;
				oldPosition = transform.position;
				direction = Vector3.right;
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				moving = true;
				time = 0.0f;
				oldPosition = transform.position;
				direction = Vector3.left;
			}
			if (Input.GetKey(KeyCode.UpArrow)) {
				moving = true;
				time = 0.0f;
				oldPosition = transform.position;
				direction = Vector3.up;
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				moving = true;
				time = 0.0f;
				oldPosition = transform.position;
				direction = Vector3.down;
			}
		}

		if (moving) {
			time += Time.deltaTime * MoveSpeed;
			float movement = MovementCurve.Evaluate (time);
			transform.position = oldPosition + direction * movement;

			if (time > 1.0f) {
				moving = false;
			}
		}
	}
}
