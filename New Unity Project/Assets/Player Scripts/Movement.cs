﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	int movementSpeed = 5;
	Rigidbody2D rb;

	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (Input.GetKey ("w")) {
			transform.Translate (Vector2.up / 5);
		} else if (Input.GetKey ("s")) {
			transform.Translate (Vector2.down / 5);
		} else if (Input.GetKey ("a")) {
			transform.Translate (Vector2.left / 5);
		} else if (Input.GetKey ("d")) {
			transform.Translate (Vector2.right / 5);
		}
	}
}