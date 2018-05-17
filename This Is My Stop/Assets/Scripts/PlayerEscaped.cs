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
            Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(2);
            DontDestroyOnLoad(playerCollider);
            SceneManager.MoveGameObjectToScene(playerCollider.gameObject, sceneToLoad);
            SceneManager.SetActiveScene(sceneToLoad);

            //Debug.Log("CONGRATS YOU'VE ESCAPED");
        }
    }
}
