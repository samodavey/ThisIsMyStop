using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private Renderer renderer;

    public PlayerControlsSO playerControls;

    private float playerHealthVal;

    // Use this for initialization
    void Start () {
        renderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        playerHealthVal = 1 - ((float)playerControls.health / 500);
        //Debug.Log(value);
        HandleBar();
    }

    public void HandleBar()
    {
        if(renderer == null)
        {
            renderer = GetComponent<Renderer>();
        }
        renderer.material.SetFloat("_Cutoff", playerHealthVal);
    }
}
