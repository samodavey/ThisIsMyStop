﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour {

    /// <summary>
    /// Spawns a train model which slightly moves around to give the image of the engine running
    /// </summary>
    /// 
    [SerializeField]
    private GameObject trainToSpawn;
    GameObject trainInlevel;
    private float movementSpeed = 50.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsInvoking("SpawnTrain"))
        {    
            Invoke("SpawnTrain", 5);
        }
        if(trainInlevel != null)
        {
            trainInlevel.transform.position = new Vector3(this.transform.position.x - (Time.deltaTime * movementSpeed), trainInlevel.transform.position.y + (Mathf.Sin(Time.time * 10) * 0.005f), trainInlevel.transform.position.z + (Mathf.Sin(Time.time * 10) * 0.005f )  );
        }
        
    }

    void SpawnTrain()
    {
        Destroy(trainInlevel);
        trainInlevel = Instantiate(trainToSpawn, transform.position, transform.rotation, transform);
    }
}
