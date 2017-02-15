using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	private Text counterText;
	private int score;

	void Start () {
		counterText = GetComponentInChildren <Text> ();
	}

	public void UpdateScore () {
		score+=36;
		counterText.text = "Apartments Filled: " + score;
	}
}
