using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BasicAI : MonoBehaviour {

    public Transform target;
    public AIControlsSO AIControls;
    public UnityEvent onDamage;

    private bool hasLookedAt;

    private Vector3 randomDir;

    private Animator aiAnim;

    private Transform newObject;

    private float speed = 5;

    private bool takePunchTrigger;

    private bool isDead = false;

    private NavMeshAgent agent;


    // Use this for initialization
    void Start () {
        aiAnim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        hasLookedAt = false;
        AIControls.health = 250;
    }
	
	// Update is called once per frame
	void Update () {

        float steps = speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, target.position) <= 1 )
        {
            if(hasLookedAt == false)
            {
                randomizeLookAt();
            }
            agent.SetDestination(target.position);
            transform.LookAt(randomDir);
            aiAnim.SetFloat("Speed", 0);
        }
        else
        {
            agent.SetDestination(target.position);
            aiAnim.SetFloat("Speed", 1.0f);
            hasLookedAt = false;

        }

        //Blocking
        //SET THIS UP WHEN I GET AI TO ATTACK

        //if (Input.GetKey(playerControls.blocking))
        //{
        //    blockTrigger = true;
        //}
        //else
        //{
        //    blockTrigger = false;
        //}
        //anim.SetBool("Blocking", blockTrigger);

        aiAnim.SetBool("TakePunch", takePunchTrigger);
    }

    public float playerHealth(float playerHealth)
    {
        //playerHealth = playerOneCurrentHealth;
        return playerHealth;
    }

    public void TakeDamage(int damage)
    {
        //print("hit " + transform + " for " + damage);
        if (AIControls.health == 0 && !isDead)
        {
            isDead = true;
            //takePunch = false;
            newObject = (Transform)PrefabUtility.InstantiatePrefab(AIControls.aiRagdoll);
            newObject.transform.position = transform.position;
            newObject.transform.rotation = transform.rotation;
            gameObject.SetActive(false);
        }
        else
        {
            takePunchTrigger = true;
            AIControls.health = AIControls.health - damage;
            onDamage.Invoke();
        }
    }

    public void AlertObservers(string message)
    {
        if (message == "Animation Ended")
        {
            takePunchTrigger = false;
        }

    }

    private void randomizeLookAt()
    {
        hasLookedAt = true;
        randomDir = new Vector3(Random.Range(-150.0f, 150.0f), 0, Random.Range(-150.0f, 150.0f));
    }
}
