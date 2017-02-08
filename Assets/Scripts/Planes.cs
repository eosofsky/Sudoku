using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planes : MonoBehaviour {
    Ray _click;
    RaycastHit _clickHit;

    // Information for the previously clicked object
    Renderer _prevRenderer;
    Material _prevMaterial;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Jump")) {
            _prevRenderer = null;
            _prevMaterial = null;
        }
        // trigger when the user clicks the mouse
        if (Input.GetButtonUp("Fire1")) {
            _click = Camera.main.ScreenPointToRay(Input.mousePosition);

            // See if ray from camera to user click hits something
            if (Physics.Raycast(_click, out _clickHit))
            {
                if (_clickHit.transform.tag == "Plane")
                {
                    PlaySelectedAnimation();
                }
            }
        }
    }

    void PlaySelectedAnimation()
    {
        var currentRenderer = _clickHit.transform.GetComponent<Renderer>();
        Debug.LogFormat("Clicked Renderer {1}; Material : {0}", currentRenderer.material, currentRenderer);

        // For now, we will just highlight it white
        Material white = new Material(Shader.Find("Transparent/Diffuse"));
        white.color = Color.white;

        if (_prevRenderer != null)
        {
            if (currentRenderer.Equals(_prevRenderer))
            {
                // Deselect the current selected plane
                RestorePreviousState();
            }
            else
            {
                // Deselect the last selected plane
                RestorePreviousState();

                // The current plane is now the previously selected one
                _prevRenderer = currentRenderer;
                _prevMaterial = _prevRenderer.material;

                _prevRenderer.material = white;
            }
            Debug.LogFormat("Previous mat : {0}; Current mat : {1}", _prevMaterial, currentRenderer.material);
        }
        else
        {
            Debug.LogFormat("Initializing mat : {0}", currentRenderer.material);
            // The current plane is now the previously selected one
           _prevRenderer = currentRenderer;
            _prevMaterial = _prevRenderer.material;

            _prevRenderer.material = white;
        }
    }

    /*
     *  Restore the previously selected plane to its original state
     */ 
    void RestorePreviousState()
    {
        _prevRenderer.material = _prevMaterial;
    }
}

/*
 * 
 * 
            var clickOrigin = Camera.main.transform.position;         
            var clickDirect = Input.mousePosition;              // location of user click
 */
