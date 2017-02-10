using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planes : MonoBehaviour {
    Ray _click;
    RaycastHit _clickHit;

    // Base Materials (will be replaced by Sprites)
    Material _red;
    Material _green;
    Material _yellow;
    
    /*
    public Button redButton;
    public Button greenButton;
    public Button yellowButton;
    */

    // Information for the previously clicked object
    Renderer _prevRenderer;
    Material _prevMaterial;

    public AudioClip click_note;
    AudioSource click;

    // Use this for initialization
    void Start ()
    {
        click = GetComponent<AudioSource>();
        /*
        Button button_red = redButton.GetComponent<Button>();
        button_red.onClick.AddListener(RedButtonClicked);
        greenButton.onClick.AddListener(GreenButtonClicked);
        yellowButton.onClick.AddListener(YellowButtonClicked);
        */
    }

    /*
     *  For debugging, it is red button; all buttons should be changed to respective animals
     *  in the future.
     *
    public void RedButtonClicked()
    {
        Debug.LogFormat("Is a renderer selected? {0}", _prevRenderer);
        if (_prevRenderer != null)
        {
            _prevRenderer.material = _red;
            _prevMaterial = _red;
        }
    }

    public void GreenButtonClicked()
    {
        Debug.LogFormat("Is a renderer selected? {0}", _prevRenderer);
        if (_prevRenderer != null)
        {
            _prevRenderer.material = _green;
            _prevMaterial = _green;
        }
    }

    public void YellowButtonClicked()
    {
        Debug.LogFormat("Is a renderer selected? {0}", _prevRenderer);
        if (_prevRenderer != null)
        {
            _prevRenderer.material = _yellow;
            _prevMaterial = _yellow;
        }
    }
    */

    // Update is called once per frame
    void Update () {
        Material giraffe = Resources.Load("giraffe_placed") as Material;
        Material puma = Resources.Load("puma_placed") as Material;
        Material gorilla = Resources.Load("gorilla_placed") as Material;
        
        if (Input.GetButtonDown("Jump")) {
            _prevRenderer = null;
            _prevMaterial = null;
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            click.PlayOneShot(click_note);

            if (_prevRenderer != null)
            {
                _prevRenderer.material = giraffe;
                _prevMaterial = giraffe;
            }
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            click.PlayOneShot(click_note);

            if (_prevRenderer != null)
            {
                _prevRenderer.material = puma;
                _prevMaterial = puma;
            }
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            click.PlayOneShot(click_note);

            if (_prevRenderer != null)
            {
                _prevRenderer.material = gorilla;
                _prevMaterial = gorilla;
            }
        }

        // trigger when the user clicks the mouse
        if (Input.GetButtonUp("Fire1"))
        {
            _click = Camera.main.ScreenPointToRay(Input.mousePosition);


            // See if ray from camera to user click hits something
            if (Physics.Raycast(_click, out _clickHit))
            {
				if ((_clickHit.transform.tag == "Plane_A") || (_clickHit.transform.tag == "Plane_B") ||
					(_clickHit.transform.tag == "Plane_C") || (_clickHit.transform.tag == "Plane_D"))
                {
                    PlaySelectedAnimation();
                }
            }
        }
    }

    void PlaySelectedAnimation()
    {
        var currentRenderer = _clickHit.transform.GetComponent<Renderer>();
        
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
        }
        else
        {
            // The current plane is now the previously selected one
            _prevRenderer = currentRenderer;
            _prevMaterial = _prevRenderer.material;

            _prevRenderer.material = white;
        }
    }

    /*
     *  Restore the previously selected plane to its original state
     */ 
    public void RestorePreviousState()
    {
        _prevRenderer.material = _prevMaterial;
    }
}