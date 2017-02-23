using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearButton : MonoBehaviour {

	public void MousedOver () {
		if (CursorManager.instance) {
			CursorManager.instance.OverButtonInGame ();
		}
	}

	public void MousedOff () {
		if (CursorManager.instance) {
			CursorManager.instance.OffButtonInGame ();
		}
	}

	public void ClickButton () {
		if (CursorManager.instance) {
			CursorManager.instance.ClickButtonInGame ();
		}
		LevelManager.instance.ClearCurrentCube ();
	}
}
