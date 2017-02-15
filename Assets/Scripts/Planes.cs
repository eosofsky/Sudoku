using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planes : MonoBehaviour {
    Ray _click;
    RaycastHit _clickHit;

    // Base Materials (will be replaced by Sprites)
    public Sprite _giraffe;
    public Sprite _gorilla;
    public Sprite _puma;
    public Sprite _room;

    /*
    public Button redButton;
    public Button greenButton;
    public Button yellowButton;
    */

    // Information for the previously clicked object
    GameObject _prevObject;
    Sprite _prevSprite;

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

    // Update is called once per frame
    void Update () {        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
			if (_prevObject != null && !_prevObject.CompareTag("Plane"))
            {
                click.PlayOneShot(click_note);
                
                //Destroy(_prevObject.GetComponent<SpriteRenderer>().sprite);
                _prevObject.GetComponent<SpriteRenderer>().sprite = _giraffe;
                _prevSprite = _giraffe;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
			if (_prevObject != null && !_prevObject.CompareTag("Plane"))
            {
                click.PlayOneShot(click_note);

                //Destroy(_prevObject.GetComponent<SpriteRenderer>().sprite);
                _prevObject.GetComponent<SpriteRenderer>().sprite = _puma;
                _prevSprite = _puma;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
			if (_prevObject != null && !_prevObject.CompareTag("Plane"))
            {
                click.PlayOneShot(click_note);

                //Destroy(_prevObject.GetComponent<SpriteRenderer>().sprite);
                _prevObject.GetComponent<SpriteRenderer>().sprite = _gorilla;
                _prevSprite = _gorilla;
            }
        }

        // trigger when the user clicks the mouse
        if (Input.GetButtonUp("Fire1"))
        {
            _click = Camera.main.ScreenPointToRay(Input.mousePosition);

			int layerMask = (1 << 8);
            // See if ray from camera to user click hits something
            if (Physics.Raycast(_click, out _clickHit, 5, layerMask))
            {
                PlaySelectedAnimation();
            }
        }
    }

	public void ResetPrevObject () {
		_prevObject = null;
	}

    void PlaySelectedAnimation()
    {
        var currentObject = _clickHit.transform.gameObject;

		if (currentObject.CompareTag ("Plane")) {
			return;
		}

        if (_prevObject != null)
        {
            if (currentObject.Equals(_prevObject))
            {
                // Deselect the current selected plane
                RestorePreviousState();
            }
            else
            {
                // Deselect the last selected plane
                RestorePreviousState();

                // The current plane is now the _prevObject 
                _prevObject = currentObject;
                _prevSprite = currentObject.GetComponent<SpriteRenderer>().sprite;
            }
        }
        else
        {
            // The current plane is now the previously selected one
            _prevObject = currentObject;
            _prevSprite = _prevObject.GetComponent<SpriteRenderer>().sprite;
        }
    }

    /*
     *  Restore the previously selected plane to its original state
     */ 
    public void RestorePreviousState()
    {
        // currently does nothing?
    }
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
