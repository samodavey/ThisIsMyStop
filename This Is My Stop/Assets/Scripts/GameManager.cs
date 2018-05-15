using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    [SerializeField]
    private GameObject[] exitPoints;

    private GameObject[] randomTag = new GameObject[3];

    private List<GameObject> taggedTeams = new List<GameObject>();

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

        var randomTeamIndex = Random.Range(1, 4);
        var randomDesIndex = Random.Range(0, 4);

        if (initializingTeams)
        {
            //yield return new WaitForEndOfFrame();
            //Maybe knock it down to one for loop?

            //FIX THIS!

            for (int i = 0; i < team1Chars.Length; i++)
            {
                if (team1Chars[i].gameObject.activeSelf)
                {
                    taggedTeams.Add(team1Chars[i].gameObject);
                }
            }
            for (int i = 0; i < team2Chars.Length; i++)
            {
                if (team2Chars[i].gameObject.activeSelf)
                {
                    taggedTeams.Add(team2Chars[i].gameObject);
                }
            }
            for (int i = 0; i < team3Chars.Length; i++)
            {
                if (team3Chars[i].gameObject.activeSelf)
                {
                    taggedTeams.Add(team3Chars[i].gameObject);
                }
            }
            for (int i = 0; i < team4Chars.Length; i++)
            {
                if (team4Chars[i].gameObject.activeSelf)
                {
                    taggedTeams.Add(team4Chars[i].gameObject);
                }
            }

            for (int i = 0; i < taggedTeams.Capacity; i++)
            {
                if (taggedTeams[i].gameObject.tag == "Team " + randomTeamIndex)
                {
                    teamToHunt.Add(taggedTeams[i]);
                }
            }

            for (int y = 0; y < teamToHunt.Capacity; y++)
            {
                teamToHunt[y].gameObject.tag = "Hunted";
            }

            //Turn on a random destination point
            exitPoints[randomDesIndex].SetActive(true);

            Debug.Log("You must hunt team " + randomTeamIndex);
            Debug.Log("Escaping team must reach train " + randomDesIndex);

            initializingTeams = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

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


    //IEnumerator teamInit()
    //{

    //}
}
