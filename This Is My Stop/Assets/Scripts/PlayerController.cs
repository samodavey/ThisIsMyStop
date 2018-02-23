using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator anim;

    [SerializeField]
    private Transform player1;

    [SerializeField]
    private Transform player2;

    //[SerializeField]
    //private Transform player3;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        //PLAYER 1 CONTROLS
        var x = Input.GetAxis("Joy1_Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Joy1_Vertical") * Time.deltaTime * -7.0f;
        player1.Rotate(0, x, 0);
        player1.Translate(0, 0, z);


        //PLAYER 2 CONTROLS
        var a = Input.GetAxis("Joy2_Horizontal") * Time.deltaTime * 150.0f;
        var b = Input.GetAxis("Joy2_Vertical") * Time.deltaTime * -7.0f;
        player2.Rotate(0, a, 0);
        player2.Translate(0, 0, b);


        anim.SetFloat("Speed1", z);
        anim.SetFloat("Speed2", b);
        //Debug.Log("Speed: " + z);
    }

}
