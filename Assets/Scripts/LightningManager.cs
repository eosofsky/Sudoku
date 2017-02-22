using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour {

	public Light light;
	public float minInterval;
	public float threshold;

	private float lastTime = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastTime > minInterval) {
			if (Random.value > threshold) {
				light.enabled = true;
				light.intensity = 0.5f + (Random.value / 5.0f);
			} else {
				light.enabled = false;
				lastTime = Time.time;
			}
		}

	}
}
