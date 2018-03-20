using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour {

    NavMeshAgent agent;

    private Animator anim;

    private int controllerNumber;
    private string horizontalAxis;
    private string verticalAxis;
    private string xButton;

    /// <summary>
    /// This class focuses on player controls and actions
    /// </summary>

    public PlayerControlsSO playerControls;

    [SerializeField]
    private Collider headCollider;

    [SerializeField]
    private Collider middleCollider;

    //[SerializeField]
    //private Collider playerOneFistCollider;

    //[SerializeField]
    //private Collider playerTwoFistCollider;

    //[SerializeField]
    //private Collider playerThreeFistCollider;

    //[SerializeField]
    //private Collider playerFourFistCollider;

    //private int collisionCount = 0;
    
    private bool lightPunchTrigger;

    private bool heavyPunchTrigger;

    private bool takePunch;

    bool isDead = false;

    //private int hitCount = 0;

    private Transform newObject;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
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
        var joyHor = Input.GetAxis(playerControls.horizontal); //* Time.deltaTime * 150.0f;

        //Vertical Value
        var joyVert = Input.GetAxis(playerControls.vertical); //* Time.deltaTime * -20.0f;

        var joyCamHor1 = Input.GetAxis("Joy1_CameraRotateHor");

        transform.Rotate(0, joyCamHor1, 0);
        //transform.Translate(0, 0, joyVert);

        Vector3 move = new Vector3(joyHor, 0, -joyVert);

        agent.Move(move);

        //Remove Camera Axis + stuff below

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
        if (Input.GetKeyDown(playerControls.lightPunch))
        {
            lightPunchTrigger = true;
        }
        else
        {
            lightPunchTrigger = false;
        }

        anim.SetBool("LightPunch", lightPunchTrigger);

        //Heavy Punches
        if (Input.GetKeyDown(playerControls.heavyPunch))
        {
            heavyPunchTrigger = true;
        }
        else
        {
            heavyPunchTrigger = false;
        }

        anim.SetBool("HeavyPunch", heavyPunchTrigger);

    }

    public void OnTriggerEnter(Collider other)
    {

        //MAYBE MAKE A FOR LOOP WHICH CYCLES THROUGH ALL THE TEAMS?


        //ALSO PERHAPS ADD 3 TRIGGER COLLIDERS TO PLAYERS HEAD, CHEST, LEGS FOR TRIGGER + DAMAGE
        //RATHER THAN FIST TRIGGER

        //print(other);
            if (playerControls.health == 0 && !isDead)
            {
            isDead = true;
            //GetComponent<Collider>().enabled = false;
                //newObject = (Transform)PrefabUtility.InstantiatePrefab(playerControls.playerRagdoll);
                newObject = (Transform)PrefabUtility.InstantiatePrefab(playerControls.playerRagdoll);
                newObject.transform.position = other.transform.position;
                newObject.transform.rotation = other.transform.rotation;
                
                for (int z = other.transform.childCount - 1; z >= 0; --z)
                {
                    Transform child = other.transform.GetChild(z);
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
                other.gameObject.SetActive(false);
                playerControls.health = 500;
            }
            else
            {
                playerControls.health = playerControls.health - 250;
                anim.SetBool("TakePunch", takePunch);
            }
    }

    public void punchCheck(int hitCount)
    {
        if (hitCount == 1)
        {
            //playerOneFistCollider.enabled = true;
        }
        else if (hitCount == 0)
        {
            //playerOneFistCollider.enabled = false;
        }
    }

    public float playerHealth(float playerHealth)
    {
        //playerHealth = playerOneCurrentHealth;
        return playerHealth;
    }

    public void TakeDamage(int damage)
    {
        print("hit " + transform + " for " + damage);
        // if health is zero, activate ragdoll here
    }
}
