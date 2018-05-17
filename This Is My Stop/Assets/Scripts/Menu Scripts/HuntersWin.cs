using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntersWin : MonoBehaviour
{
    private GameObject winningTeam;

    PlayerController collidedPlayer;
    Canvas canvas;
    TextMesh title;
    AudioListener audioListener;
    string teamName;

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
            title.text = "               " + teamName + "\n killed the escaping team!";
        }
    }
}
