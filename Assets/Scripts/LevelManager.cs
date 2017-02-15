using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject cube;
    public Transform spawnPoint;
    
    private Cube currentCubeScript = null;
	private GameObject oldCube = null;
	private ScoreManager scoreManager;
	private CameraManager cameraManager;

    void Start()
    {
		scoreManager = GameObject.FindGameObjectWithTag ("Score_Manager").GetComponent<ScoreManager> ();
		cameraManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraManager> ();

		SpawnCube ();
    }

	void Update () {
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
}
