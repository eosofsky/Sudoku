using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareClick : MonoBehaviour {
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    int facemask;                       // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    Renderer rend;      // the renderer of our plane
    Material start_mat; // the starting material of our plane
    Material default_mat;
    int click_count;

    private void Start()
    {
        click_count = 0;
        rend = GetComponent<Renderer>();
        start_mat = rend.material;

        /*
         *  Creating a white material as the default material color. 
         */
        Material material = new Material(Shader.Find("Transparent/Diffuse"));
        material.color = Color.white;
        default_mat = material;
    }

    void OnMouseDown()
    {
        click_count++;
        Debug.LogFormat("clicked : {0} times", click_count);

        rend.material = default_mat;

       
    }
}

/**
 *   // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit faceHit; // which face of the cube did the player select?

        // Perform the raycast and if it hits something on the face [cube] layer...
        if (Physics.Raycast(camRay, out faceHit, camRayLength, facemask))
        {
            // Find the object that the player clicked on
            // faceHit.point

        }
 */
