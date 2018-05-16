using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(teamInit());

        //Maybe make an GameObject Array?
        GameObject[] team1Chars = GameObject.FindGameObjectsWithTag("Team 1");
        GameObject[] team2Chars = GameObject.FindGameObjectsWithTag("Team 2");
        GameObject[] team3Chars = GameObject.FindGameObjectsWithTag("Team 3");
        GameObject[] team4Chars = GameObject.FindGameObjectsWithTag("Team 4");
        teamsArray[0] = team1Chars.OrderBy((x) => x.name).ToArray(); ;
        teamsArray[1] = team2Chars.OrderBy((x) => x.name).ToArray(); ;
        teamsArray[2] = team3Chars.OrderBy((x) => x.name).ToArray(); ;
        teamsArray[3] = team4Chars.OrderBy((x) => x.name).ToArray(); ;

        var randomDesIndex = Random.Range(0, 4);

        if (initializingTeams)
        {
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

        teamToHunt = GameObject.FindGameObjectsWithTag("Hunted");

        for (int i = 0; i < teamToHunt.Length; i++)
        {
            if (teamToHunt[i].activeSelf == false)
            {
                targetDestroyed++;
            }
        }

        if (targetDestroyed == teamToHunt.Length)
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

        List<GameObject> finalPlayers = new List<GameObject>();

        for (int j = 0; j < teamsArray.Length; j++)
        {
            if (teamsArray[j][0] != null)
            {
                teamCount++;
            }
        }


        var randomTeamIndex = Random.Range(0, teamCount);


        for(int l = 0; l < 4; l++)
        {
            finalPlayers.Add(teamsArray[randomTeamIndex][l]);
            finalPlayers[l].gameObject.tag = "Hunted";
        }

        Debug.Log(finalPlayers);

    }
}
