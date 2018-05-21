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
        //If escaping team hits escape point load escaped scene
        if(playerCollider.transform.parent.gameObject.tag == "Hunted")
        {
            SceneManager.LoadScene("Escaped",LoadSceneMode.Additive);
            Scene sceneToLoad = SceneManager.GetSceneByName("Escaped");
            SceneManager.MoveGameObjectToScene(playerCollider.gameObject, sceneToLoad);
            SceneManager.UnloadSceneAsync("MainScene");
        }
    }
}
