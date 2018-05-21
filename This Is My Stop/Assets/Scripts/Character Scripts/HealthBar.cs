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
        //Calculates character's current health to be later calculated by the shader
        playerHealthVal = 1 - ((float)playerControls.health / 500);
        HandleBar();
    }

    public void HandleBar()
    {
        if(renderer == null)
        {
            renderer = GetComponent<Renderer>();
        }
        //Sets the shader material cutoff dependant on the health value
        renderer.material.SetFloat("_Cutoff", playerHealthVal);
    }
}
