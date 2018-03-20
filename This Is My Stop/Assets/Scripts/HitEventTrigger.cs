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
	void OnTriggerEnter (Collider other)
    {
        other.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
        onHit.Invoke();	
	}
}
