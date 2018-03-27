using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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

    public UnityEvent onDamage;

    private int timePunch;

    private bool lightPunchTrigger;

    private bool heavyPunchTrigger;

    private bool blockTrigger;

    private bool takePunchTrigger;

    private bool isDead = false;

    private Transform newObject;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        anim = GetComponent<Animator>();
        playerControls.health = 500;
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

        var joyCamHor = Input.GetAxis(playerControls.camHor) * Time.deltaTime * 170.0f;

        transform.Rotate(0, joyCamHor, 0);
        transform.Rotate(0, joyHor, 0);
        transform.Translate(0, 0, joyVert);

        Vector3 move = new Vector3(joyHor, 0, -joyVert);
          
        anim.SetFloat("Speed", joyVert);

        //Light Punches
        //switch (timePunch)
        //{
        //    case 0:
        //        if (Input.GetKeyDown(playerControls.lightPunch))
        //        {
        //            lightPunchTrigger = true;
        //            anim.SetBool("LightPunch", lightPunchTrigger);
        //            if (anim.GetCurrentAnimatorStateInfo(0).IsName("LightPunch"))
        //            {
        //                anim.SetBool("LightPunch", false);
        //                timePunch++;
        //            }
        //        }
        //        break;
        //    case 1:
        //        if (Input.GetKeyDown(playerControls.lightPunch))
        //        {
        //            lightPunchTrigger = true;
        //            anim.SetBool("LightPunch", false);
        //            anim.SetBool("LightPunch2", lightPunchTrigger);
        //            anim.SetBool("LightPunch3", false);
        //            timePunch++;
        //        }
        //        break;
        //    case 2:
        //        if (Input.GetKeyDown(playerControls.lightPunch))
        //        {
        //            lightPunchTrigger = true;
        //            anim.SetBool("LightPunch", false);
        //            anim.SetBool("LightPunch2", false);
        //            anim.SetBool("LightPunch3", lightPunchTrigger);
        //            timePunch++;
        //        }
        //        break;
        //    default:
        //        anim.SetBool("LightPunch", false);
        //        anim.SetBool("LightPunch2", false);
        //        anim.SetBool("LightPunch3", false);
        //        break;
        //}

        Mathf.Clamp(timePunch, 0, 2);
        if (Input.GetKeyDown(playerControls.lightPunch))
        {
            lightPunchTrigger = true;
            switch (timePunch)
            {
                case 0:
                    anim.SetBool("LightPunch1", lightPunchTrigger);
                    timePunch++;
                    break;
                case 1:
                    anim.SetBool("LightPunch2", lightPunchTrigger);
                    timePunch++;
                    break;
                case 2:
                    anim.SetBool("LightPunch3", lightPunchTrigger);
                    timePunch++;
                    break;
                //case 3:
                //    break;
                default:
                    anim.SetBool("LightPunch", false);
                    anim.SetBool("LightPunch2", false);
                    anim.SetBool("LightPunch3", false);
                    timePunch = 0;
                    break;
            }
        }
        for (int i = 0; i <= 3; i++)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Light Punch " + i))
            {
                anim.SetBool("LightPunch" + i, false);
            }
        }

        Debug.Log(timePunch);

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

        //Blocking
        if (Input.GetKey(playerControls.blocking))
        {
            blockTrigger = true;
        }
        else
        {
            blockTrigger = false;
        }
        anim.SetBool("Blocking", blockTrigger);

        anim.SetBool("TakePunch", takePunchTrigger);

    }

    public void punchCheck(int hitCount)
    {
        Collider[] attackCollider = GetComponentsInChildren<Collider>();
        for(int i = 0; i < attackCollider.Length; i++)
        {
            if (hitCount == 1 && attackCollider[i].gameObject.tag == "DamageObj")
            {
                attackCollider[i].GetComponent<Collider>().enabled = true;
            }
            else if (hitCount == 0)
            {
                attackCollider[i].GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void startBlock(string message)
    {
        if(message == "Start Blocking")
        {

        }
    }

    public void AlertObservers(string message)
    {
        if(message == "Animation Ended")
        {
            takePunchTrigger = false;
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
        if (playerControls.health == 0 && !isDead)
        {
            isDead = true;
            //takePunch = false;
            newObject = (Transform)PrefabUtility.InstantiatePrefab(playerControls.playerRagdoll);
            newObject.transform.position = transform.position;
            newObject.transform.rotation = transform.rotation;
            gameObject.SetActive(false);
        }
        else
        {
            takePunchTrigger = true;
            playerControls.health = playerControls.health - damage;
            onDamage.Invoke();
        }
    }
}
