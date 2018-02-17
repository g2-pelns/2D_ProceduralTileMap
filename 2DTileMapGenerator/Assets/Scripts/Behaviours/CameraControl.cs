using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public float dragSpeed = 2;
	private Vector3 dragOrigin;

	public float zoomSpeed = 1;
	public float targetOrtho;
	public float smoothSpeed = 2.0f;
	public float minOrtho = 1.0f;
	public float maxOrtho = 20.0f;

	void Start(){
		targetOrtho = Camera.main.orthographicSize;
	}

	// Update is called once per frame
	void Update () {

		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll != 0.0f) {
			targetOrtho -= scroll * zoomSpeed;
			targetOrtho = Mathf.Clamp (targetOrtho, minOrtho, maxOrtho);
		}

		Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);

		if (Input.GetMouseButtonDown(1)) {
			dragOrigin = Input.mousePosition;
			return;
		}

		if (!Input.GetMouseButton(1)) {
			return;
		}

		Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - dragOrigin);

		Vector3 move = new Vector3 (pos.x * dragSpeed, pos.y * dragSpeed, 0);



		transform.Translate (move, Space.World);
	}
}
