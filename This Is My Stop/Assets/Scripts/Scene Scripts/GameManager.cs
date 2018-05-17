using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    [SerializeField]
    private GameObject[] exitPoints;

    private GameObject[] randomTag = new GameObject[3];

    private GameObject[][] teamsArray = new GameObject[4][];

    //private GameObject[] taggedTeams = new GameObject[16];

    private GameObject[] teamToHunt = new GameObject[4];

    private int targetDestroyed;

    private bool initializingTeams = true;

    private bool[] checkedActiveState = new bool[4];

    private bool movedScene;

    private Collision collision;
    // Use this for initialization
    void Start()
    {
        //StartCoroutine(teamInit());

        //Maybe make an GameObject Array?
        GameObject[] team1Chars = GameObject.FindGameObjectsWithTag("Team 1");
        GameObject[] team2Chars = GameObject.FindGameObjectsWithTag("Team 2");
        GameObject[] team3Chars = GameObject.FindGameObjectsWithTag("Team 3");
        GameObject[] team4Chars = GameObject.FindGameObjectsWithTag("Team 4");
        teamsArray[0] = team1Chars.OrderBy((x) => x.name).ToArray();
        teamsArray[1] = team2Chars.OrderBy((x) => x.name).ToArray();
        teamsArray[2] = team3Chars.OrderBy((x) => x.name).ToArray();
        teamsArray[3] = team4Chars.OrderBy((x) => x.name).ToArray();

        int randomDesIndex = UnityEngine.Random.Range(0, 3);

        if (initializingTeams)
        {
            StartCoroutine(teamInit());

            //Turn on a random destination point
            exitPoints[randomDesIndex].SetActive(true);


            initializingTeams = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(isTeamDead());

        if (targetDestroyed == teamToHunt.Length && movedScene == false)
        {
            //Collider charCollision = GetComponent<Collision>().collider;
            movedScene = true;
            WinningTeam winningTeam = GetComponent<WinningTeam>(); 

            //Load victory screen for player that killed the last team member
            SceneManager.LoadScene("HuntersWin", LoadSceneMode.Additive);
            Scene sceneToLoad = SceneManager.GetSceneByName("HuntersWin");
            SceneManager.MoveGameObjectToScene(winningTeam.gameObject, sceneToLoad);
            SceneManager.UnloadSceneAsync("MainScene");
        }
    }

    IEnumerator isTeamDead()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < teamToHunt.Length; i++)
        {
            if (teamToHunt[i].gameObject.activeSelf == false && checkedActiveState[i] == false)
            {
                targetDestroyed++;
                checkedActiveState[i] = true;
                collision = GetComponent<Collision>();
                Debug.Log(collision.gameObject);
            }
        }
    }

    IEnumerator teamInit()
    {
        yield return new WaitForSeconds(0.5f);

        int teamCount = 0;

        int playerCount = FindObjectsOfType<PlayerController>().Length;

        List<GameObject> huntedPlayers = new List<GameObject>();

        for (int j = 0; j < teamsArray.Length; j++)
        {
            if (teamsArray[j][0] != null)
            {
                teamCount++;
            }
        }


        GameObject[][] liveTeams = teamsArray.Where((team) => team[0] != null).ToArray();
        var randomTeamIndex = UnityEngine.Random.Range(0, liveTeams.Length - 1);

        for (int l = 0; l < 4; l++)
        {
            if(liveTeams[randomTeamIndex][0] != null)
            {
                huntedPlayers.Add(liveTeams[randomTeamIndex][l]);
                liveTeams[randomTeamIndex][l].gameObject.tag = "Hunted";
            }
            else
            {
                Debug.LogError("Invalid selected team");
            }

        }

        Debug.Log(huntedPlayers);



        teamToHunt = GameObject.FindGameObjectsWithTag("Hunted");


        //If that team makes it to the exit then they win!



    }
}
