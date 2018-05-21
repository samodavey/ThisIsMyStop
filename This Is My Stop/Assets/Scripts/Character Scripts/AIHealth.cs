using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour {

    private Renderer renderer;

    public AIControlsSO AIControls;

    private float aiHealthVal;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculates character's current health to be later calculated by the shader
        aiHealthVal = 1 - ((float)AIControls.health / 250);
        HandleBar();
    }

    public void HandleBar()
    {
        if (renderer == null)
        {
            renderer = GetComponent<Renderer>();
        }
        //Sets the shader material cutoff dependant on the health value
        renderer.material.SetFloat("_Cutoff", aiHealthVal);
    }
}
