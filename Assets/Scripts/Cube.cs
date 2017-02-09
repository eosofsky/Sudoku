using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	public GameObject[] pieces;
	public float height = 0f;

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
			if (Mathf.Abs(pieces[i].transform.position.y  - (height + y)) <= 0.1) {
				StartCoroutine (rotate (pieces[i], left));
			}
		}
	}

	IEnumerator	rotate (GameObject piece, bool left) {
		int amount = 10;
		Vector3 dir = left ? Vector3.up : -Vector3.up;
		Vector3 point = Vector3.zero;
		point.y += height;
		for (int i = 0; i < 90 / amount; i++) {
			piece.transform.RotateAround (point, dir, amount);
			yield return new WaitForSeconds (0.05f);
		}
		yield return null;
	}

	void GetPlanes (GameObject[] face1, GameObject[] face2, GameObject[] face3, GameObject[] face4) {
		int[] indices = new int[4];
		for (int i = 0; i < 26; i++) {
			// front and back
			if (Mathf.Abs(pieces[i].transform.position.z  - 1.1f) <= 0.1) {
				face1[indices[0]] = pieces[i];
				indices [0]++;
			} else if (Mathf.Abs(pieces[i].transform.position.z  - (-1.1f)) <= 0.1) {
				face2[indices[1]] = pieces[i];
				indices [1]++;
			}

			// left and right
			if (Mathf.Abs(pieces[i].transform.position.x  - 1.1f) <= 0.1) {
				face3[indices[2]] = pieces[i];
				indices [2]++;
			} else if (Mathf.Abs(pieces[i].transform.position.x  - (-1.1f)) <= 0.1) {
				face4[indices[3]] = pieces[i];
				indices [3]++;
			}
		}
	}

	public bool CheckWin () {
		GameObject[] face1 = new GameObject[9]; // front
		GameObject[] face2 = new GameObject[9]; // back
		GameObject[] face3 = new GameObject[9]; // left
		GameObject[] face4 = new GameObject[9]; // right

		GetPlanes (face1, face2, face3, face4);

		return true;
	}

}
