using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour {

    /// <summary>
    /// This class is for specifying which direction the arrow gameobject points to
    /// </summary>

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
            //Arrow transform looks at target
            transform.LookAt(exitTarget);

            //Disables arrow if within a certain distance
            if (exitDist < 15)
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
                    //Arrow transform looks at target
                    transform.LookAt(assignedTarget.transform);
                }

                //Disables arrow if within a certain distance
                if (playerDist < 15)
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
