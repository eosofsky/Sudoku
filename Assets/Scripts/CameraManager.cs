using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	private Camera camera;
	private GameObject light;
	private Vector3 endPos; 
	private bool movingCamera;

	void Start () {
		camera = GetComponent<Camera> ();
		light = GameObject.FindGameObjectWithTag ("Light");
		endPos = camera.transform.position;
		movingCamera = false;
	}

	public void UpdateCamera () {
		endPos.y += 3.3f;
		movingCamera = true;
	}

	void FixedUpdate () {
		if (movingCamera) {
			Vector3 nextPos = camera.transform.position;
			nextPos.y += 0.05f;
			camera.transform.position = nextPos;//Vector3.Lerp (camera.transform.position, endPos, 10f * Time.deltaTime);
			if (endPos.y - camera.transform.position.y <= 0.1f) {
				movingCamera = false;
			}
		}
	}
}
