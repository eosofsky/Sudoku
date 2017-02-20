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
    private Vector3 _originalPosition;
    private Vector3 _offset;
    private Quaternion _originalRotation;
    private float _distance;

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
        if (Input.GetButtonDown("Fire1"))
        {
            GrabAnimal();
        }

        if (Input.GetButton("Fire1") && _animalSelected != null)
        {
            DragAnimal();
        }

        // trigger when the user clicks the mouse
        if (Input.GetButtonUp("Fire1") && _animalSelected != null)
        {
            PlaceAnimal();
        }
    }

    void GrabAnimal()
    {
        _click = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = (1 << 9);
        if (Physics.Raycast(_click, out _clickHit, 500, layerMask))
        {
            Debug.Log("Hit!");
            if (_clickHit.transform.tag == "Giraffe")
            {
                _animalSelected = _clickHit.transform.gameObject;
                _originalPosition = _animalSelected.transform.position;
                _originalRotation = _animalSelected.transform.rotation;
                Distance();
            }

            if (_clickHit.transform.tag == "Gorilla")
            {
                _animalSelected = _clickHit.transform.gameObject;
                _originalPosition = _animalSelected.transform.position;
                _originalRotation = _animalSelected.transform.rotation;
                Distance();
            }

            if (_clickHit.transform.tag == "Puma")
            {
                _animalSelected = _clickHit.transform.gameObject;
                _originalPosition = _animalSelected.transform.position;
                _originalRotation = _animalSelected.transform.rotation;
                Distance();
            }
        }
    }

    void Distance()
    {
        var camPoint = Camera.main.transform.position;
        var animalPoint = _animalSelected.transform.position;

        _distance = Vector3.Distance(camPoint, animalPoint);
        var clickPosition = _click.GetPoint(_distance);

        _offset = clickPosition - animalPoint;
    }

    void DragAnimal()
    {
        _click = Camera.main.ScreenPointToRay(Input.mousePosition);
        var clickPosition = _click.GetPoint(_distance);
        var newPosition = clickPosition - _offset;

        int layerMask = (1 << 8);
        // See if ray from camera to user click hits something
        if (Physics.Raycast(_click, out _clickHit, 5, layerMask))
        {
            newPosition = _clickHit.transform.position - _offset;
            if (_animalSelected.tag.Equals("Giraffe") || _animalSelected.tag.Equals("Puma"))
            {
                var newRotation = new Quaternion(
                    _originalRotation.x,
                    _originalRotation.y,
                    _originalRotation.z,
                    _originalRotation.w);
                _animalSelected.transform.rotation = newRotation;
            }
        }

        _animalSelected.transform.position = newPosition;
    }

    void PlaceAnimal()
    {
        _click = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = (1 << 8);
        // See if ray from camera to user click hits something
        if (Physics.Raycast(_click, out _clickHit, 5, layerMask))
        {
            _animalSelected.transform.position = _originalPosition;
            _animalSelected.transform.rotation = _originalRotation;
            PlaySelectedAnimation();
        }
        else
        {
            _animalSelected.transform.position = _originalPosition;
            _animalSelected.transform.rotation = _originalRotation;
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

        if (_animalSelected != null)
        {
            if (_animalSelected.tag.Equals("Giraffe"))
            {
                click.PlayOneShot(click_note);
                _prevObject.GetComponent<SpriteRenderer>().sprite = _giraffe;
                _prevSprite = _giraffe;
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
            _distance = 0.0f;
            _offset = new Vector3(0.0f,0.0f,0.0f);
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