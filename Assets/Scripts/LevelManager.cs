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
    private int _frameCount;

    void Start()
    {
        _frameCount = 0;

		scoreManager = GameObject.FindGameObjectWithTag ("Score_Manager").GetComponent<ScoreManager> ();
		cameraManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraManager> ();

		SpawnCube ();
        SpawnWave();
    }

	void Update () {
        if (_frameCount % 20 == 0)
        {
            Debug.Log("Raising Wave");

            RaiseWave();
            //check lose condition
        }

		if (currentCubeScript.CheckWin ()) { /* Cube complete */
			/* Update score */
			scoreManager.UpdateScore ();

			/* Spawn new cube */
			Vector3 newPos = spawnPoint.transform.position;
			newPos.y += 3.3f;
			spawnPoint.position = newPos;
			SpawnCube ();

			/* Update camera */
			cameraManager.UpdateCamera ();
		}

        _frameCount++;
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
            spawnPoint.position.y - 0.5f,
            spawnPoint.position.z);
        wave = Instantiate(wave, wavePosition, spawnPoint.rotation);
    }

    void RaiseWave ()
    {
        var newPosition = new Vector3(
            wave.transform.position.x,
            wave.transform.position.y + 0.025f,
            wave.transform.position.z);

        wave.transform.position = newPosition;
    }
}
