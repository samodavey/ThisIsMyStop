using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    [SerializeField]
    private GameObject[] exitPoints;

    private GameObject[] randomTag = new GameObject[3];

    private GameObject[][] teamsArray = new GameObject[4][];

    //private GameObject[] taggedTeams = new GameObject[16];

    private List<GameObject> teamToHunt = new List<GameObject>();

    private int targetDestroyed;

    private bool initializingTeams = true;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(teamInit());

        //Maybe make an GameObject Array?
        GameObject[] team1Chars = GameObject.FindGameObjectsWithTag("Team 1");
        GameObject[] team2Chars = GameObject.FindGameObjectsWithTag("Team 2");
        GameObject[] team3Chars = GameObject.FindGameObjectsWithTag("Team 3");
        GameObject[] team4Chars = GameObject.FindGameObjectsWithTag("Team 4");
        teamsArray[0] = team1Chars;
        teamsArray[1] = team2Chars;
        teamsArray[2] = team3Chars;
        teamsArray[3] = team4Chars;

        var randomDesIndex = Random.Range(0, 4);

        if (initializingTeams)
        {
            //FIX THIS!

            StartCoroutine(teamInit());

            //Turn on a random destination point
            exitPoints[randomDesIndex].SetActive(true);

            //Debug.Log("You must hunt team " + randomTeamIndex);
            //Debug.Log("Escaping team must reach train " + randomDesIndex);

            initializingTeams = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(teamsArray);

        for (int i = 0; i < teamToHunt.Capacity; i++)
        {
            if (teamToHunt[i].activeSelf == false)
            {
                targetDestroyed++;
            }
        }

        if (targetDestroyed == teamToHunt.Capacity)
        {
            //Load victory screen for player that killed the last team member
            Debug.Log("YOU WIN");

        }

        //If that team makes it to the exit then they win!

    }


    IEnumerator teamInit()
    {
        yield return new WaitForSeconds(0.5f);

        int teamCount = 0;

        int playerCount = FindObjectsOfType<PlayerController>().Length;

        for (int j = 0; j < playerCount; j++)
        {
            if(teamsArray[j][0].gameObject.activeSelf)
            {
                teamCount++;
            }
        }


        var randomTeamIndex = Random.Range(1, teamCount);

        //for (int i = 0; i < teamsArray.Length; i++)
        //{
        //    if (teamsArray[i][0].gameObject.tag == "Team " + randomTeamIndex)
        //    {
                for(int j = 0; j < teamsArray[randomTeamIndex].Length; j++)
                {
                    //teamToHunt.Add(teamsArray[i][j]);
                    teamsArray[randomTeamIndex][j].gameObject.tag = "Hunted";
                }
                //break;
          //  }
       // }
    }
}
