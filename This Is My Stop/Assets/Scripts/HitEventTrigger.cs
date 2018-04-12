using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitEventTrigger : MonoBehaviour
{
    public UnityEvent onHit;

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    private void Update()
    {

    }

    void OnTriggerEnter (Collider other)
    {
        Animator anim = GetComponentInParent<Animator>();
        if (other.tag == "DamageObj" && !anim.GetCurrentAnimatorStateInfo(0).IsName("Blocking"))
        {
            //other.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
            onHit.Invoke();
        }
        else if(other.tag == "DamageObj" && anim.GetCurrentAnimatorStateInfo(0).IsName("Blocking"))
        {
            //anim.SetBool("Blocking", false);
            anim.SetBool("BlockReact", true);
        }
	}
}
