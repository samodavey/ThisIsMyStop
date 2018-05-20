using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour {

    private Transform exitTarget;

    private GameObject[] playerTarget;

    private Renderer rend;

    // Use this for initialization
    void Start () {

        rend = GetComponent<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {

        exitTarget = GameObject.FindGameObjectWithTag("Destination").transform;
        playerTarget = GameObject.FindGameObjectsWithTag("Hunted");
        float exitDist = Vector3.Distance(exitTarget.position, transform.position);
        float playerDist;

        if (transform.parent.tag == "Hunted")
        {
            transform.LookAt(exitTarget);
            if(exitDist < 15)
            {
                rend.enabled = false;
            }
            else
            {
                rend.enabled = true;
            }
        }
        else
        {
            foreach(GameObject assignedTarget in playerTarget)
            {
                playerDist = Vector3.Distance(assignedTarget.gameObject.transform.position, transform.position);

                if (assignedTarget.name.Contains("Player"))
                {
                    transform.LookAt(assignedTarget.transform);
                }

                if (playerDist < 30)
                {
                    rend.enabled = false;
                }
                else
                {
                    rend.enabled = true;
                }
            }
        }
    }
}
