using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	private Camera camera;
	private GameObject light;
	private GameObject wave;
	private Vector3 endPos; 
	private bool movingCamera;

	void Start () {
		camera = GetComponent<Camera> ();
		light = GameObject.FindGameObjectWithTag ("Light");
		wave = GameObject.FindGameObjectWithTag ("Wave");
		endPos = camera.transform.position;
		movingCamera = false;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			StartCoroutine (rotateCamera (false));
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			StartCoroutine (rotateCamera (true));
		}
	}

	public void UpdateCamera () {
		endPos.y += 3.3f;
		movingCamera = true;
	}

	void FixedUpdate () {
		if (movingCamera) {
			Vector3 nextPos = camera.transform.position;
			nextPos.y += 0.1f;
			camera.transform.position = nextPos;//Vector3.Lerp (camera.transform.position, endPos, 10f * Time.deltaTime);
			if (endPos.y - camera.transform.position.y <= 0.1f) {
				movingCamera = false;
			}
		}
	}

	IEnumerator	rotateCamera (bool left) {
		int amount = 10;
		Vector3 dir = left ? Vector3.up : -Vector3.up;
		for (int i = 0; i < 90 / amount; i++) {
			camera.transform.RotateAround (Vector3.zero, dir, amount);
			light.transform.RotateAround (Vector3.zero, dir, amount);
			wave.transform.RotateAround (Vector3.zero, dir, amount);
			yield return new WaitForSeconds (0.05f);
		}
		yield return null;
	}

}
