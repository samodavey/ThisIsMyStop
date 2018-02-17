using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour {

    public Transform target;
    public Transform myTransform;
    public List<Transform> allyTransforms;

    private bool hasLookedAt;

    private Vector3 randomDir;

    private Animator aiAnim;

    private float speed = 5;


    // Use this for initialization
    void Start () {
        aiAnim = GetComponent<Animator>();
        hasLookedAt = false;
    }
	
	// Update is called once per frame
	void Update () {

        //transform.Translate(Vector3.forward * 5 * Time.deltaTime);

        float steps = speed * Time.deltaTime;

        
        //Originally would avoid player, experiment with this a bit more?
        if (Vector3.Distance(transform.position, target.position) <= 1 )
        {
            if(hasLookedAt == false)
            {
                randomizeLookAt();
            }

            transform.position = Vector3.MoveTowards(transform.position, transform.position, steps);
            transform.LookAt(randomDir);
            aiAnim.SetFloat("AISpeed", 0);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, steps);
            transform.LookAt(target);
            aiAnim.SetFloat("AISpeed", 1.0f);
            hasLookedAt = false;

            //for (int i = 0; i < allyTransforms.Capacity; i++)
            //{
            //    Vector3 spaceFromFriendly = myTransform.position - allyTransforms[i].position;

            //    if (Vector3.Distance(transform.position, allyTransforms[i].position) <= 5)
            //    {
            //        transform.position = Vector3.MoveTowards(transform.position, spaceFromFriendly, steps);
            //    }
            //}

        }

        //if(Vector3.Distance(transform.position, allyTransform.position) <= 5)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, new Vector3(5, 0, 5), steps);
        //}

    }

    private void randomizeLookAt()
    {
        hasLookedAt = true;
        randomDir = new Vector3(Random.Range(-150.0f, 150.0f), 0, Random.Range(-150.0f, 150.0f));
    }
}
