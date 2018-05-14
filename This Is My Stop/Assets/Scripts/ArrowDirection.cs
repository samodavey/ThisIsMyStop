using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour {

    private Transform exitTarget;

    private GameObject[] playerTarget;

    // Use this for initialization
    void Start () {

        //target = GameObject.Find()
        exitTarget = GameObject.FindGameObjectWithTag("Destination").transform;
        playerTarget = GameObject.FindGameObjectsWithTag("Hunted");



        Debug.Log(exitTarget.name);
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.parent.tag == "Hunted")
        {
            transform.LookAt(exitTarget);
        }
        else
        {
            foreach(GameObject assignedTarget in playerTarget)
            {
                if (assignedTarget.name.Contains("Player"))
                {
                    transform.LookAt(assignedTarget.transform);
                }
            }
            //LOOK AT DEPENDANT ON DISTANCE
        }
    }
}
