using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject cube;
	public Transform spawnPoint;

	private Cube currentCubeScript = null;
	private Cube oldCubeScript = null;

	void Start () {
		SpawnCube ();
	}

	void Update () {
		if (Input.GetKeyDown ("space")) {
			Vector3 newPos = spawnPoint.transform.position;
			newPos.y += 3.3f;
			spawnPoint.position = newPos;
			SpawnCube ();
		}
		currentCubeScript.CheckWin ();
	}
	
	void SpawnCube () {
		if (oldCubeScript) {
			oldCubeScript.enabled = false;
		}
		cube = Instantiate (cube, spawnPoint.position, spawnPoint.rotation);
		currentCubeScript = cube.GetComponent <Cube> ();
		currentCubeScript.height = spawnPoint.position.y;
		currentCubeScript.enabled = true;
		oldCubeScript = currentCubeScript;
	}
}
