using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    [SerializeField]
    private Transform playerOneRagdoll;

    [SerializeField]
    private Transform playerTwoRagdoll;


    private bool punchTrigger1;
    private bool punchTrigger2;

    [SerializeField]
    private Collider playerOneFistCollider;

    private int hitCount = 0;

    private Transform newObject;

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

    }

    public void OnTriggerEnter(Collider objectCollision)
    {
        if (objectCollision.gameObject.CompareTag("Team 2"))
        {
            newObject = (Transform)PrefabUtility.InstantiatePrefab(playerTwoRagdoll);
            newObject.transform.position = objectCollision.transform.position;
            newObject.transform.rotation = objectCollision.transform.rotation;

            for (int i = objectCollision.transform.childCount - 1; i >= 0; --i)
            {
                Transform child = objectCollision.transform.GetChild(i);
                Debug.Log("moving object: " + child.name);
                child.SetParent(newObject.transform, false);

                string childName = child.gameObject.name;

                switch (childName)
                {
                    case "2nd Player Camera":
                        child.transform.position = new Vector3(newObject.transform.position.x, 10,newObject.transform.position.z);
                        //child.transform.rotation = new Quaternion(180, newObject.transform.rotation.y, newObject.transform.rotation.z,0);
                        child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation, new Quaternion(newObject.transform.rotation.x, newObject.transform.rotation.y, 90, 0), Time.deltaTime * 0.2f);
                        break;
                    case "Cube":
                        child.gameObject.SetActive(false);
                        break;
                    case "Player 2 Circle":
                        child.gameObject.SetActive(false);
                        break;

                    default:
                        //child.transform.position = newObject.transform.position;
                        //child.transform.rotation = newObject.transform.rotation;
                        break;
                }
            }


            objectCollision.gameObject.SetActive(false);
        }
    }

    public void punchCheck(int hitCount)
    {
        if (hitCount == 1)
        {
            playerOneFistCollider.enabled = true;  
        }
        else if (hitCount == 0)
        {
            playerOneFistCollider.enabled = false;
        }
    }
}
