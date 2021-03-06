﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Collections;

public class LobbySystem : MonoBehaviour {

    /// <summary>
    /// Main lobby system, transfers players from one scene to another and culls the camera
    /// </summary>

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    private Image timerContent;

    [SerializeField]
    private SpriteRenderer instructionsSprite;

    public PlayerControlsSO[] playerControllers;

    private static int playersConnected;
    private static bool[] playerEntered = new bool[4];
    private bool awayFromTitle;

    private List<GameObject> panels = new List<GameObject>();
    private List<GameObject> panelItem = new List<GameObject>();

    private float timeLeft = 10.0f;
    private bool timerEnabled;
    private bool lockedIn;
    private int playerReadied = 0;

    private Quaternion newRotation = new Quaternion(0, 0, 0, 0);
    private Vector3 newVector = new Vector3(13.86f, 6.779f, -8.909f);

    // Use this for initialization
    void Start () {

        if (canvas != null)
        {
            int childCount = canvas.transform.childCount;

            for (int y = 0; y < childCount; y++)
            {
                GameObject canvasChild = canvas.transform.GetChild(y).gameObject;
                if (canvasChild.name.Contains("Panel"))
                {
                    panels.Add(canvasChild);
                    panelItem.Add(canvasChild.transform.GetChild(0).gameObject);
                    panelItem.Add(canvasChild.transform.GetChild(1).gameObject);
                    panelItem.Add(canvasChild.transform.GetChild(2).gameObject);
                    panelItem.Add(canvasChild.transform.GetChild(3).gameObject);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.JoystickButton7) && mainCamera.transform.localRotation.y != 180)
        {
            mainCamera.transform.rotation = newRotation;
            mainCamera.transform.position = newVector;
            awayFromTitle = true;
        }

        if (awayFromTitle == true)
        {
            StartCoroutine(instructionsFade());
        }

        for (int i = 0; i < playerControllers.Length; i++)
        {
            if (Input.GetKeyDown(playerControllers[i].lobbyJoin) && playerEntered[i] == false)
            {
                PlayerJoined(i);
            }
            else if (Input.GetKeyDown(playerControllers[i].lobbyLeave) && playerEntered[i] == true && lockedIn == false)
            {
                PlayerLeft(i);
            }
            else if (Input.GetKeyDown(playerControllers[i].lobbyReady) && playerEntered[i] == true && playersConnected > 1)
            {
                PlayerLocked(i);
                lockedIn = true;
                timerEnabled = true;
                playerReadied++;
            }

        }

        if (timerEnabled && playerReadied == playersConnected)
        {
            timeLeft -= Time.deltaTime;
            timeLeft = Mathf.Clamp(timeLeft, 0, 10);

            GameObject timer = canvas.transform.GetChild(6).gameObject;
            GameObject timerNumber = timer.transform.GetChild(0).gameObject;

            timer.SetActive(true);
            timerContent.fillAmount -= 0.1f * Time.deltaTime;
            timerNumber.GetComponent<Text>().text = timeLeft.ToString("F0");

            if (timeLeft == 0)
            {
                SceneManager.LoadScene("MainScene");
            }
        }


        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainScene"))
        {
            //Adjusts cameras dependant on which players are active within the main scene
            GameObject[] charactersToDeactivate;

            List<PlayerController> totalPlayers = new List<PlayerController>();
            for (int i = 1; i <= playerEntered.Length; i++)
            {
                int playerEnteredIndex = i - 1;
                PlayerController[] activePlayers = FindObjectsOfType<PlayerController>().OrderBy((x) => x.name).ToArray();


                if (playerEntered[playerEnteredIndex] == false)
                {
                    charactersToDeactivate = GameObject.FindGameObjectsWithTag("Team " + i);

                    for (int y = 0; y < charactersToDeactivate.Length; y++)
                    {
                        Destroy(charactersToDeactivate[y]);
                    }
                }
                else
                {
                    totalPlayers.Add(activePlayers[playerEnteredIndex]);
                }

            }
            switch (playersConnected)
            {
                case 2:
                    totalPlayers[0].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0.5f), new Vector2(1, 0.5f));
                    totalPlayers[1].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(1, 0.5f));   
                    break;
                case 3:
                    totalPlayers[0].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0.5f), new Vector2(1, 0.5f));
                    totalPlayers[1].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 0.5f));
                    totalPlayers[2].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 0.5f));
                    
                    break;

                default:
                    break;

            }
            DestroyImmediate(this.gameObject);
        }
    }

    private void PlayerLocked(int playerNumber)
    {
        //Player LOCKED
        switch (playerNumber)
        {
            case 0:
                if (panelItem[0])
                {
                    panelItem[2].SetActive(false);
                    panelItem[3].SetActive(true);
                }
                break;
            case 1:
                if (panelItem[2])
                {
                    panelItem[6].SetActive(false);
                    panelItem[7].SetActive(true);
                }
                break;
            case 2:
                if (panelItem[4])
                {
                    panelItem[10].SetActive(false);
                    panelItem[11].SetActive(true);
                }
                break;
            case 3:
                if (panelItem[6])
                {
                    panelItem[14].SetActive(false);
                    panelItem[15].SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    public void PlayerJoined(int playerNumber)
    {
        //Player connecting
        switch (playerNumber)
        {
            case 0:
                if (panelItem[0])
                {
                    panels[0].GetComponent<Image>().color = Color.green;
                    panelItem[0].SetActive(false);
                    panelItem[1].GetComponent<Text>().text = "Player 1 Connected \n Ready?";
                    panelItem[1].GetComponent<Text>().transform.localPosition = new Vector3(-13, 13, 0);
                    panelItem[2].SetActive(true);
                    playerEntered[playerNumber] = true;
                    playersConnected++;
                }
                break;
            case 1:
                if (panelItem[2])
                {
                    panels[1].GetComponent<Image>().color = Color.green;
                    panelItem[4].SetActive(false);
                    panelItem[5].GetComponent<Text>().text = "Player 2 Connected \n Ready?";
                    panelItem[5].GetComponent<Text>().transform.localPosition = new Vector3(-13, 13, 0);
                    panelItem[6].SetActive(true);
                    playerEntered[playerNumber] = true;
                    playersConnected++;
                }
                break;
            case 2:
                if (panelItem[4])
                {
                    panels[2].GetComponent<Image>().color = Color.green;
                    panelItem[8].SetActive(false);
                    panelItem[9].GetComponent<Text>().text = "Player 3 Connected \n Ready?";
                    panelItem[9].GetComponent<Text>().transform.localPosition = new Vector3(-13, 13, 0);
                    panelItem[10].SetActive(true);
                    playerEntered[playerNumber] = true;
                    playersConnected++;
                }
                break;
            case 3:
                if (panelItem[6])
                {
                    panels[3].GetComponent<Image>().color = Color.green;
                    panelItem[12].SetActive(false);
                    panelItem[13].GetComponent<Text>().text = "Player 4 Connected \n Ready?";
                    panelItem[13].GetComponent<Text>().transform.localPosition = new Vector3(-13, 13, 0);
                    panelItem[14].SetActive(true);
                    playerEntered[playerNumber] = true;
                    playersConnected++;
                }
                break;
            default:
                break;
        }
    }

    public void PlayerLeft(int playerNumber)
    {

        switch (playerNumber)
        {
            case 0:
                if (panelItem[0])
                {
                    panels[0].GetComponent<Image>().color = new Vector4(192, 57, 61, 1);
                    panelItem[0].SetActive(true);
                    panelItem[1].GetComponent<Text>().text = "Join";
                    panelItem[1].GetComponent<Text>().transform.localPosition = new Vector3(159.6f, -150, 0);
                    panelItem[2].SetActive(false);
                    playerEntered[playerNumber] = false;
                    playersConnected--;
                }
                break;
            case 1:
                if (panelItem[2])
                {
                    panels[1].GetComponent<Image>().color = new Vector4(192, 57, 61, 1);
                    panelItem[3].SetActive(true);
                    panelItem[4].GetComponent<Text>().text = "Join";
                    panelItem[4].GetComponent<Text>().transform.localPosition = new Vector3(159.6f, -150, 0);
                    panelItem[5].SetActive(false);
                    playerEntered[playerNumber] = false;
                    playersConnected--;
                }
                break;
            case 2:
                if (panelItem[4])
                {
                    panels[2].GetComponent<Image>().color = new Vector4(192, 57, 61, 1);
                    panelItem[6].SetActive(true);
                    panelItem[7].GetComponent<Text>().text = "Join";
                    panelItem[7].GetComponent<Text>().transform.localPosition = new Vector3(159.6f, -150, 0);
                    panelItem[8].SetActive(false);
                    playerEntered[playerNumber] = false;
                    playersConnected--;
                }
                break;
            case 3:
                if (panelItem[6])
                {
                    panels[3].GetComponent<Image>().color = new Vector4(192, 57, 61, 1);
                    panelItem[9].SetActive(true);
                    panelItem[10].GetComponent<Text>().text = "Join";
                    panelItem[10].GetComponent<Text>().transform.localPosition = new Vector3(159.6f, -150, 0);
                    panelItem[11].SetActive(false);
                    playerEntered[playerNumber] = false;
                    playersConnected--;
                }
                break;
            default:
                break;
        }

    }
    private IEnumerator instructionsFade()
    {
        yield return new WaitForSeconds(5);
        if (instructionsSprite != null)
        {
            instructionsSprite.color = new Color(instructionsSprite.color.r, instructionsSprite.color.g, instructionsSprite.color.b, instructionsSprite.color.a - (Time.deltaTime / 2));
        }
    }
}
