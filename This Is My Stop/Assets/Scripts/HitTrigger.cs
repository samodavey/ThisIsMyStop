using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnHitDamage : UnityEvent<int> { }

public class HitTrigger : MonoBehaviour
{
    public int damage = 10;

    private bool takePunch;

    private Animator anim;

    private Transform newObject;

    private GameObject parent;

    public PlayerControlsSO playerControls;

    public UnityEvent onHit;

    public OnHitDamage onHitDamage;

    // Use this for initialization
    void Start () {
        anim = transform.parent.GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        onHit.Invoke();
        //onHitDamage.Invoke(damage);

        Debug.Log(other);
        //MAYBE MAKE A FOR LOOP WHICH CYCLES THROUGH ALL THE TEAMS?


        //ALSO PERHAPS ADD 3 TRIGGER COLLIDERS TO PLAYERS HEAD, CHEST, LEGS FOR TRIGGER + DAMAGE
        //RATHER THAN FIST TRIGGER

        print(other);
        if (playerControls.health == 0)
        {
            newObject = (Transform)PrefabUtility.InstantiatePrefab(playerControls.playerRagdoll);
            newObject.transform.position = other.transform.position;
            newObject.transform.rotation = other.transform.rotation;

            for (int z = other.transform.childCount - 1; z >= 0; --z)
            {
                Transform child = other.transform.GetChild(z);
                Quaternion newRot = new Quaternion(90, newObject.transform.rotation.y, newObject.transform.rotation.z, 0);
                //Debug.Log("moving object: " + child.name);
                child.SetParent(newObject.transform, false);

                string childName = child.gameObject.name;

                switch (childName)
                {
                    case "Main Camera":
                        child.transform.position = new Vector3(newObject.transform.position.x, 10, newObject.transform.position.z);
                        child.transform.rotation = Quaternion.Lerp(child.transform.rotation, newRot, Time.deltaTime * 0.5f);
                        break;
                    case "Cube":
                        child.gameObject.SetActive(false);
                        break;
                    case "Player 1 Circle":
                        child.gameObject.SetActive(false);
                        break;

                    default:
                        break;
                }
            }
            other.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Hit me in the stomach!");
            playerControls.health = playerControls.health - 250;
            takePunch = true;
            anim.SetBool("TakePunch", takePunch);
        }
    }

    public void punchCheck(int hitCount)
    {
        Collider fistCollider;
        fistCollider = GetComponent<Collider>();

        if (hitCount == 1)
        {
            fistCollider.enabled = true;
        }
        else if (hitCount == 0)
        {
            fistCollider.enabled = false;
        }
    }

}
