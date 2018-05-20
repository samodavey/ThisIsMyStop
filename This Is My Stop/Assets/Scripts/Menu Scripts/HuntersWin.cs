using UnityEngine;
using UnityEngine.SceneManagement;

public class HuntersWin : MonoBehaviour
{
    private GameObject winningTeam;

    private PlayerController collidedPlayer;
    private Canvas canvas;
    private TextMesh title;
    private AudioListener audioListener;
    private string teamName;

    [SerializeField]
    private PlayerControlsSO[] playerControllers;

    // Use this for initialization
    void Start()
    {
        audioListener = FindObjectOfType<AudioListener>();
        winningTeam = GameObject.Find("WinningTeam");
        collidedPlayer = FindObjectOfType<PlayerController>();
        canvas = FindObjectOfType<Canvas>();
        title = FindObjectOfType<TextMesh>();
        //teamName = LayerMask.LayerToName(collidedPlayer.gameObject.layer);

        Destroy(audioListener);
    }

    // Update is called once per frame
    void Update()
    {
        if(winningTeam == null)
        {
            winningTeam = GameObject.Find("WinningTeam");
        }
        else
        {
            teamName = winningTeam.GetComponent<WinningTeam>().winnner;
            title.text = teamName + "\n killed the escaping team! \n Press start to quit the game";
        }

        for (int i = 0; i < playerControllers.Length; i++)
        {
            if (Input.GetKeyDown(playerControllers[i].lobbyReady))
            {
                Application.Quit();
            }
        }

    }
}
