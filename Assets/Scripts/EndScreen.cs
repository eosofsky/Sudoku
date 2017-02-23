using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

	public AnimationClip fadeColorAnimationClip;

	//[HideInInspector] public Animator animColorFade;

	void Awake () {
		Text text = GetComponentInChildren<Text> ();
		text.text = "Apartments Filled: " + ScoreManager.score;
		if (CursorManager.instance) {
			CursorManager.instance.SetIdle ();
		}
	}

	public void LoadDelayedRestart()
	{
		SceneManager.LoadScene ("Scene1");
	}

	public void Restart () {
		//Invoke ("LoadDelayedRestart", fadeColorAnimationClip.length * .5f);
		//animColorFade.SetTrigger ("fade");
		if (CursorManager.instance) {
			CursorManager.instance.ClickButtonInMenu ();
		}

		Application.LoadLevel("Scene1");
	}

	public void ReturnToMenu () {
		if (CursorManager.instance) {
			CursorManager.instance.ClickButtonInMenu ();
		}

		Application.LoadLevel("menu_Screen");
	}
}
