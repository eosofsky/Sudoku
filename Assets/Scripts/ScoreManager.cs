using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	private Text counterText;
	public static int score;
    public GameObject[] towers;
    public GameObject waveSprite;

    private string _scaffoldText = "Scaffold_{0}";
    private string _floorText = "Floor_{0}";

    private float wavePos;

    private GameObject scaffoldToHide;
    private GameObject scaffoldToShow;
    private GameObject floorToShow;

    void Start () {
        score = 0;
        wavePos = waveSprite.transform.position.y;
	}

    public void Update()
    {
        if (score > 0)
        {
            waveSprite.transform.position = new Vector3(
                waveSprite.transform.position.x,
                waveSprite.transform.position.y + 0.033f);
        }
    }

    public void UpdateScore() {
        score += 1;
        SetWaveSprite();
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
        else
        {
            // YOU WIN!!!!!!!!
        }
    }

    void SetWaveSprite()
    {
        waveSprite.transform.position = new Vector3 (
            waveSprite.transform.position.x,
            wavePos + (110.0f * score));
    }
}
