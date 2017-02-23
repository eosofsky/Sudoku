using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

	public Texture2D idle;
	public Texture2D[] grab;
	public Texture2D[] letGo;
	public Texture2D[] swipeLeft;
	public Texture2D[] swipeRight;
	public Texture2D[] overButton;
	public Texture2D[] offButton;
	public Texture2D[] click;

	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 idleHotSpot = new Vector2 (0, 0);
	public Vector2 gamePlayHotSpot = new Vector2 (100, 100);
	public float speed = 0.1f;

	public static CursorManager instance;

	void Awake () {
		DontDestroyOnLoad(this.gameObject);
		instance = this;
		SetIdle ();
	}

	public void SetIdle () {
		Cursor.SetCursor (idle, idleHotSpot, cursorMode);
	}

	public void SetOpen () {
		Cursor.SetCursor (letGo[letGo.Length - 1], gamePlayHotSpot, cursorMode);
	}

	public void Grab () {
		StartCoroutine (AnimateCursor(grab, gamePlayHotSpot));
	}

	public void LetGo () {
		StartCoroutine (AnimateCursor(letGo, gamePlayHotSpot));
	}

	public void SwipeLeft () {
		StartCoroutine (AnimateCursor(swipeLeft, gamePlayHotSpot));
	}

	public void SwipeRight () {
		StartCoroutine (AnimateCursor(swipeRight, gamePlayHotSpot));
	}

	public void OverButton () {
		StartCoroutine (AnimateCursor(overButton, gamePlayHotSpot));
	}

	public void OffButton () {
		StartCoroutine (AnimateCursor(offButton, gamePlayHotSpot));
	}

	public void ClickButton () {
		StartCoroutine (AnimateCursor(click, gamePlayHotSpot));
	}

	IEnumerator AnimateCursor (Texture2D[] animation, Vector2 hotSpot) {
		for (int i = 0; i < animation.Length; i++) {
			Cursor.SetCursor (animation [i], hotSpot, cursorMode);
			yield return new WaitForSeconds (speed);
		}
	}

}
