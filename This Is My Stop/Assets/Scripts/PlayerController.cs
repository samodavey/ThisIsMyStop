using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator anim;

    private int controllerNumber;
    private string horizontalAxis;
    private string verticalAxis;
    private string xButton;

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

    [SerializeField]
    private Collider playerOneFistCollider;

    private int collisionCount = 0;

    private bool lightPunchTrigger1;
    private bool lightPunchTrigger2;

    private bool heavyPunchTrigger1;
    private bool heavyPunchTrigger2;

    private bool takePunch1;
    private bool takePunch2;

    private int hitCount = 0;

    private Transform newObject;

    public float Horizontal { get; set; }
    public float Vertical { get; set; }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        //playerOneFistCollider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        ////PLAYER 1 CONTROLS
        //var x = Input.GetAxis("Joy1_Horizontal") * Time.deltaTime * 150.0f;
        //var z = Input.GetAxis("Joy1_Vertical") * Time.deltaTime * -7.0f;
        //player1.Rotate(0, x, 0);
        //player1.Translate(0, 0, z);


        ////PLAYER 2 CONTROLS
        //var a = Input.GetAxis("Joy2_Horizontal") * Time.deltaTime * 150.0f;
        //var b = Input.GetAxis("Joy2_Vertical") * Time.deltaTime * -7.0f;
        //player2.Rotate(0, a, 0);
        //player2.Translate(0, 0, b);


        ////Make into a switch?
        ////Light Punches
        //if(Input.GetKeyDown(KeyCode.Joystick1Button2))
        //{
        //    lightPunchTrigger1 = true;
        //}
        //else if (Input.GetKeyDown(KeyCode.Joystick2Button2))
        //{
        //    lightPunchTrigger2 = true;
        //}
        //else
        //{
        //    lightPunchTrigger1 = false;
        //    lightPunchTrigger2 = false;
        //}

        ////Heavy Punches
        //if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        //{
        //    heavyPunchTrigger1 = true;
        //}
        //else if (Input.GetKeyDown(KeyCode.Joystick2Button3))
        //{
        //    heavyPunchTrigger2 = true;
        //}
        //else
        //{
        //    heavyPunchTrigger1 = false;
        //    heavyPunchTrigger2 = false;
        //}


        //anim.SetFloat("Speed1", z);
        //anim.SetFloat("Speed2", b);

        //anim.SetBool("LightPunch", lightPunchTrigger1);
        //anim.SetBool("LightPunch2", lightPunchTrigger2);

        //anim.SetBool("HeavyPunch", heavyPunchTrigger1);
        //anim.SetBool("HeavyPunch2", heavyPunchTrigger2);

    }

    private void FixedUpdate()
    {
        if(controllerNumber > 0)
        {
            Horizontal = Input.GetAxis(horizontalAxis);
            Vertical = Input.GetAxis(verticalAxis);
        }
    }

    private void SetControllerNumber(int number)
    {
        controllerNumber = number;
        horizontalAxis = "Joy" + number + "_Horizontal";
        verticalAxis = "Joy" + number + "_Vertical";
        xButton = "Joy" + number + "_X";
    }

    public void OnTriggerEnter(Collider objectCollision)
    {
        Debug.Log("Collided " + collisionCount + " times!");
        if (objectCollision.gameObject.CompareTag("Team 2") && collisionCount == 3)
        {
            newObject = (Transform)PrefabUtility.InstantiatePrefab(playerTwoRagdoll);
            newObject.transform.position = objectCollision.transform.position;
            newObject.transform.rotation = objectCollision.transform.rotation;

            for (int i = objectCollision.transform.childCount - 1; i >= 0; --i)
            {
                Transform child = objectCollision.transform.GetChild(i);
                Quaternion newRot = new Quaternion(90, newObject.transform.rotation.y, newObject.transform.rotation.z, 0);
                //Debug.Log("moving object: " + child.name);
                child.SetParent(newObject.transform, false);

                string childName = child.gameObject.name;

                switch (childName)
                {
                    case "2nd Player Camera":
                        child.transform.position = new Vector3(newObject.transform.position.x, 10,newObject.transform.position.z);
                        child.transform.rotation = Quaternion.Lerp(child.transform.rotation, newRot, Time.deltaTime * 0.5f);
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
        else if(objectCollision.gameObject.CompareTag("Team 2") && collisionCount < 3)
        {
            //anim.SetBool("TakePunch", takePunch1);
            anim.SetBool("TakePunch2", takePunch2);
            collisionCount++;
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
