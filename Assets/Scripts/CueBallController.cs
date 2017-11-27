using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallController : MonoBehaviour {
	public float cueAcceleration;
	private Rigidbody rb;

	private Vector3 cueHitSpeed;
	private Vector3 cueHitPosition;

	private bool isCueShooting;
	private bool isCueReleased;

	// Use this for initialization
	void Start() {
		isCueShooting = false;
		isCueReleased = false;
		rb = GetComponent<Rigidbody>();
	}

	// NOTE: better for physics, because
	// fixed FPS? see notes on `void Update()`
	// not controlled by framerate
	private void FixedUpdate() {
		shotControls();
	}
	
	// Update is called once per frame
	// NOTE: this is bad for physics because
	// if someone has a better gaming computer
	// they're FPS is higher, so this will happen
	// more often then someone with a low FPS
	void Update() {
		
	}

	private void shotControls() {
		float shotSpeed = Input.GetAxis("Vertical");

		// get hit speed
		if (shotSpeed > 0) {
			isCueShooting = true;
			isCueReleased = false;

			cueHitSpeed += new Vector3(0.0f, 0.0f, shotSpeed) * cueAcceleration;
		}
		else {
			isCueReleased = true;
		}

		// temp center cue ball until we have cuePosition from cue stick obj etc
		cueHitPosition = new Vector3(rb.position.x / 2.0f, rb.position.y / 2.0f, rb.position.z / 2.0f);

		// move the object (impluse like a hit)
		if (isCueShooting && isCueReleased) {
			rb.AddForceAtPosition(cueHitSpeed, cueHitPosition, ForceMode.Impulse);

			// reset bools for shooting cue
			isCueReleased = false;
			isCueShooting = false;

			// reset cue vars
			cueHitSpeed = new Vector3();
			// center ball by default
			cueHitPosition = new Vector3(rb.position.x / 2.0f, rb.position.y / 2.0f, rb.position.z / 2.0f);
		}
	}
}
