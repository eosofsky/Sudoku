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
		} else if (Input.GetKeyDown ("j")) {
			CheckWin ();
		}

	}

	void rotateRow (float y, bool left) {
		for (int i = 0; i < 26; i++) {
			if (Mathf.Abs(pieces[i].transform.position.y  - (height + y)) <= 0.1) {
				StartCoroutine (rotate (pieces[i], left));
			}
		}
	}

	void UpdateTag (GameObject piece, bool left) {
		GameObject[] plane_As = GameObject.FindGameObjectsWithTag ("Plane_A");
		GameObject[] plane_Bs = GameObject.FindGameObjectsWithTag ("Plane_B");
		GameObject[] plane_Cs = GameObject.FindGameObjectsWithTag ("Plane_C");
		GameObject[] plane_Ds = GameObject.FindGameObjectsWithTag ("Plane_D");

		/* Plane A */
		for (int i = 0; i < plane_As.Length; i++) {
			if (plane_As [i].transform.parent != piece.transform) {
				continue;
			}
			string oldTag = plane_As[i].tag;
			if (left) {
				plane_As[i].tag = "Plane_D";
			} else {
				plane_As[i].tag = "Plane_B";
			}
		}

		/* Plane B */
		for (int i = 0; i < plane_Bs.Length; i++) {
			if (plane_Bs [i].transform.parent != piece.transform) {
				continue;
			}
			string oldTag = plane_Bs[i].tag;
			if (left) {
				plane_Bs[i].tag = "Plane_A";
			} else {
				plane_Bs[i].tag = "Plane_C";
			}
		}

		/* Plane C */
		for (int i = 0; i < plane_Cs.Length; i++) {
			if (plane_Cs [i].transform.parent != piece.transform) {
				continue;
			}
			string oldTag = plane_Cs[i].tag;
			if (left) {
				plane_Cs[i].tag = "Plane_B";
			} else {
				plane_Cs[i].tag = "Plane_D";
			}
		}

		/* Plane D */
		for (int i = 0; i < plane_Ds.Length; i++) {
			if (plane_Ds [i].transform.parent != piece.transform) {
				continue;
			}
			string oldTag = plane_Ds[i].tag;
			if (left) {
				plane_Ds[i].tag = "Plane_C";
			} else {
				plane_Ds[i].tag = "Plane_A";
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
		UpdateTag (piece, left);
		yield return null;
	}

	private void MakeRows(GameObject[] face, GameObject[] plane) {
		int top_index = 0;
		int middle_index = 3;
		int bottom_index = 6;
		for (int i = 0; i < 9; i++) {
			if (Mathf.Abs(plane[i].transform.position.y  - 0f) <= 0.1f) {
				face [bottom_index] = plane [i];
				bottom_index++;
			} else if (Mathf.Abs(plane[i].transform.position.y  - 1.1f) <= 0.1f) {
				face [middle_index] = plane [i];
				middle_index++;
			} else if (Mathf.Abs(plane[i].transform.position.y  - 2.2f) <= 0.1f) {
				face [top_index] = plane [i];
				top_index++;
			}
		}
	}

	private void Swap (GameObject[] face, int index_1, int index_2) {
		GameObject temp = face [index_1];
		face [index_1] = face [index_2];
		face [index_2] = temp;
	}

	private void MakeCols(GameObject[] face, bool front_or_back) {
		for (int r = 0; r < 3; r++) {
			for (int c = 0; c < 3; c++) {
				int i = r * 3 + c;
				float val, min, mid, max;
				if (front_or_back) {
					val = face [i].transform.position.x;
					min = -1.1f;
					mid = 0f;
					max = 1.1f;
				} else {
					val = face [i].transform.position.z;
					min = -0.6f;
					mid = 0.5f;
					max = 1.6f;
				}
				if (Mathf.Abs(val  - min) <= 0.1f) {
					Swap (face, i, r * 3 + 0);
				} else if (Mathf.Abs(val - mid) <= 0.1f) {
					Swap (face, i, r * 3 + 1);
				} else if (Mathf.Abs(val  - max) <= 0.1f) {
					Swap (face, i, r * 3 + 2);
				}
			}
		}

	}

	private GameObject[] MakeFace(GameObject[] plane, bool front_or_back) {
		GameObject[] face = new GameObject[9];
		MakeRows (face, plane);
		MakeCols (face, front_or_back);
		return face;
	}

	private bool CheckPlane (GameObject[] plane, bool front_or_back) {
		GameObject[] face = MakeFace (plane, front_or_back);

		/* Check rows */
		for (int r = 0; r < 3; r++) {
			bool hasOne = false;
			bool hasTwo = false;
			bool hasThree = false;
			for (int c = 0; c < 3; c++) {
				/*if (face [r * 3 + c] == 1) {
					hasOne = true;
				} else if (face [r * 3 + c] == 2) {
					hasTwo = true;
				} else if (face [r * 3 + c] == 3) {
					hasThree = true;
				}*/
			}
			if (!(hasOne && hasTwo && hasThree)) {
				return false;
			}
		}

		/* Check cols */
		for (int c = 0; c < 3; c++) {
			bool hasOne = false;
			bool hasTwo = false;
			bool hasThree = false;
			for (int r = 0; r < 3; r++) {
				/*if (face [r * 3 + c] == 1) {
					hasOne = true;
				} else if (face [r * 3 + c] == 2) {
					hasTwo = true;
				} else if (face [r * 3 + c] == 3) {
					hasThree = true;
				}*/
			}
			if (!(hasOne && hasTwo && hasThree)) {
				return false;
			}
		}

		return true;
	}

	public bool CheckWin () {
		GameObject[] front = GameObject.FindGameObjectsWithTag ("Plane_A");
		GameObject[] right = GameObject.FindGameObjectsWithTag ("Plane_B");
		GameObject[] back = GameObject.FindGameObjectsWithTag ("Plane_C");
		GameObject[] left = GameObject.FindGameObjectsWithTag ("Plane_D");

		return CheckPlane (front, true) && CheckPlane(right, false) &&
		    CheckPlane(back, true) && CheckPlane(left, false);
	}

}
