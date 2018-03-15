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


    [SerializeField]
    private Camera playerOneCamera;

    [SerializeField]
    private Camera playerTwoCamera;

    [SerializeField]
    private Camera playerThreeCamera;

    [SerializeField]
    private Camera playerFourCamera;

    [SerializeField]
    private Transform player1;

    [SerializeField]
    private Transform player2;

    [SerializeField]
    private Transform player3;

    [SerializeField]
    private Transform player4;

    [SerializeField]
    private Transform playerRagdoll;

    [SerializeField]
    private Transform playerTwoRagdoll;

    [SerializeField]
    private Transform playerThreeRagdoll;

    [SerializeField]
    private Transform playerFourRagdoll;

    [SerializeField]
    private Collider playerOneFistCollider;

    [SerializeField]
    private Collider playerTwoFistCollider;

    [SerializeField]
    private Collider playerThreeFistCollider;

    [SerializeField]
    private Collider playerFourFistCollider;

    private int collisionCount = 0;

    private bool lightPunchTrigger1;
    private bool lightPunchTrigger2;

    private bool heavyPunchTrigger1;
    private bool heavyPunchTrigger2;

    private bool takePunch1;
    private bool takePunch2;

    private int hitCount = 0;

    public float playerOneCurrentHealth = 500f;
    public float playerTwoCurrentHealth = 500f;
    public float playerThreeCurrentHealth = 500f;
    public float playerFourCurrentHealth = 500f;


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
        //Horizontal Values
        var joyHor1 = Input.GetAxis("Joy1_Horizontal") * Time.deltaTime * 50.0f;
        var joyHor2 = Input.GetAxis("Joy2_Horizontal") * Time.deltaTime * 50.0f;
        var joyHor3 = Input.GetAxis("Joy3_Horizontal") * Time.deltaTime * 50.0f;
        var joyHor4 = Input.GetAxis("Joy4_Horizontal") * Time.deltaTime * 50.0f;

        //Vertical Values
        var joyVert1 = Input.GetAxis("Joy1_Vertical") * Time.deltaTime * -5.0f;
        var joyVert2 = Input.GetAxis("Joy2_Vertical") * Time.deltaTime * -5.0f;
        var joyVert3 = Input.GetAxis("Joy3_Vertical") * Time.deltaTime * -5.0f;
        var joyVert4 = Input.GetAxis("Joy4_Vertical") * Time.deltaTime * -5.0f;

        //Camera Rotation Horizontal
        var joyCamHor1 = Input.GetAxis("Joy1_CameraRotateHor") * Time.deltaTime * 7.0f;
        var joyCamHor2 = Input.GetAxis("Joy2_CameraRotateHor") * Time.deltaTime * 7.0f;
        var joyCamHor3 = Input.GetAxis("Joy3_CameraRotateHor") * Time.deltaTime * 7.0f;
        var joyCamHor4 = Input.GetAxis("Joy4_CameraRotateHor") * Time.deltaTime * 7.0f;

        //Camera Rotation Vertical
        var joyCamVert1 = Input.GetAxis("Joy1_CameraRotateVert");
        var joyCamVert2 = Input.GetAxis("Joy2_CameraRotateVert");
        var joyCamVert3 = Input.GetAxis("Joy3_CameraRotateVert");
        var joyCamVert4 = Input.GetAxis("Joy4_CameraRotateVert");


        player1.Rotate(0, joyHor1, 0);
        player1.Translate(0, 0, joyVert1);

        playerOneCamera.transform.LookAt(player1);
        playerOneCamera.transform.Rotate(-15, 0, 0);
        playerOneCamera.transform.Translate(Vector3.right * joyCamHor1);

        player2.Rotate(0, joyHor2, 0);
        player2.Translate(0, 0, joyVert2);

        playerTwoCamera.transform.LookAt(player2);
        playerTwoCamera.transform.Rotate(-15, 0, 0);
        playerTwoCamera.transform.Translate(Vector3.right * joyCamHor2);

        player3.Rotate(0, joyHor3, 0);
        player3.Translate(0, 0, joyVert3);

        playerThreeCamera.transform.LookAt(player3);
        playerThreeCamera.transform.Rotate(-15, 0, 0);
        playerThreeCamera.transform.Translate(Vector3.right * joyCamHor3);

        player4.Rotate(0, joyHor4, 0);
        player4.Translate(0, 0, joyVert4);

        playerFourCamera.transform.LookAt(player4);
        playerFourCamera.transform.Rotate(-15, 0, 0);
        playerFourCamera.transform.Translate(Vector3.right * joyCamHor4);

        anim.SetFloat("Speed1", joyVert1);
        anim.SetFloat("Speed2", joyVert2);
        anim.SetFloat("Speed3", joyVert3);
        anim.SetFloat("Speed4", joyVert4);


        //Make into a switch? (Can't be done!)
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


        if (objectCollision.gameObject.tag == "Team 1")
        {
            playerOneCurrentHealth = playerOneCurrentHealth - 250;
            anim.SetBool("TakePunch", takePunch1);
            if (playerOneCurrentHealth == 0)
            {
                newObject = (Transform)PrefabUtility.InstantiatePrefab(playerRagdoll);
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
        }

            //if (!objectCollision.gameObject.CompareTag("Team 1"))
            //{
            //    anim.SetBool("TakePunch2", takePunch2);
            //    if (currentHealth == 0)
            //    {
            //        newObject = (Transform)PrefabUtility.InstantiatePrefab(playerTwoRagdoll);
            //        newObject.transform.position = objectCollision.transform.position;
            //        newObject.transform.rotation = objectCollision.transform.rotation;

            //        for (int i = objectCollision.transform.childCount - 1; i >= 0; --i)
            //        {
            //            Transform child = objectCollision.transform.GetChild(i);
            //            Quaternion newRot = new Quaternion(90, newObject.transform.rotation.y, newObject.transform.rotation.z, 0);
            //            //Debug.Log("moving object: " + child.name);
            //            child.SetParent(newObject.transform, false);

            //            string childName = child.gameObject.name;

            //            switch (childName)
            //            {
            //                case "2nd Player Camera":
            //                    child.transform.position = new Vector3(newObject.transform.position.x, 10, newObject.transform.position.z);
            //                    child.transform.rotation = Quaternion.Lerp(child.transform.rotation, newRot, Time.deltaTime * 0.5f);
            //                    break;
            //                case "Cube":
            //                    child.gameObject.SetActive(false);
            //                    break;
            //                case "Player 2 Circle":
            //                    child.gameObject.SetActive(false);
            //                    break;

            //                default:
            //                    //child.transform.position = newObject.transform.position;
            //                    //child.transform.rotation = newObject.transform.rotation;
            //                    break;
            //            }
            //        }
            //        objectCollision.gameObject.SetActive(false);
            //    }
            //}
            //else if (objectCollision.gameObject.CompareTag("Team 2") && collisionCount < 3)
            //{
            //    //anim.SetBool("TakePunch", takePunch1);
            //    collisionCount++;
            //}
        }

    public void punchCheck(int hitCount)
    {
        if (hitCount == 1)
        {
            playerOneFistCollider.enabled = true;
            playerTwoFistCollider.enabled = true;
            playerThreeFistCollider.enabled = true;
            playerFourFistCollider.enabled = true;
        }
        else if (hitCount == 0)
        {
            playerOneFistCollider.enabled = false;
            playerTwoFistCollider.enabled = false;
            playerThreeFistCollider.enabled = false;
            playerFourFistCollider.enabled = false;
        }
    }

    public float playerHealth(float playerHealth)
    {
        playerHealth = playerOneCurrentHealth;
        return playerHealth;
    }
}
