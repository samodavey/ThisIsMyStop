using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //public string[] tagArray;

    //private GameObject[] taggedTeams;

    [SerializeField]
    private Transform exitPoint;


    private GameObject[] randomTag = new GameObject[3];

    private List<GameObject> taggedTeams = new List<GameObject>();

    private List<GameObject> teamToHunt = new List<GameObject>();

    private int targetDestroyed;

    // Use this for initialization
    void Start()
    {
        //Maybe make an GameObject Array?
        GameObject[] team1Chars = GameObject.FindGameObjectsWithTag("Team 1");
        GameObject[] team2Chars = GameObject.FindGameObjectsWithTag("Team 2");
        GameObject[] team3Chars = GameObject.FindGameObjectsWithTag("Team 3");
        GameObject[] team4Chars = GameObject.FindGameObjectsWithTag("Team 4");

        //Maybe knock it down to one for loop?
        for (int i = 0; i < team1Chars.Length; i++)
        {
            taggedTeams.Add(team1Chars[i].gameObject);
        }
        for (int i = 0; i < team2Chars.Length; i++)
        {
            taggedTeams.Add(team2Chars[i].gameObject);
        }
        for (int i = 0; i < team3Chars.Length; i++)
        {
            taggedTeams.Add(team3Chars[i].gameObject);
        }
        for (int i = 0; i < team4Chars.Length; i++)
        {
            taggedTeams.Add(team4Chars[i].gameObject);
        }

        var randomIndex = Random.Range(1, 4);
        //string randomTag = taggedTeams[randomIndex].ToString();

        for (int i = 0; i < taggedTeams.Capacity; i++)
        {
            if (taggedTeams[i].gameObject.tag == "Team " + randomIndex)
            {
                teamToHunt.Add(taggedTeams[i]);
            }
        }
        //Debug.Log(teamToHunt);
    }

    // Update is called once per frame
    void Update()
    {
        //NEED TO GIVE THE AI SOME HEALTH!
        //MAKE THE HEALTH RINGS
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
            Debug.Log("");

        }

        //If that team makes it to the exit then they win!

    }
}
