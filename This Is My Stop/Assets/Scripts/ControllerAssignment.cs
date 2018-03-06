using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAssignment : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i <= 4; i++)
        {
            if (Input.GetButton("Joy" + i + "_A"))
            {
                AddPlayerController(i);
            }
        }
	}

    private void AddPlayerController(int controller)
    {
        throw new NotImplementedException();
    }
}
