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

    /// <summary>
    /// I need to come up with a more efficient way of having each variable be unique to the character
    /// rather than just having a whole bunch of variables
    /// </summary>

    public PlayerControlsSO playerControls;

    //[SerializeField]
    //private Camera playerOneCamera;

    //[SerializeField]
    //private Camera playerTwoCamera;

    //[SerializeField]
    //private Camera playerThreeCamera;

    //[SerializeField]
    //private Camera playerFourCamera;

    //[SerializeField]
    //private Transform player1;

    //[SerializeField]
    //private Transform player2;

    //[SerializeField]
    //private Transform player3;

    //[SerializeField]
    //private Transform player4;

    //[SerializeField]
    //private Transform playerRagdoll;

    //[SerializeField]
    //private Transform playerTwoRagdoll;

    //[SerializeField]
    //private Transform playerThreeRagdoll;

    //[SerializeField]
    //private Transform playerFourRagdoll;

    //[SerializeField]
    //private Collider playerOneFistCollider;

    //[SerializeField]
    //private Collider playerTwoFistCollider;

    //[SerializeField]
    //private Collider playerThreeFistCollider;

    //[SerializeField]
    //private Collider playerFourFistCollider;

    private int collisionCount = 0;

    private bool lightPunchTrigger1;
    private bool lightPunchTrigger2;

    private bool heavyPunchTrigger1;
    private bool heavyPunchTrigger2;

    private bool takePunch1;
    private bool takePunch2;

    private int hitCount = 0;

    //public float playerOneCurrentHealth = 500f;
    //public float playerTwoCurrentHealth = 500f;
    //public float playerThreeCurrentHealth = 500f;
    //public float playerFourCurrentHealth = 500f;


    private Transform newObject;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        // MOVE TO LOBBY AT SOME POINT

        //if (Input.GetButton("Joy1_Start"))
        //{

        //}
        //else if (Input.GetButton("Joy2_Start"))
        //{

        //}
        //else if (Input.GetButton("Joy3_Start"))
        //{

        //}
        //else if (Input.GetButton("Joy4_Start"))
        //{

        //}
    }
	
	// Update is called once per frame
	void Update () {
        //Horizontal Value
        var joyHor = Input.GetAxis(playerControls.horizontal) * Time.deltaTime * 150.0f;

        //Vertical Value
        var joyVert = Input.GetAxis(playerControls.vertical) * Time.deltaTime * -20.0f;

        transform.Rotate(0, joyHor, 0);
        transform.Translate(0, 0, joyVert);

        //Camera Rotation Horizontal
        //var joyCamHor1 = Input.GetAxis("Joy1_CameraRotateHor") * Time.deltaTime * 7.0f;
        //var joyCamHor2 = Input.GetAxis("Joy2_CameraRotateHor") * Time.deltaTime * 7.0f;
        //var joyCamHor3 = Input.GetAxis("Joy3_CameraRotateHor") * Time.deltaTime * 7.0f;
        //var joyCamHor4 = Input.GetAxis("Joy4_CameraRotateHor") * Time.deltaTime * 7.0f;

        //Camera Rotation Vertical
        //var joyCamVert1 = Input.GetAxis("Joy1_CameraRotateVert");
        //var joyCamVert2 = Input.GetAxis("Joy2_CameraRotateVert");
        //var joyCamVert3 = Input.GetAxis("Joy3_CameraRotateVert");
        //var joyCamVert4 = Input.GetAxis("Joy4_CameraRotateVert");


        anim.SetFloat("Speed", joyVert);

        //Make into a switch?
        //Light Punches
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            lightPunchTrigger1 = true;
        }
        else if (Input.GetKeyDown(KeyCode.Joystick2Button2))
        {
            lightPunchTrigger2 = true;
        }
        else
        {
            lightPunchTrigger1 = false;
            lightPunchTrigger2 = false;
        }

        anim.SetBool("LightPunch", lightPunchTrigger1);
        anim.SetBool("LightPunch2", lightPunchTrigger2);

        //Heavy Punches
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            heavyPunchTrigger1 = true;
        }
        else if (Input.GetKeyDown(KeyCode.Joystick2Button3))
        {
            heavyPunchTrigger2 = true;
        }
        else
        {
            heavyPunchTrigger1 = false;
            heavyPunchTrigger2 = false;
        }

        anim.SetBool("HeavyPunch", heavyPunchTrigger1);
        anim.SetBool("HeavyPunch2", heavyPunchTrigger2);

    }

    public void OnTriggerEnter(Collider objectCollision)
    {

        //MAYBE MAKE A FOR LOOP WHICH CYCLES THROUGH ALL THE TEAMS?


        //ALSO PERHAPS ADD 3 TRIGGER COLLIDERS TO PLAYERS HEAD, CHEST, LEGS FOR TRIGGER + DAMAGE
        //RATHER THAN FIST TRIGGER

            print(objectCollision);
            if (playerControls.health == 0)
            {
                newObject = (Transform)PrefabUtility.InstantiatePrefab(playerControls.playerRagdoll);
                newObject.transform.position = objectCollision.transform.position;
                newObject.transform.rotation = objectCollision.transform.rotation;

                for (int z = objectCollision.transform.childCount - 1; z >= 0; --z)
                {
                    Transform child = objectCollision.transform.GetChild(z);
                    Quaternion newRot = new Quaternion(90, newObject.transform.rotation.y, newObject.transform.rotation.z, 0);
                    //Debug.Log("moving object: " + child.name);
                    child.SetParent(newObject.transform, false);

                    string childName = child.gameObject.name;

                    switch (childName)
                    {
                        case "Main Camera":
                            child.transform.position = new Vector3(newObject.transform.position.x, 10, newObject.transform.position.z);
                            child.transform.rotation = Quaternion.Lerp(child.transform.rotation, newRot, Time.deltaTime * 0.5f);
                            break;
                        case "Cube":
                            child.gameObject.SetActive(false);
                            break;
                        case "Player 1 Circle":
                            child.gameObject.SetActive(false);
                            break;

                        default:
                            break;
                    }
                }
                objectCollision.gameObject.SetActive(false);
            }
            else
            {
                playerControls.health = playerControls.health - 250;
                anim.SetBool("TakePunch", takePunch1);
            }
        }

    public void punchCheck(int hitCount)
    {
        if (hitCount == 1)
        {
            //playerOneFistCollider.enabled = true;
            //playerTwoFistCollider.enabled = true;
            //playerThreeFistCollider.enabled = true;
            //playerFourFistCollider.enabled = true;
        }
        else if (hitCount == 0)
        {
            //playerOneFistCollider.enabled = false;
            //playerTwoFistCollider.enabled = false;
            //playerThreeFistCollider.enabled = false;
            //playerFourFistCollider.enabled = false;
        }
    }

    public float playerHealth(float playerHealth)
    {
        //playerHealth = playerOneCurrentHealth;
        return playerHealth;
    }
}
