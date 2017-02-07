using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject cube;
	public Transform spawnPoint;

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
	}
	
	void SpawnCube () {
		if (oldCubeScript) {
			oldCubeScript.enabled = false;
		}
		cube = Instantiate (cube, spawnPoint.position, spawnPoint.rotation);
		Cube cubeScript = cube.GetComponent <Cube> ();
		cubeScript.height = spawnPoint.position.y;
		cubeScript.enabled = true;
		oldCubeScript = cubeScript;
	}
}
