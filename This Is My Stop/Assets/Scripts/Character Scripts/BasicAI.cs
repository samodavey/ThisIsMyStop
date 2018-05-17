using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BasicAI : MonoBehaviour {

    [SerializeField]
    private Transform target;

    public AIControlsSO AIControls;

    public UnityEvent onDamage;

    private bool hasLookedAt;

    private Vector3 randomDir;

    private Animator aiAnim;

    private Transform newObject;

    private float speed = 3;

    private bool takePunchTrigger;

    private bool isDead = false;

    private bool newTarget = false;

    private float timeUntilNextAttack = 0;

    private NavMeshAgent agent;

    private List<GameObject> taggedTeams = new List<GameObject>();



    // Use this for initialization
    void Start () {
        aiAnim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        hasLookedAt = false;
        AIControls.health = 250;
        GameObject[] team1Chars = GameObject.FindGameObjectsWithTag("Team 1");
        GameObject[] team2Chars = GameObject.FindGameObjectsWithTag("Team 2");
        GameObject[] team3Chars = GameObject.FindGameObjectsWithTag("Team 3");
        GameObject[] team4Chars = GameObject.FindGameObjectsWithTag("Team 4");
        //Maybe knock it down to one for loop?
        for (int i = 0; i < team1Chars.Length; i++)
        {
            taggedTeams.Add(team1Chars[i].gameObject);
        }
        for (int i = 0; i < team2Chars.Length; i++)
        {
            taggedTeams.Add(team2Chars[i].gameObject);
        }
        for (int i = 0; i < team3Chars.Length; i++)
        {
            taggedTeams.Add(team3Chars[i].gameObject);
        }
        for (int i = 0; i < team4Chars.Length; i++)
        {
            taggedTeams.Add(team4Chars[i].gameObject);
        }
    }


    GameObject characterTemp = null;
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

        //If enemy is within a certain range, move towards that target
        //Whilst moving towards target in range, when within a certain distance
        //Do a random attack

        List<string> randomAttackList = new List<string> { "LightPunch1", "LightPunch2", "LightPunch3", "HeavyPunch1", "HeavyPunch2", "Kick1" };

        string chosenAttack = randomAttackList[Random.Range(0, randomAttackList.Count)];

        if (taggedTeams.Contains(null))
        {
            taggedTeams.RemoveAll((x) => x == null);
        }

        foreach (GameObject character in taggedTeams)
        {
            if (character.tag != this.gameObject.tag)
            {
                //List<Transform> targetedChar = new List<Transform>();

                float distance = Vector3.Distance(character.transform.position, this.transform.position);

                //targetedChar.Add(target);

                //Can't be done because of Monobehaviour to ScriptableObject issue

                //AIControlsSO enemyHealth = character.GetComponent<AIControlsSO>();
                //List<int> listedEnemyHealth = new List<int>();

                //listedEnemyHealth.Add(enemyHealth.health);

                ////Ideally attack the enemy with the lowest health
                //int lowestEnemyHealth = listedEnemyHealth.Min();

                //if(enemyHealth.health == lowestEnemyHealth)
                //{
                //    target.position = new Vector3(character.transform.position.x, character.transform.position.y, character.transform.position.z);
                //}
                    if (distance <= 8)
                    {
                        timeUntilNextAttack += Time.deltaTime;

                            if (newTarget == false || timeUntilNextAttack >= 2 && target.hasChanged)
                            {

                                //target.gameObject.transform.position = character.transform.position;

                                //target = character.transform;
                                //INVOKING IS GREAT!
                                if (!IsInvoking("SelectTarget"))
                                {
                                    characterTemp = character;
                                    Invoke("SelectTarget", 0.5f);
                                }

                                //target.position = new Vector3(character.transform.position.x, character.transform.position.y, character.transform.position.z - 3);

                                newTarget = true;

                                aiAnim.SetBool(chosenAttack, true);
                                timeUntilNextAttack = 0;

                            }
                    }

                    //Search for a new enemy
                    if (target.gameObject.activeSelf == false)
                    {
                        newTarget = false;
                        aiAnim.SetBool(chosenAttack, false);

                        if(character.tag == this.gameObject.tag)
                        {
                            target = character.transform;
                        }
                    
                    }

                    //Run away if health is low
                    if (AIControls.health <= 100)
                    {
                        aiAnim.SetBool("Blocking", true);
                    }
            }
        }

        aiAnim.SetBool("TakePunch", takePunchTrigger);
    }


    void SelectTarget()
    {
        target = characterTemp.transform;
        this.gameObject.transform.LookAt(characterTemp.transform);

    }

    public void punchCheck(int hitCount)
    {
        Collider[] attackCollider = GetComponentsInChildren<Collider>();
        for (int i = 0; i < attackCollider.Length; i++)
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

    public float playerHealth(float playerHealth)
    {
        //playerHealth = playerOneCurrentHealth;
        return playerHealth;
    }

    public void TakeDamage(int damage)
    {
        //print("hit " + transform + " for " + damage);
        if (AIControls.health <= 0 && !isDead)
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

    public void AlertObservers(string attackName)
    {
        aiAnim.SetBool(attackName, false);
        takePunchTrigger = false;
    }

    private void randomizeLookAt()
    {
        hasLookedAt = true;
        randomDir = new Vector3(Random.Range(-150.0f, 150.0f), 0, Random.Range(-150.0f, 150.0f));
    }

}
