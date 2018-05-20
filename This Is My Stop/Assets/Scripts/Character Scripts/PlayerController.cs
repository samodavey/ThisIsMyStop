using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
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

    private int timeLightPunch;

    private int timeHeavyPunch;

    private int timeKick;

    private bool lightPunchTrigger;

    private bool heavyPunchTrigger;

    private bool kickTrigger;

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
    }
	
	// Update is called once per frame
	void Update () {
        //Horizontal Value
        var joyHor = Input.GetAxis(playerControls.horizontal) * Time.deltaTime * 350.0f;

        //Vertical Value
        var joyVert = Input.GetAxis(playerControls.vertical) * Time.deltaTime * -20.0f;

        var joyCamHor = Input.GetAxis(playerControls.camHor) * Time.deltaTime * 170.0f;


        //Blocking
        if (Input.GetKey(playerControls.blocking))
        {
            blockTrigger = true;
        }
        else
        {
            blockTrigger = false;
            //Player cannot move forward whilst blocking
            transform.Translate(0, 0, joyVert);
        }
        anim.SetBool("Blocking", blockTrigger);

        transform.Rotate(0, joyCamHor, 0);
        transform.Rotate(0, joyHor, 0);

        Vector3 move = new Vector3(joyHor, 0, -joyVert);
          
        anim.SetFloat("Speed", joyVert);

        //Light Punches

        if (Input.GetKeyDown(playerControls.lightPunch))
        {
            lightPunchTrigger = true;
            switch (timeLightPunch)
            {
                case 0:
                    anim.SetBool("LightPunch1", lightPunchTrigger);
                    timeLightPunch++;
                    break;
                case 1:
                    anim.SetBool("LightPunch2", lightPunchTrigger);
                    timeLightPunch++;
                    break;
                case 2:
                    anim.SetBool("LightPunch3", lightPunchTrigger);
                    timeLightPunch = 0;
                    break;
                default:
                    anim.SetBool("LightPunch", false);
                    anim.SetBool("LightPunch2", false);
                    anim.SetBool("LightPunch3", false);
                    timeLightPunch = 0;
                    break;
            }
        }
        if (Input.GetKeyDown(playerControls.heavyPunch))
        {
            heavyPunchTrigger = true;
            switch (timeHeavyPunch)
            {
                case 0:
                    anim.SetBool("HeavyPunch1", heavyPunchTrigger);
                    timeHeavyPunch++;
                    break;
                case 1:
                    anim.SetBool("HeavyPunch2", heavyPunchTrigger);
                    timeHeavyPunch = 0;
                    break;
                //case 2:
                //    anim.SetBool("HeavyPunch3", heavyPunchTrigger);
                //    timeHeavyPunch++;
                //    break;
                default:
                    anim.SetBool("HeavyPunch1", false);
                    anim.SetBool("HeavyPunch2", false);
                    //anim.SetBool("HeavyPunch3", false);
                    timeHeavyPunch = 0;
                    break;
            }
        }
        if (Input.GetKeyDown(playerControls.kick))
        {
            kickTrigger = true;
            switch (timeKick)
            {
                case 0:
                    anim.SetBool("Kick1", kickTrigger);
                    timeKick = 0;
                    break;
                //case 1:
                //    anim.SetBool("Kick2", kickTrigger);
                //    timeKick++;
                //    break;
                //case 2:
                //    anim.SetBool("HeavyPunch3", heavyPunchTrigger);
                //    timeHeavyPunch++;
                //    break;
                default:
                    anim.SetBool("Kick1", false);
                    //anim.SetBool("Kick2", false);
                    timeKick = 0;
                    break;
            }
        }
        for (int i = 0; i <= 3; i++)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Light Punch " + i))
            {
                anim.SetBool("LightPunch" + i, false);
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy Punch " + i))
            {
                anim.SetBool("HeavyPunch" + i, false);
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Kick " + i))
            {
                anim.SetBool("Kick" + i, false);
            }
        }

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

            //To prevent hit point boxes getting turned off
            if (attackCollider[i].gameObject.tag == "HitPoint")
            {
                attackCollider[i].GetComponent<Collider>().enabled = true;
            }
        }
    }

    public void kickCheck(int hitCount)
    {
        Collider[] attackCollider = GetComponentsInChildren<Collider>();
        for (int i = 0; i < attackCollider.Length; i++)
        {
            if (hitCount == 1 && attackCollider[i].gameObject.tag == "KickDamageObj")
            {
                attackCollider[i].GetComponent<Collider>().enabled = true;
            }
            else if (hitCount == 0)
            {
                attackCollider[i].GetComponent<Collider>().enabled = false;
            }

            //To prevent hit point boxes getting turned off
            if (attackCollider[i].gameObject.tag == "HitPoint")
            {
                attackCollider[i].GetComponent<Collider>().enabled = true;
            }
        }
    }

    public void AlertObservers(string message)
    {
        if (message == "TakePunch")
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
        //print("hit " + transform + " for " + damage);
        if (playerControls.health <= 0 && !isDead)
        {
            isDead = true;
            //takePunch = false;
            newObject = Instantiate(playerControls.playerRagdoll).transform;
            newObject.transform.position = transform.position;
            newObject.transform.rotation = transform.rotation;
            gameObject.SetActive(false);
        }
        else
        {
            takePunchTrigger = true;
            playerControls.health -= damage;
            onDamage.Invoke();
        }
    }
}
