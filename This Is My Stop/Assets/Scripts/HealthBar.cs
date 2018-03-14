using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    private Image content;

    private float playerHealth = 500f;

    PlayerController playerController = new PlayerController();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleBar();
    }

    private void HandleBar()
    {
        float barHealth = playerController.playerHealth(playerHealth);
        content.fillAmount = Map(barHealth, 0,500,0,1);
        //^^Update with player health value!
    }

    private float Map(float val, float inMin, float inMax, float outMin, float outMax)
    {
        return (val - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
