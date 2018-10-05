using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    protected static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    int currentInputMode;

	// Use this for initialization
	void Start () {
        if (_instance == null)
        {
            _instance = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (currentInputMode == 0) //Main Menu
        {

        }
        else if (currentInputMode == 1) //Preparation Phase
        {

        }
        else if (currentInputMode == 2) //Battle Phase
        {

        }
        else
        {
            Debug.Log("INPUT ERROR");
        }
	}



}
