using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
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
		cueControls();
	}
	
	// Update is called once per frame
	// NOTE: this is bad for physics because
	// if someone has a better gaming computer
	// they're FPS is higher, so this will happen
	// more often then someone with a low FPS
	void Update() {
		
	}

	private void cueControls() {
		// keys AD shooter left/right controls
		float strafe = Input.GetAxis("Horizontal");
		// keys WS shooter forward/back controls
		float forwardBackward = Input.GetAxis("Vertical");

		// init the movement of the object (temp by WASD)
		if (Mathf.Abs(strafe) > 0 || Mathf.Abs(forwardBackward) > 0) {
			isCueShooting = true;
			isCueReleased = false;

			cueHitSpeed += new Vector3(strafe, 0.0f, forwardBackward) * cueAcceleration;
		}
		else {
			isCueReleased = true;
		}


		// Debug.Log("isCueShooting: " + isCueShooting + "\nisCueReleased: " + isCueReleased + "\ncueHitSpeed: " + cueHitSpeed);
		// Debug.Log("cueHitSpeed: " + cueHitSpeed);

		// temp center cue ball until we have cuePosition from cue stick obj etc
		cueHitPosition = new Vector3(rb.position.x / 2.0f, rb.position.y / 2.0f, rb.position.z / 2.0f);

		// move the object (impluse like a hit)
		//		rb.AddForce(movement, cueSpeed);
		//		rb.AddForceAtPosition(cueHitDirection, cueHitPosition);
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
