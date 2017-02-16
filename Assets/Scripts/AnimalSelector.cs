using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSelector : MonoBehaviour {

    [SerializeField]private string _current_animal;

    private const string _giraffe = "Giraffe";
    private const string _gorilla = "Gorilla";
    private const string _puma = "Puma";

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        // trigger when the user clicks the mouse
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("clicked");
            var click = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(click, out hit, 500))
            {
                Debug.Log("hit");
                var currentObject = hit.transform.gameObject;
                Debug.LogFormat("clickedObject {0}", currentObject);
                if (currentObject.tag.Equals(_giraffe))
                {
                    _current_animal = _giraffe;
                    Debug.Log("clicked giraffe");
                }
                else if (currentObject.tag.Equals(_gorilla))
                {
                    _current_animal = _gorilla;
                    Debug.Log("clicked gorilla");
                }
                else if (currentObject.tag.Equals(_puma))
                {
                    _current_animal = _puma;
                    Debug.Log("clicked puma");
                }
                else
                {
                    _current_animal = "NONE";
                }
            }
        }
    }
}
