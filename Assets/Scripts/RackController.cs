using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackController : MonoBehaviour {

	private Transform rackTransform;
	private int rackDirection;

	// Use this for initialization
	void Start () {
		rackTransform = GetComponent<Transform>();
		rackDirection = 1;
	}

	private void FixedUpdate () {
		if (Input.GetButton("Rack")) {
			if (Input.GetKey(KeyCode.LeftShift)) {
				gameObject.SetActive(false);
			}
			else {
				rackDirection *= -1;
				rackBalls(rackDirection);
			}
		}
	}

	private void rackBalls(int direction) {
		Vector3 newPosition = rackTransform.position;
		newPosition.z += 30f + Random.Range(1f, 5f) * direction;

		rackTransform.position = Vector3.MoveTowards(rackTransform.position, newPosition, 10f * Time.deltaTime);
	}
}
