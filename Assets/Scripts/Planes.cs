﻿using System;
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
			if (_prevObject != null && !_prevObject.CompareTag("Plane"))
            {
                click.PlayOneShot(click_note);
                
                _prevObject.GetComponent<SpriteRenderer>().sprite = _giraffe;
                _prevSprite = _giraffe;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
			if (_prevObject != null && !_prevObject.CompareTag("Plane"))
            {
                click.PlayOneShot(click_note);
                
                _prevObject.GetComponent<SpriteRenderer>().sprite = _puma;
                _prevSprite = _puma;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
			if (_prevObject != null && !_prevObject.CompareTag("Plane"))
            {
                click.PlayOneShot(click_note);
                
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