using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbySystem : MonoBehaviour {

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Canvas canvas;

    public PlayerControlsSO[] playerControllers;

    private int playersConnected;
    private bool[] playerEntered = new bool[4];
    private bool awayFromTitle;
    private List<GameObject> panels = new List<GameObject>();
    private List<GameObject> panelItem = new List<GameObject>();

    // Use this for initialization
    void Start () {

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
            }
        }

        //GameObject resetPanel = Instantiate(panels, new Vector3(panels[0].transform.position.x, panels[0].transform.position.y, -59.604f), Quaternion.identity);
        //resetPanel.transform.SetParent(canvas.transform, false);
        //resetPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.JoystickButton7) && mainCamera.transform.localRotation.y != 180)
        {
            //Maybe have the Camera lerp?
            mainCamera.transform.rotation = new Quaternion(0, 0, 0, 0);
            mainCamera.transform.position = new Vector3(13.86f, 6.779f, -8.909f);
            awayFromTitle = true;
        }

        for (int i = 0; i < playerControllers.Length; i++)
        {
            if (Input.GetKeyDown(playerControllers[i].lobbyJoin) && playerEntered[i] == false )
            {
                PlayerJoined(i);
            }
            else if (Input.GetKeyDown(playerControllers[i].lobbyLeave) && playerEntered[i] == true)
            {
                PlayerLeft(i);
                //print("Player " + i + " has left");
            }
            else if (Input.GetKeyDown(playerControllers[i].lobbyReady) && playerEntered[i] == true)
            {
                LoadGame(playersConnected);
            }

        }
        Debug.Log(playersConnected);
    }

    public void LoadGame(int playerCount)
    {
        //When all players are ready then load the game passing appropriate variables
        print("THERE ARE " + playerCount + " PLAYERS JOINING");

        SceneManager.LoadScene("MainScene");

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
                    panelItem[3].SetActive(false);
                    panelItem[4].GetComponent<Text>().text = "Player 2 Connected \n Ready?";
                    panelItem[4].GetComponent<Text>().transform.localPosition = new Vector3(-13, 13, 0);
                    panelItem[5].SetActive(true);
                    playerEntered[playerNumber] = true;
                    playersConnected++;
                }
                break;
            case 2:
                if (panelItem[4])
                {
                    panels[2].GetComponent<Image>().color = Color.green;
                    panelItem[6].SetActive(false);
                    panelItem[7].GetComponent<Text>().text = "Player 3 Connected \n Ready?";
                    panelItem[7].GetComponent<Text>().transform.localPosition = new Vector3(-13, 13, 0);
                    panelItem[8].SetActive(true);
                    playerEntered[playerNumber] = true;
                    playersConnected++;
                }
                break;
            case 3:
                if (panelItem[6])
                {
                    panels[3].GetComponent<Image>().color = Color.green;
                    panelItem[9].SetActive(false);
                    panelItem[10].GetComponent<Text>().text = "Player 4 Connected \n Ready?";
                    panelItem[10].GetComponent<Text>().transform.localPosition = new Vector3(-13, 13, 0);
                    panelItem[11].SetActive(true);
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
        //GET THIS WORKING FOR ALL OTHER PLAYERS
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
}
