using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitEventTrigger : MonoBehaviour
{
    /// <summary>
    /// Checks if the chest or head has been hit and returns the relevant damage value
    /// </summary>


    public UnityEvent onHit;

    private string hitBy;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip punchSound;

    // Use this for initialization
    void Start () {
        audioSource.clip = punchSound;
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
            if(audioSource != null)
            {
                //Plays punching sound
                audioSource.Play();
            }
            else
            {
                return;
            }

            //Checks last gameobject to collide with the hunted team
            if (gameObject.transform.root.tag == "Hunted")
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
