using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	//void Update () {
	//	if (Input.GetKeyDown (KeyCode.P)) {
	//		int[] face = GetCompleteFace ();
	//		RemoveSome (face);
	//		for (int r = 0; r < 3; r++) {
	//			Debug.Log(face[r * 3] + " " + face[r * 3 + 1] + " " + face[r * 3 + 2]);
	//		}
	//	}
	//}

	public int[] GetLevel () {
		int[] level = GetCompleteFace ();
		RemoveSome (level);
		return level;
	}

	private int[] GetCompleteFace () {
		int[] face = new int[9];
		/* Initialize matrix */
		for (int r = 0; r < 3; r++) {
			for (int c = 0; c < 3; c++) {
				int index = r * 3 + c;
				face[index] = -1;
			}
		}

		/* Top Row */
		/* Place 1 */
		int index1_top = 3;
		while (index1_top == 3) { /* Between 0 and 2, inc. */
			index1_top = (int)(Random.value * 3.0f);
		}
		face[index1_top] = 1;
		/* Place 2 */
		int index2_top = 3;
		while (index2_top == 3 || index2_top == index1_top) {
			index2_top = (int)(Random.value * 3.0f);
		}
		face[index2_top] = 2;
		/* Place 3 */
		int index3_top = -1;
		for (int i = 0; i < 3; i++) {
			if (face[i] == -1) {
				face[i] = 3;
				index3_top = i;
				break;
			}
		}

		/* Middle Row */
		/* Place 1 */
		int index1_mid = 3;
		while (index1_mid == 3 || index1_mid == index1_top) {
			index1_mid = (int)(Random.value * 3.0f);
		}
		face[3 + index1_mid] = 1;
		/* Place 2 & 3 */
		int index2_mid, index3_mid;
		if (index2_top == index1_mid) {
			index2_mid = index3_top;
			index3_mid = index1_top;
		}
		else {
			index2_mid = index1_top;
			index3_mid = index2_top;
		}
		face[3 + index2_mid] = 2;
		face[3 + index3_mid] = 3;

		/* Bottom Row */
		for (int c = 0; c < 3; c++) {
			if (face[c] != 1 && face[3 + c] != 1) {
				face[6 + c] = 1;
			} else if (face[c] != 2 && face[3 + c] != 2) {
				face[6 + c] = 2;
			} else {
				face[6 + c] = 3;
			}
		}

		return face;
	}

	private void RemoveSome(int[] face) {
		/* Remove 3 or 4 pieces per face */
		int numRemove = 0;
		float rand = 0.5f;
		while (rand != 0.5f) {
			rand = Random.value;
		}
		if (rand > 0) {
			numRemove = 4;
		} else {
			numRemove = 5;
		}

		for(int i = 0; i < numRemove; i++) {
			int r = 3;
			while (r == 3) { /* Between 0 and 2, inc. */
				r = (int)(Random.value * 3.0f);
			} 

			int c = 3;
			while (c == 3) { /* Between 0 and 2, inc. */
				c = (int)(Random.value * 3.0f);
			}

			if (face[r * 3 + c] == -1) {
				i--;
			} else {
				face[r * 3 + c] = -1;
			}
		}
	}
}