using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] music = new AudioClip[4];

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private GameObject[] exitPoints;

    private GameObject[] randomTag = new GameObject[3];

    private GameObject[][] teamsArray = new GameObject[4][];

    //private GameObject[] taggedTeams = new GameObject[16];

    private GameObject[] teamToHunt = new GameObject[4];

    private TextMesh[] playerRole;

    private int targetDestroyed;

    private bool initializingTeams = true;

    private bool[] checkedActiveState = new bool[4];

    private bool movedScene = false;

    private float timePassed;

    // Use this for initialization
    void Start()
    {

        PlayNextSong();

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

        StartCoroutine(isTeamDead());

    }

    // Update is called once per frame
    void Update()
    {

        if(targetDestroyed == teamToHunt.Length && movedScene == false)
        {
            movedScene = true;
            WinningTeam winningTeam = GetComponent<WinningTeam>();

            //Load victory screen for player that killed the last team member
            SceneManager.LoadScene("HuntersWin", LoadSceneMode.Additive);
            Scene sceneToLoad = SceneManager.GetSceneByName("HuntersWin");
            SceneManager.MoveGameObjectToScene(winningTeam.gameObject, sceneToLoad);
            SceneManager.UnloadSceneAsync("MainScene");
        }



        timePassed += Time.deltaTime;

        if(playerRole != null)
        {
            for (int i = 0; i < playerRole.Length; i++)
            {
                if (playerRole[i] != null)
                {
                    playerRole[i].color = new Color(playerRole[i].color.r, playerRole[i].color.g, playerRole[i].color.b, playerRole[i].color.a - (Time.deltaTime / 7.5f));
                }
                else
                {
                    return;
                }
                if (timePassed >= 10)
                {
                    Destroy(playerRole[i]);
                }
            }
        }

        if (targetDestroyed == teamToHunt.Length && movedScene == false)
        {
            //movedScene = true;
            //WinningTeam winningTeam = GetComponent<WinningTeam>(); 

            ////Load victory screen for player that killed the last team member
            //SceneManager.LoadScene("HuntersWin", LoadSceneMode.Additive);
            //Scene sceneToLoad = SceneManager.GetSceneByName("HuntersWin");
            //SceneManager.MoveGameObjectToScene(winningTeam.gameObject, sceneToLoad);
            //SceneManager.UnloadSceneAsync("MainScene");
        }
    }

    private void PlayNextSong()
    {
        audioSource.clip = music[UnityEngine.Random.Range(0, music.Length)];
        audioSource.Play();
        Invoke("PlayNextSong", audioSource.clip.length + 2);
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
            }
        }
        StartCoroutine(isTeamDead());
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

        teamToHunt = GameObject.FindGameObjectsWithTag("Hunted");

        playerRole = FindObjectsOfType<TextMesh>();
        //DISPLAY TEXT FOR PLAYERS, LASTS A FEW SECONDS BEFORE REMOVAL
        for (int k = 0; k < playerRole.Length; k++)
        {
            if(playerRole[k].gameObject.transform.parent.tag == "Hunted")
            {
                playerRole[k].text = "YOU ARE THE HUNTED! \n RUN TO YOUR TRAIN HOME!";
            }
            else
            {
                playerRole[k].text = "YOU ARE A HUNTER! \n CHASE DOWN THE HUNTED!";
            }
        }



    }
}
