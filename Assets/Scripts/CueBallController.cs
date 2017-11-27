using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallController : MonoBehaviour {
	public GameObject cueGuide;
	public GameObject cueBall;

	public float cueAcceleration;
	private Rigidbody rb;

	private Vector3 cueHitSpeed;
	private Vector3 cueHitPosition;

	private bool isCueShooting;
	private bool isCueReleased;

	private Vector3 cueGuideOffset;

	// Use this for initialization
	void Start() {
		isCueShooting = false;
		isCueReleased = false;
		rb = cueBall.GetComponent<Rigidbody>();

		cueGuideOffset = cueGuide.transform.position - cueBall.transform.position;
	}

	// NOTE: better for physics, because
	// fixed FPS? see notes on `void Update()`
	// not controlled by framerate
	private void FixedUpdate() {
		shotControls();
		rotateCueAimControls();
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
		Vector3 hitVector = cueGuide.transform.position;
		cueHitPosition = new Vector3(hitVector.x * 2.0f, hitVector.y * 2.0f, hitVector.z * 2.0f);

		Vector3 rotatedCueHitPosition = cueGuide.transform.localRotation * cueHitPosition;

		Vector3 rotatedCueHitSpeed = cueGuide.transform.localRotation * cueHitSpeed;

		// move the object (impluse like a hit)
		if (isCueShooting && isCueReleased) {
			rb.AddForceAtPosition(rotatedCueHitSpeed, rotatedCueHitPosition, ForceMode.Impulse);

			// reset bools for shooting cue
			isCueReleased = false;
			isCueShooting = false;

			// reset cue vars
			cueHitSpeed = new Vector3();
		}
	}

	private void rotateCueAimControls() {
		cueGuide.transform.position = cueBall.transform.position + cueGuideOffset;

		// Rotate using A and D
		if (Input.GetKey(KeyCode.A)) {
			cueGuide.transform.LookAt(cueGuide.transform);
			cueGuide.transform.Rotate(Vector3.left * Time.deltaTime * Mathf.Pow(cueAcceleration, 2f));
		}

		if (Input.GetKey(KeyCode.D)) {
			cueGuide.transform.LookAt(cueGuide.transform);
			cueGuide.transform.Rotate(Vector3.right * Time.deltaTime * Mathf.Pow(cueAcceleration, 2f));
		}
	}
}
