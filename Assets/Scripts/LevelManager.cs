using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject cube;
    public GameObject wave;
    public Transform spawnPoint;
    
    private Cube currentCubeScript = null;
	private GameObject oldCube = null;
	private ScoreManager scoreManager;
	private CameraManager cameraManager;

    private Vector3 easeUp;
    private float _timePassed; // total time passed
    private float _ebb; // the amount the wave has raised at this level
    private bool _haveWon; // check if the user has won before

    void Start()
    {
        _haveWon = false;
        _timePassed = 0.0f;

		scoreManager = GameObject.FindGameObjectWithTag ("Score_Manager").GetComponent<ScoreManager> ();
		cameraManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraManager> ();

		SpawnCube ();
        SpawnWave();
    }

	void Update () {
        _timePassed += Time.deltaTime;
        if (_timePassed > 10 && _haveWon)
        {
            if (wave.transform.position.y < easeUp.y)
            {
                RaiseWave(0.025f);
            }
            else
            {
                RaiseWave(0.015f);
                _timePassed = 0;
            }

            //check lose condition
            if (_ebb > 0.50f)
            {
                // we lose
				Application.LoadLevel ("Scene1");
            }
        }

		if (currentCubeScript.CheckWin ()) { /* Cube complete */
            _haveWon = true;
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

    void SpawnWave ()
    {
        var wavePosition = new Vector3(
            spawnPoint.position.x,
            spawnPoint.position.y - 0.7f,
            spawnPoint.position.z);
        wave = Instantiate(wave, wavePosition, spawnPoint.rotation);
    }

    void RaiseWave (float distance)
    {
        var newPosition = new Vector3(
            wave.transform.position.x,
            wave.transform.position.y + distance,
            wave.transform.position.z);

        wave.transform.position = newPosition;
        _ebb += distance;
    }
}
