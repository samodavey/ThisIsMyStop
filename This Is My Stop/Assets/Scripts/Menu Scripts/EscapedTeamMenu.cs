using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapedTeamMenu : MonoBehaviour {


    private PlayerController collidedPlayer;
    private Canvas canvas;
    private TextMesh title;
    private string teamName;
    private AudioListener audioListener;
    [SerializeField]
    private PlayerControlsSO[] playerControllers;

    // Use this for initialization
    void Start () {

        audioListener = FindObjectOfType<AudioListener>();
        collidedPlayer = FindObjectOfType<PlayerController>();
        canvas = FindObjectOfType<Canvas>();
        title = FindObjectOfType<TextMesh>();
        teamName = LayerMask.LayerToName(collidedPlayer.gameObject.layer);

        title.text = teamName + " Escaped! \n Press start to return to quit the game";
        Destroy(audioListener);
    }
	
	// Update is called once per frame
	void Update () {
        

        for(int i = 0; i < playerControllers.Length; i++)
        {
            if (Input.GetKeyDown(playerControllers[i].lobbyReady))
            {
                Application.Quit();
            }
        }

	}
}
