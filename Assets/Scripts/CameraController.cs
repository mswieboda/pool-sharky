using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject angledViewCamera;
	public GameObject cueBall;

	private Camera cueCamera;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		cueCamera = GetComponent<Camera>();
		offset = transform.position - cueBall.transform.position;
	}

	void FixedUpdate () {
		transform.position = cueBall.transform.position + offset;

		if (Input.GetButton("Alt Camera")) {
			cueCamera.enabled = false;
			angledViewCamera.SetActive(true);
		}
		else {
			cueCamera.enabled = true;
			angledViewCamera.SetActive(false);
		}
	}
}
