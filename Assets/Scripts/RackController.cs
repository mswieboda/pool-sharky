using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackController : MonoBehaviour {

	private Transform rackTransform;
	private bool isRackingBalls;

	// Use this for initialization
	void Start () {
		isRackingBalls = false;
		rackTransform = GetComponent<Transform>();
	}

	private void FixedUpdate () {
		if (!isRackingBalls && Input.GetButtonDown("Rack")) {
			isRackingBalls = true;
//			Debug.Log("Rack the balls automatically");
			rackBalls();
		}
	}

	private void rackBalls() {

		// rack them balls back n forth
		rackTransform.Translate(new Vector3(1.0f, 0.0f, 0.0f));

		isRackingBalls = false;
	}
}
