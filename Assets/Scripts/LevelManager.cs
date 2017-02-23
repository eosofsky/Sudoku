using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject cube;
    public GameObject wave;
    public Transform spawnPoint;

	public static LevelManager instance;

    private Cube currentCubeScript = null;
	private GameObject oldCube = null;
	private ScoreManager scoreManager;
	private CameraManager cameraManager;
    public static GameObject currentWave;

    private Vector3 easeUp;
    private float _timePassed; // total time passed
    private float _ebb; // the amount the wave has raised at this level

    private bool _haveWon; // check if the user has won before
    private int _timesWon;
    private int _timesRaised;
    private float _deltaDistance;

    private float _lossHeight; // How high wave raises into cube to cause user loss

	void Awake () {
		instance = this;
	}

    void Start()
    {
        _haveWon = false;
        _timesRaised = 0;
        _timesWon = 0;
        _timePassed = 0.0f;
        _lossHeight = 1.70f;

		scoreManager = GameObject.FindGameObjectWithTag ("Score_Manager").GetComponent<ScoreManager> ();
		cameraManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraManager> ();

		SpawnCube ();
        SpawnWave();
    }

	void Update () {
        _timePassed += Time.deltaTime;
        if (_haveWon && _timesRaised < _timesWon)
        {
            if (currentWave.transform.position.y < easeUp.y)
            {
                RaiseWave(0.03f);
            }
            else
            {
                _timesRaised++;
            }
        }

        if (_timePassed >= 7.0f && _haveWon)
        {
            RaiseWave(0.01f);
            _deltaDistance += .01f;

            if (_deltaDistance > 0.05f)
            {
                _timePassed = 0.0f;
                _deltaDistance = 0.0f;
            }

            //check lose condition
            if (_ebb > (easeUp.y + _lossHeight))
            {
                // we lose
                Application.LoadLevel("End");
            }
        }

		if (currentCubeScript.CheckWin ()) { /* Cube complete */
            _haveWon = true;
            _timesWon++;
			/* Update score */
			scoreManager.UpdateScore ();

			/* Spawn new cube */
			Vector3 newPos = spawnPoint.transform.position;
			newPos.y += 3.3f;
			spawnPoint.position = newPos;
            newPos.y -= 0.7f;
            easeUp = newPos;

			SpawnCube ();

			/* Update camera */
			cameraManager.UpdateCamera ();
		}
	}
	
	void SpawnCube () {
		if (oldCube) {
			oldCube.tag = "Untagged";
			Cube oldCubeScript = oldCube.GetComponent<Cube> ();
			oldCubeScript.EndCube ();
			oldCubeScript.enabled = false;
		}
		Instantiate (cube, spawnPoint.position, spawnPoint.rotation);
		GameObject currentCube = GameObject.FindGameObjectWithTag("Current_Cube");
		currentCubeScript = currentCube.GetComponent <Cube> ();
		currentCubeScript.height = spawnPoint.position.y;
		currentCubeScript.enabled = true;
		oldCube = currentCube;
	}

	public void ClearCurrentCube () {
		if (currentCubeScript) {
			currentCubeScript.Clear ();
		}
	}

    void SpawnWave ()
    {
        var wavePosition = new Vector3(
            spawnPoint.position.x,
            spawnPoint.position.y - 0.7f,
            spawnPoint.position.z);
        currentWave = Instantiate(wave, wavePosition, spawnPoint.rotation);
    }

    void RaiseWave (float distance)
    {
        var newPosition = new Vector3(
            currentWave.transform.position.x,
            currentWave.transform.position.y + distance,
            currentWave.transform.position.z);

        currentWave.transform.position = newPosition;
        _ebb += distance;
    }
}
