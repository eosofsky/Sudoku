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

    private GameObject _animalSelected = null;

    // Information for the previously clicked object
    GameObject _prevObject;
    Sprite _prevSprite;

    public AudioClip click_note;
    AudioSource click;

    // Use this for initialization
    void Start ()
    {
        click = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);

        if (Input.GetButtonDown("Fire1"))
        {
            _click = Camera.main.ScreenPointToRay(Input.mousePosition);

            int layerMask = (1 << 9);
            if (Physics.Raycast(_click, out _clickHit, 500, layerMask))
            {
                Debug.Log("Hit!");
                if (_clickHit.transform.tag == "Giraffe")
                {
                    _animalSelected = _clickHit.transform.gameObject;

                    offset = _animalSelected.transform.position - _click.origin;
                }

                if (_clickHit.transform.tag == "Gorilla")
                {
                    _animalSelected = _clickHit.transform.gameObject;

                    offset = _animalSelected.transform.position - _click.origin;
                }

                if (_clickHit.transform.tag == "Puma")
                {
                    _animalSelected = _clickHit.transform.gameObject;

                    offset = _animalSelected.transform.position - _click.origin;
                }
                Debug.LogFormat("Animal Selected : {0}", _animalSelected);
            }
        }

        // trigger when the user clicks the mouse
        if (Input.GetButtonUp("Fire1") && _animalSelected != null)
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
            // Deselect the last selected plane
            RestorePreviousState();

            // The current plane is now the _prevObject 
            _prevObject = currentObject;
            _prevSprite = currentObject.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            // The current plane is now the previously selected one
            _prevObject = currentObject;
            _prevSprite = _prevObject.GetComponent<SpriteRenderer>().sprite;
        }

        Debug.LogFormat("Animal selected : {0}", _animalSelected);
        if (_animalSelected != null)
        {
            if (_animalSelected.tag.Equals("Giraffe"))
            {
                click.PlayOneShot(click_note);
                _prevObject.GetComponent<SpriteRenderer>().sprite = _giraffe;
                _prevSprite = _gorilla;
            }

            if (_animalSelected.tag.Equals("Gorilla"))
            {
                click.PlayOneShot(click_note);
                _prevObject.GetComponent<SpriteRenderer>().sprite = _gorilla;
                _prevSprite = _gorilla;
            }

            if (_animalSelected.tag.Equals("Puma"))
            {
                click.PlayOneShot(click_note);
                _prevObject.GetComponent<SpriteRenderer>().sprite = _puma;
                _prevSprite = _puma;
            }

            _animalSelected = null;
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