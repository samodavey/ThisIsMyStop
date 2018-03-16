using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnHitDamage : UnityEvent<int> { }

public class HitTrigger : MonoBehaviour
{
    public int damage = 10;
    public UnityEvent onHit;

    public OnHitDamage onHitDamage;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
        onHit.Invoke();
        onHitDamage.Invoke(damage);

    }
}
