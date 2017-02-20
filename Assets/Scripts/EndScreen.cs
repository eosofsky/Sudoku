using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

	public AnimationClip fadeColorAnimationClip;

	//[HideInInspector] public Animator animColorFade;

	void Start () {
		Text text = GetComponentInChildren<Text> ();
		text.text = "Apartments Filled: " + ScoreManager.score;
	}

	public void LoadDelayedRestart()
	{
		SceneManager.LoadScene ("Scene1");
	}

	public void Restart () {
		//Invoke ("LoadDelayedRestart", fadeColorAnimationClip.length * .5f);
		//animColorFade.SetTrigger ("fade");
		Application.LoadLevel("Scene1");
	}

	public void ReturnToMenu () {
		Application.LoadLevel("menu_Screen");
	}
}
