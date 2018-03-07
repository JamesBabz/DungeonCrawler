using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    private bool isPressed;

	// Use this for initialization
	void Start ()
	{
	    isPressed = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("pressed");
        isPressed = true;
    }

    public bool GetIsPressed()
    {
        return isPressed;
    }
}
