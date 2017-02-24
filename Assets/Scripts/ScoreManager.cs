using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	private Text counterText;
	public static int score;
    public GameObject[] towers;

    private string _scaffoldText = "Scaffold_{0}";
    private string _floorText = "Floor_{0}";

    private GameObject scaffoldToHide;
    private GameObject scaffoldToShow;
    private GameObject floorToShow;

    void Start () {
        score = 0;
	}

    public void UpdateScore() {
        score += 1;
        if (score < 8) {
            foreach (var obj in towers)
            {
                // hide previous scaffolding
                if (obj.name.Equals(string.Format(_scaffoldText, score)))
                {
                    obj.SetActive(false);
                }
                // show next scaffolding
                else if (obj.name.Equals(string.Format(_scaffoldText, score + 1)))
                {
                    obj.SetActive(true);
                }
                // show completed floor
                else if (obj.name.Equals(string.Format(_floorText, score)))
                {
                    obj.SetActive(true);
                }
            }
        }
    }

}
