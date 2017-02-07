using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	private Camera camera;
	private GameObject light;

	void Start () {
		camera = GetComponent<Camera> ();
		light = GameObject.FindGameObjectWithTag ("Light");
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			StartCoroutine (rotateCamera (false));
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			StartCoroutine (rotateCamera (true));
		}
	}

	IEnumerator	rotateCamera (bool left) {
		int amount = 10;
		Vector3 dir = left ? Vector3.up : -Vector3.up;
		for (int i = 0; i < 90 / amount; i++) {
			camera.transform.RotateAround (Vector3.zero, dir, amount);
			light.transform.RotateAround (Vector3.zero, dir, amount);
			yield return new WaitForSeconds (0.05f);
		}
		yield return null;
	}
}
