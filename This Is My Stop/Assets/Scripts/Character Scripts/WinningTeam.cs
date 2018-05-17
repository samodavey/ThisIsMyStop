using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningTeam : MonoBehaviour {

    public string winnner;

    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
