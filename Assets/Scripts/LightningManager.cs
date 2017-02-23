using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour {

	public Light light;
	public float minInterval;
	public float threshold;

	public AudioSource thunder_bg;
	public AudioSource thunder_1;
	public AudioSource thunder_2;
	public AudioSource thunder_3;
	public AudioSource thunder_4;

	private float lastTime = 0;
	private AudioSource[] thunderSounds;
	private bool lightningActive;

	// Use this for initialization
	void Awake () {
		
		thunderSounds = new AudioSource[] {
			thunder_1,
			thunder_2,
			thunder_3,
			thunder_4
		};

		for (int i = 0; i < thunderSounds.Length; i++) {
			thunderSounds[i].rolloffMode = AudioRolloffMode.Linear;
		}
	}

	void Start() {
		light.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		float currentTime = Time.time;
		// wait at least 10 seconds before starting lightning
		if (currentTime > 10 && currentTime - lastTime > minInterval && !lightningActive) {
			if (Random.value > threshold) {
				StartCoroutine (LightningSequence ());
				thunderSounds [Random.Range (0, 3)].Play (); // play a random thunder clip
				StartCoroutine(Delay(Random.value / 2 + 0.5f));
			} else {
				light.enabled = false;
				lastTime = Time.time;
			}
		}

	}

	IEnumerator Delay(float time) {
		lightningActive = true;
		yield return new WaitForSeconds(time);
		lightningActive = false;
	}

	IEnumerator LightningSequence() {
		int iterations = Random.Range (2, 4);

		for (int i = 0; i <= iterations; i++) {
			light.intensity = 0.5f + (Random.value / 2.0f);
			light.enabled = true;
			yield return new WaitForSeconds(Random.value / 8.0f);
			light.enabled = false;
			yield return new WaitForSeconds(Random.value / 6.0f);
		}
			
	}

}
