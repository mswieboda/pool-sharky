using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject angledViewCamera;
	public GameObject cueBall;
	public GameObject cueGuide;
	public GameObject cueGhostGuide;

	private Camera cueCamera;
	private AudioListener cueCameraAudio;
	private Vector3 offset;
	private float offsetYRotation;

	// Use this for initialization
	void Start () {
		cueCamera = GetComponent<Camera>();
		cueCameraAudio = cueCamera.GetComponent<AudioListener>();
		offset = transform.position - cueBall.transform.position;
		offsetYRotation = cueGuide.transform.rotation.y - transform.rotation.y;
	}

	void LateUpdate () {

		if (Input.GetButton("Alt Camera")) {
			angledViewCamera.transform.LookAt(cueBall.transform);
		}

		smoothFollow(cueGhostGuide.transform);
	}

	private void smoothFollow(Transform target) {
		float distance = 10.0f;
		float height = 5.0f;
		float heightDamping = 2.0f;
		float rotationDamping = 3.0f;

		// Early out if we don't have a target
		if (!target) {
			return;
		}

		// Calculate the current rotation angles
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;

		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

		// Damp the height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		// Set the height of the camera
		transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);

		// Always look at the target
		transform.LookAt(target);
	}

	void FixedUpdate () {
		if (Input.GetButton("Alt Camera")) {
			cueCamera.enabled = false;
			cueCameraAudio.enabled = false;

			angledViewCamera.SetActive(true);
		}
		else {
			cueCamera.enabled = true;
			cueCameraAudio.enabled = true;

			angledViewCamera.SetActive(false);
		}
	}
}
