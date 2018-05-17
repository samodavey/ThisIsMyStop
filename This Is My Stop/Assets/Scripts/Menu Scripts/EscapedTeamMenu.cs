using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapedTeamMenu : MonoBehaviour {


    PlayerController collidedPlayer;
    Canvas canvas;
    TextMesh title;
    string teamName;

    // Use this for initialization
    void Start () {

        collidedPlayer = FindObjectOfType<PlayerController>();
        canvas = FindObjectOfType<Canvas>();
        title = FindObjectOfType<TextMesh>();
        teamName = LayerMask.LayerToName(collidedPlayer.gameObject.layer);

        title.text = teamName + " Escaped! \n Press start to return to the main menu";
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(collidedPlayer);
	}
}
