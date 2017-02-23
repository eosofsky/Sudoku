using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	public void MousedOver () {
		if (CursorManager.instance) {
			CursorManager.instance.OverButton ();
		}
	}

	public void MousedOff () {
		if (CursorManager.instance) {
			CursorManager.instance.OffButton ();
		}
	}

	public void ClickButton () {
		if (CursorManager.instance) {
			CursorManager.instance.ClickButton ();
		}
		LevelManager.instance.ClearCurrentCube ();
	}
}
