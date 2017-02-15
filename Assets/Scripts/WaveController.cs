using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
    public float magnitude;
    public float acceleration;
    public float decelaration;

    float min_velocity;
    float max_velocity;
    float current_velocity;

    int direction; // 1 means up, -1 means down
    int frame_count; // wave will only accelerate/decelerate every 5 frames
    float distance_traveled;
    float time;

    // Use this for initialization
    void Start() {
        min_velocity = 0.0025f;
        max_velocity = 0.0075f;
        
        // If rising, wave will go until hits the distance_traveled >= magnitude
        // If going down, wave will go until hits distance_traveled <= 0;
        current_velocity = min_velocity;
        direction = 1;
        distance_traveled = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        time = Time.deltaTime;
        frame_count++;

        CheckDirection();

        // the distance the wave will travel
        var distance = (current_velocity * time) * direction;
        var new_y = distance_traveled + distance;

        gameObject.transform.position = new Vector3(0.0f, new_y, 0.0f);
        distance_traveled += distance;

        if (frame_count % 5 == 0)
        {
            var potential_new_velocity = current_velocity;
            if (direction > 0) // the wave is rising
            {
                if (distance_traveled < magnitude / 2)
                {
                    potential_new_velocity *= acceleration;
                }
                else
                {
                    potential_new_velocity *= decelaration;
                }
            }
            else // the wave is going down
            {
                if (distance_traveled > magnitude / 2)
                {
                    potential_new_velocity *= acceleration;
                }
                else
                {
                    potential_new_velocity *= decelaration;
                }
            }
            if (potential_new_velocity < max_velocity && potential_new_velocity > min_velocity)
            {
                current_velocity = potential_new_velocity;
            }
        }
    }

    void CheckDirection()
    {
        if (distance_traveled >= magnitude)
        {
            direction = -1;
        }
        else if (distance_traveled <= 0)
        {
            direction = 1;
        }
    }
}
