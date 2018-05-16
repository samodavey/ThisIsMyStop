using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEscaped : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider playerCollider)
    {
        //Destroy(other.gameObject);
        if(playerCollider.transform.parent.gameObject.tag == "Hunted")
        {

            DontDestroyOnLoad(playerCollider);
            SceneManager.LoadScene("Escaped");

            //Debug.Log("CONGRATS YOU'VE ESCAPED");
        }
    }
}
