using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitPointTrigger : MonoBehaviour {

    public UnityEvent onPlayerEnter;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collidingPlayer)
    {
        if(collidingPlayer.gameObject.tag == "Hunted")
        {
            Debug.Log("YOU WIN");
        }
    }
}
