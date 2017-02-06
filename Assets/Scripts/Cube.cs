using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	public GameObject[] pieces;

	/* Rows */
	//private bool rotatingBottomRow = false;

	void Update () {
		//if (!rotatingBottomRow) {
			if (Input.GetAxis ("Jump") == 1f) {
				rotateBottomRow ();
			}
		//}
		//rotate ();
	}

	/*void rotate () {
		if (rotatingBottomRow) {
			Debug.Log ("end: " + bottomRowEndRot.y);
			Debug.Log ("current: " + bottomRow.transform.rotation.y);
			if (bottomRowEndRot.y > bottomRow.transform.rotation.y + 0.01) {
				//bottomRow.transform.Rotate (Vector3.up * Time.deltaTime * speed);
				bottomRow.transform.rotation = Quaternion.Lerp (bottomRow.transform.rotation, bottomRowEndRot, 0.1f);
			} else {
				rotatingBottomRow = false;
			}
		}
	}*/

	void rotateBottomRow () {
		//Quaternion start = bottomRow.transform.rotation;
		//Quaternion end = Quaternion.Euler (bottomRow.transform.eulerAngles + new Vector3 (0, 90, 0));
		//bottomRowEndRot = Quaternion.Euler (bottomRow.transform.eulerAngles + new Vector3 (0, 90, 0));
		//rotatingBottomRow = true;

		//StartCoroutine (rotateBottomRowHelper());

		//bottomRow.transform.Rotate (Vector3.up);

		//bool bottomRowProgress [] = new bool[9];
		//int index = 0;
		for (int i = 0; i < 26; i++) {
			if (pieces[i].transform.position.y == 0) {
				//bottomRow[index] = pieces[i];
				//index++;
				//bottomRowProgress[i] = true;
				StartCoroutine (rotateBottomRowHelper(pieces[i]));
			}
		}
	}

	IEnumerator	rotateBottomRowHelper (GameObject piece) {
		//rotatingBottomRow = true;
		//float startX = piece.transform.position.x;
		//float startZ = piece.transform.position.z;
		//do {
		//for (int i = 0; i < 103; i++) {
		float start = piece.transform.rotation.eulerAngles.y;
		piece.transform.RotateAround (Vector3.zero, Vector3.up, Time.deltaTime * 50f);
		//}
			//} while ((Mathf.Abs (piece.transform.position.x - startX) > 0.01) &&
		         //(Mathf.Abs (piece.transform.position.z - startZ) > 0.01));
		//calibratePiece (piece);
		//rotatingBottomRow = false;
		//bottomRowProgress[i] = false;
		yield return null;
	}

	void calibratePiece(GameObject piece) {
		float x = 0f;
		float y = 0f;
		float z = 0f;

		/* x */
		if (Mathf.Abs (piece.transform.position.x - (-1.1f)) <= 0.01f) {
			x = -1.1f;
		} else if (Mathf.Abs (piece.transform.position.x - 1.1f) <= 0.01f) {
			x = 1.1f;
		} else if (Mathf.Abs (piece.transform.position.x) <= 0.01f) {
			x = 0f;
		}

		/* y */
		if (Mathf.Abs (piece.transform.position.y) <= 0.01f) {
			y = 0f;
		} else if (Mathf.Abs (piece.transform.position.y - 1.1f) <= 0.01f) {
			y = 1.1f;
		} else if (Mathf.Abs (piece.transform.position.y - 2.2f) <= 0.01f) {
			y = 2.2f;
		}

		/* z */
		if (Mathf.Abs (piece.transform.position.z - (-1.1f)) <= 0.01f) {
			z = -1.1f;
		} else if (Mathf.Abs (piece.transform.position.z - 1.1f) <= 0.01f) {
			z = 1.1f;
		} else if (Mathf.Abs (piece.transform.position.z) <= 0.01f) {
			z = 0f;
		}

		piece.transform.position = new Vector3 (x, y, z);
			
	}
	/*	bottomRowEndRot = Quaternion.Euler (bottomRow.transform.eulerAngles + new Vector3 (0, 90, 0));
		//for (float f = 0f; f < 1f; f += Time.deltaTime / 2f) {
			//bottomRow.transform.rotation = Quaternion.Lerp (start, end, 0.1f);
			//yield return null;
		//}

		float totalRot = 0f;
		float rotEnd = 90f;
		while (Mathf.Abs (totalRot) < Mathf.Abs (rotEnd)) {
			//bottomRow.transform.rotation =
			//	Quaternion.AngleAxis (bottomRow.transform.eulerAngles.y + (Time.deltaTime/5), Vector3.up);
			//bottomRow.transform.rotation = Quaternion.Lerp (start, end, 0.1f);
			totalRot += Time.deltaTime/5;
		}
		yield return null;
	}*/
}
