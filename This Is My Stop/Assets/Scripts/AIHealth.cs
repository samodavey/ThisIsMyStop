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
        aiHealthVal = 1 - ((float)AIControls.health / 250);
        HandleBar();
    }

    public void HandleBar()
    {
        if (renderer == null)
        {
            renderer = GetComponent<Renderer>();
        }
        renderer.material.SetFloat("_Cutoff", aiHealthVal);
    }
}
