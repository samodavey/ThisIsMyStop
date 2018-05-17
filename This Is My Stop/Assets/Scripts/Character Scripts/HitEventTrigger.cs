using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitEventTrigger : MonoBehaviour
{
    public UnityEvent onHit;

    private string hitBy;

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
        if (other.tag == "DamageObj" || other.tag == "KickDamageObj" && !anim.GetCurrentAnimatorStateInfo(0).IsName("Blocking"))
        {
            onHit.Invoke();
            if(gameObject.transform.parent.tag == "Hunted")
            {
                hitBy = LayerMask.LayerToName(other.gameObject.layer);
                FindObjectOfType<WinningTeam>().winnner = hitBy;
            }
        }
        else if(other.tag == "DamageObj" || other.tag == "KickDamageObj" && anim.GetCurrentAnimatorStateInfo(0).IsName("Blocking"))
        {
            //anim.SetBool("Blocking", false);
            anim.SetBool("BlockReact", true);
        }
	}
}
