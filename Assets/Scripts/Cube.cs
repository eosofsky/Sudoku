using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	public GameObject[] pieces;
	public float height = 0f;

	public Sprite unoccupied;
	public Sprite giraffe_closed;
	public Sprite gorilla_closed;
	public Sprite puma_closed;

	private LevelGenerator level_generator;
	private GameObject scaffolding;

	void Start () {
		level_generator = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelGenerator> ();
		scaffolding = GameObject.FindGameObjectWithTag ("Scaffolding");
		StartCube ();
	}

	void Update () {
		if (Input.GetKeyDown ("z")) {
			if (CursorManager.instance) {
				CursorManager.instance.SwipeLeft ();
			}
			rotateRow (0f, true);
		} else if (Input.GetKeyDown ("x")) {
			if (CursorManager.instance) {
				CursorManager.instance.SwipeRight ();
			}
			rotateRow (0f, false);
		} else if (Input.GetKeyDown ("a")) {
			if (CursorManager.instance) {
				CursorManager.instance.SwipeLeft ();
			}
			rotateRow (1.1f, true);
		} else if (Input.GetKeyDown ("s")) {
			if (CursorManager.instance) {
				CursorManager.instance.SwipeRight ();
			}
			rotateRow (1.1f, false);
		} else if (Input.GetKeyDown ("q")) {
			if (CursorManager.instance) {
				CursorManager.instance.SwipeLeft ();
			}
			StartCoroutine (rotateScaffolding (true));
			rotateRow (2.2f, true);
		} else if (Input.GetKeyDown ("w")) {
			if (CursorManager.instance) {
				CursorManager.instance.SwipeRight ();
			}
			StartCoroutine (rotateScaffolding (false));
			rotateRow (2.2f, false);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			RotateCube(false);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			RotateCube(true);
		}
	}

	private void Randomize (int num_r) {
		for (int i = 0; i < num_r; i++) {
			/* Choose a row randomly */
			int row = 3;
			while (row == 3) {
				row = (int)(Random.value * 3.0f);
			}

			/* Choose a direction */
			int left = 2;
			while (left == 2) {
				left = (int)(Random.value * 2.0f);
			}
			bool rot_left = (left == 1);

			if (row == 0) {
				rotateRow (0f, rot_left);
			} else if (row == 1) {
				rotateRow (1.1f, rot_left);
			} else if (row == 2) {
				StartCoroutine (rotateScaffolding (rot_left));
				rotateRow (2.2f, rot_left);
			}
		}
	}

	private void RotateCube(bool left) {
		if (left && CursorManager.instance) {
			CursorManager.instance.SwipeLeft ();
		} else if (CursorManager.instance) {
			CursorManager.instance.SwipeRight ();
		}
		rotateRow (0.0f, left);
		rotateRow (1.1f, left);
		StartCoroutine (rotateScaffolding (left));
		rotateRow (2.2f, left);
	}

	void rotateRow (float y, bool left) {
		for (int i = 0; i < 26; i++) {
			if (Mathf.Abs(pieces[i].transform.position.y  - (height + y)) <= 0.1) {
				StartCoroutine (rotate (pieces[i], left));
			}
		}
	}

	IEnumerator rotateScaffolding (bool left) {
		int amount = 10;
		Vector3 dir = left ? Vector3.up : -Vector3.up;
		Vector3 point = Vector3.zero;
		point.y += height;
		for (int i = 0; i < 90 / amount; i++) {
			if (scaffolding) {
				scaffolding.transform.RotateAround (point, dir, amount);
			}
			yield return new WaitForSeconds (0.05f);
		}
		yield return null;
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
			if (Mathf.Abs(plane[i].transform.position.y  - (0f + height)) <= 0.1f) {
				face [bottom_index] = plane [i];
				bottom_index++;
			} else if (Mathf.Abs(plane[i].transform.position.y  - (1.1f + height)) <= 0.1f) {
				face [middle_index] = plane [i];
				middle_index++;
			} else if (Mathf.Abs(plane[i].transform.position.y  - (2.2f + height)) <= 0.1f) {
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
				float val;
				if (front_or_back) {
					val = face [i].transform.position.z;
				} else {
					val = face [i].transform.position.x;
				}
				float min = -1.1f;
				float mid = 0f;
				float max = 1.1f;
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
			bool hasGiraffe = false;
			bool hasGorilla = false;
			bool hasTiger = false;
			for (int c = 0; c < 3; c++) {
				//Debug.Log ("row: " + r + " col: " + c + " " +face [r * 3 + c].transform.position);
				string animal = face [r * 3 + c].GetComponent<SpriteRenderer> ().sprite.name;
				//Debug.Log ("row: " + r + "col: " + c + "animal: " + animal);
				if (animal.Contains("giraffe")) {
					hasGiraffe = true;
				} else if (animal.Contains("gorilla")) {
					hasGorilla = true;
				} else if (animal.Contains("puma")) {
					hasTiger = true;
				}
			}
			if (!(hasGiraffe && hasGorilla && hasTiger)) {
				return false;
			}
		}

		/* Check cols */
		for (int c = 0; c < 3; c++) {
			bool hasGiraffe = false;
			bool hasGorilla = false;
			bool hasTiger = false;
			for (int r = 0; r < 3; r++) {
				string animal = face [r * 3 + c].GetComponent<SpriteRenderer> ().sprite.name;
				//Debug.Log ("row: " + r + "col: " + c + "animal: " + animal);
				if (animal.Contains("giraffe")) {
					hasGiraffe = true;
				} else if (animal.Contains("gorilla")) {
					hasGorilla = true;
				} else if (animal.Contains("puma")) {
					hasTiger = true;
				}
			}
			if (!(hasGiraffe && hasGorilla && hasTiger)) {
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

		bool ans = CheckPlane (front, true) && CheckPlane (right, false) &&
		    CheckPlane(back, true) & CheckPlane(left, false);
		//Debug.Log (ans);

		return ans;
	}

	private void CloseShutters (GameObject plane) {
		/* Make plane non-clickable */
		//plane.layer = 0;
		plane.tag = "Plane";

		/* Close the shutters */
		SpriteRenderer renderer = plane.GetComponent<SpriteRenderer> ();
		if (renderer.sprite.name.Contains ("giraffe")) {
			renderer.sprite = giraffe_closed;
		} else if (renderer.sprite.name.Contains ("gorilla")) {
			renderer.sprite = gorilla_closed;
		} else if (renderer.sprite.name.Contains ("puma")) {
			renderer.sprite = puma_closed;
		}
	}

	private void MaybeLock (GameObject plane) {
		SpriteRenderer renderer = plane.GetComponent<SpriteRenderer> ();
		if ((renderer.sprite.name.Contains ("giraffe")) ||
			(renderer.sprite.name.Contains ("gorilla")) ||
			(renderer.sprite.name.Contains ("puma"))) {
			plane.layer = LayerMask.NameToLayer("Default");
		}
	}

	private void ClearFace(GameObject[] face) {
		for (int i = 0; i < face.Length; i++) {
			SpriteRenderer renderer = face[i].GetComponent<SpriteRenderer> ();
			if (!renderer.sprite.name.Contains ("occupied")) {
				renderer.sprite = unoccupied;
			}
		}
	}

	public void Clear () {
		GameObject[] front = GameObject.FindGameObjectsWithTag ("Plane_A");
		GameObject[] right = GameObject.FindGameObjectsWithTag ("Plane_B");
		GameObject[] back = GameObject.FindGameObjectsWithTag ("Plane_C");
		GameObject[] left = GameObject.FindGameObjectsWithTag ("Plane_D");

		ClearFace (front);
		ClearFace (right);
		ClearFace (back);
		ClearFace (left);
	}

	private void AssignSprite(GameObject plane, int id) {
		SpriteRenderer renderer = plane.GetComponent<SpriteRenderer> ();
		if (id == -1) {
			renderer.sprite = unoccupied;
		} else if (id == 1) {
			renderer.sprite = giraffe_closed;
		} else if (id == 2) {
			renderer.sprite = puma_closed;
		} else {
			renderer.sprite = gorilla_closed;
		}
	}

	private void AssignPuzzleToFace (int minRemove, int maxRemove, GameObject[] plane, bool front_or_back) {
		int[] level = level_generator.GetLevel (minRemove, maxRemove);
		GameObject[] face = MakeFace (plane, front_or_back);
		for (int i = 0; i < 9; i++) {
			AssignSprite (face [i], level [i]);
		}
	}

	public void StartCube () {
		GameObject[] front = GameObject.FindGameObjectsWithTag ("Plane_A");
		GameObject[] right = GameObject.FindGameObjectsWithTag ("Plane_B");
		GameObject[] back = GameObject.FindGameObjectsWithTag ("Plane_C");
		GameObject[] left = GameObject.FindGameObjectsWithTag ("Plane_D");

		/* Assign a random puzzle */
		int level = LevelManager.instance.level;
		int minRemove;
		int maxRemove;
		int numRot;
		if (level == 1) { /* level 1 */
			minRemove = 1;
			maxRemove = 3;
			numRot = 0;
		} else if (level < 4) { /* level 2 & 3 */
			minRemove = 2;
			maxRemove = 4;
			/* Between 2 and 5 rotations */
			numRot = (int)(Random.value * 3.0f) + 2;

		} else { /* All other levels */
			minRemove = 3;
			maxRemove = 5;
			/* Between 5 and 10 rotations */
			numRot = (int)(Random.value * 5.0f) + 5;
		}
		AssignPuzzleToFace (minRemove, maxRemove, front, true);
		AssignPuzzleToFace (minRemove, maxRemove, right, false);
		AssignPuzzleToFace (minRemove, maxRemove, back, true);
		AssignPuzzleToFace (minRemove, maxRemove, left, false);

		for (int i = 0; i < 9; i++) {
			MaybeLock (front [i]);
			MaybeLock (right [i]);
			MaybeLock (back [i]);
			MaybeLock (left [i]);
		}
		Randomize (numRot);
	}

	public void EndCube () {
		GameObject[] front = GameObject.FindGameObjectsWithTag ("Plane_A");
		GameObject[] right = GameObject.FindGameObjectsWithTag ("Plane_B");
		GameObject[] back = GameObject.FindGameObjectsWithTag ("Plane_C");
		GameObject[] left = GameObject.FindGameObjectsWithTag ("Plane_D");
		for (int i = 0; i < 9; i++) {
			CloseShutters (front[i]);
			CloseShutters (right[i]);
			CloseShutters (back[i]);
			CloseShutters (left[i]);
		}
		Planes planesScript = gameObject.GetComponent<Planes> ();
		planesScript.ResetPrevObject ();
	}
}
