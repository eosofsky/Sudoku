using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	public GameObject[] pieces;

	void Update () {
		if (Input.GetKeyDown ("a")) {
			rotateRow (0f, true);
		} else if (Input.GetKeyDown ("s")) {
			rotateRow (1.1f, true);
		} else if (Input.GetKeyDown ("d")) {
			rotateRow (2.2f, true);
		} else if (Input.GetKeyDown ("f")) {
			rotateRow (0f, false);
		} else if (Input.GetKeyDown ("g")) {
			rotateRow (1.1f, false);
		} else if (Input.GetKeyDown ("h")) {
			rotateRow (2.2f, false);
		}
	}

	void rotateRow (float y, bool left) {
		for (int i = 0; i < 26; i++) {
			if (pieces[i].transform.position.y == y) {
				StartCoroutine (rotate (pieces[i], left));
			}
		}
	}

	IEnumerator	rotate (GameObject piece, bool left) {
		int absAmount = 10;
		int amount = left ? absAmount : -1 * absAmount;
		for (int i = 0; i < 90 / amount; i++) {
			piece.transform.RotateAround (Vector3.zero, Vector3.up, amount);
			yield return new WaitForSeconds (0.05f);
		}
		yield return null;
	}

}
