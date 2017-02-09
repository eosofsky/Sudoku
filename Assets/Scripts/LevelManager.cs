using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject cube;
    public Transform spawnPoint;
    
    private Cube currentCubeScript = null;
    private Cube oldCubeScript = null;

    void Start()
    {
        SpawnCube();
    }

	void Update () {
		if (Input.GetKeyDown ("space")) {
			Vector3 newPos = spawnPoint.transform.position;
			newPos.y += 3.3f;
			spawnPoint.position = newPos;
			SpawnCube ();
		}
		//currentCubeScript.CheckWin ();
	}
	
	void SpawnCube () {
		if (oldCubeScript) {
			oldCubeScript.EndCube ();
			oldCubeScript.enabled = false;
		}
		GameObject currentCube = Instantiate (cube, spawnPoint.position, spawnPoint.rotation);
		currentCubeScript = currentCube.GetComponent <Cube> ();
		currentCubeScript.height = spawnPoint.position.y;
		currentCubeScript.enabled = true;
		oldCubeScript = currentCubeScript;
	}
}
