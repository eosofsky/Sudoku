using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planes : MonoBehaviour {
    Ray _click;
    RaycastHit _clickHit;

    public GameObject _wave;
    // Animal Clipping spaces
    public GameObject _giraffeTrans;
    public GameObject _gorillaTrans;
    public GameObject _pumaTrans;
    // Room Sprites (input by user)
    public Sprite _giraffe;
    public Sprite _gorilla;
    public Sprite _puma;
    public Sprite _room;

    private GameObject _animalSelected = null;
    private Vector3 _offset;
    private Quaternion _originalRotation;
    private bool _rotatedOnce;
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
        if (_giraffeTrans && _gorillaTrans && _pumaTrans)
        {
            ChangeTrans();
        }

        if (Input.GetButtonDown("Fire1"))
        {
			if (CursorManager.instance) {
				CursorManager.instance.Grab ();
			}
            GrabAnimal();
        }

        if (Input.GetButton("Fire1") && _animalSelected != null)
        {
            DragAnimal();
        }
    }

    private void LateUpdate()
    {
        // trigger when the user clicks the mouse
        if (Input.GetButtonUp("Fire1") && _animalSelected != null)
        {
			if (CursorManager.instance) {
				CursorManager.instance.LetGo ();
			}
            PlaceAnimal();
        }
    }

    void GrabAnimal()
    {
        _click = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = (1 << 9);
        if (Physics.Raycast(_click, out _clickHit, 500, layerMask))
        {
            _animalSelected = _clickHit.transform.gameObject;
            _originalRotation = _animalSelected.transform.rotation;
            Distance();
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
            var rotation_offset = new Vector3(0.0f, 0.3f, 0.75f);
            if (_animalSelected.tag.Equals("Gorilla"))
            {
                rotation_offset = new Vector3(-0.3f, 0.3f, 0.75f);
            }
            else if (_animalSelected.tag.Equals("Puma"))
            {
                rotation_offset = new Vector3(0.25f, 0.3f, 0.80f);
            }

            if ((_animalSelected.tag.Equals("Giraffe") || _animalSelected.tag.Equals("Puma") ||
                _animalSelected.tag.Equals("Gorilla")) && !_rotatedOnce)
            {
                newPosition = _clickHit.transform.position - rotation_offset;
                _animalSelected.transform.position = newPosition;
                _animalSelected.transform.Rotate(0.0f, 0.0f, 65.0f);
                _rotatedOnce = true;
            }
            else
            {
                newPosition = _clickHit.transform.position - rotation_offset;
                _animalSelected.transform.position = newPosition;
            }
        }
        else
        {
            _animalSelected.transform.position = newPosition;
        }
    }

    void PlaceAnimal()
    {
        RestorePosition();
        _click = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = (1 << 8);
        // See if ray from camera to user click hits something
        if (Physics.Raycast(_click, out _clickHit, 5, layerMask))
        {
            PlaySelectedAnimation();
        }
    }

    void RestorePosition()
    {
        if (_animalSelected.tag.Equals("Giraffe"))
        {
            _animalSelected.transform.position = _giraffeTrans.transform.position;
            _animalSelected.transform.rotation = _originalRotation;
            _rotatedOnce = false;
        }
        else if (_animalSelected.tag.Equals("Gorilla"))
        {
            _animalSelected.transform.position = _gorillaTrans.transform.position;
            _animalSelected.transform.rotation = _originalRotation;
            _rotatedOnce = false;
        }
        else if (_animalSelected.tag.Equals("Puma"))
        {
            _animalSelected.transform.position = _pumaTrans.transform.position;
            _animalSelected.transform.rotation = _originalRotation;
            _rotatedOnce = false;
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

    // Updates each trans to the correct y position
    public void ChangeTrans()
    {
        var y = _wave.transform.position.y;

        _giraffeTrans.transform.position = new Vector3(
            _giraffeTrans.transform.position.x,
            y,
            _giraffeTrans.transform.position.z);
        _gorillaTrans.transform.position = new Vector3(
            _gorillaTrans.transform.position.x,
            y,
            _gorillaTrans.transform.position.z);
        _pumaTrans.transform.position = new Vector3(
            _pumaTrans.transform.position.x,
            y,
            _pumaTrans.transform.position.z);
    }

    /*
     *  Restore the previously selected plane to its original state
     */ 
    public void RestorePreviousState()
    {
        // currently does nothing?
    }
}