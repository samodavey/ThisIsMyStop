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

    //[SerializeField]
    //private Transform player4;

    private bool punchTrigger1;
    private bool punchTrigger2;

    [SerializeField]
    private Collider playerOneFistCollider;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        //playerOneFistCollider = GetComponent<Collider>();
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


        //Make into a switch?
        if(Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            punchTrigger1 = true;
        }
        else
        {
            punchTrigger1 = false;
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button2))
        {
            punchTrigger2 = true;
        }
        else
        {
            punchTrigger2 = false;
        }


        anim.SetFloat("Speed1", z);
        anim.SetFloat("Speed2", b);

        anim.SetBool("LightPunch", punchTrigger1);
        anim.SetBool("LightPunch2", punchTrigger2);

        //Debug.Log("Speed is: " + z);
    }

    private void OnCollisionEnter(Collision fistCollision)
    {
        if(fistCollision.collider.gameObject == fistCollision.gameObject && fistCollision.gameObject.CompareTag("Team 2"))
        {
            //Destroy(fistCollision.collider.gameObject);
            fistCollision.collider.gameObject.SetActive(false);
            Debug.Log("Hit Detected!");
        }
    }
    public void PrintEvent(string s)
    {
        Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
    }
}
