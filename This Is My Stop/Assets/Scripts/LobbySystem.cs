using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbySystem : MonoBehaviour {

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    private GameObject panelPrefab;

    public PlayerControlsSO[] playerControllers;

    private int playersConnected;
    private bool[] playerEntered = new bool[4];
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
            mainCamera.transform.rotation = new Quaternion(0, 0, 0, 0);
            mainCamera.transform.position = new Vector3(13.86f, 6.779f, -8.909f);
        }

        for (int i = 0; i < playerControllers.Length; i++)
        {
            if (Input.GetKeyDown(playerControllers[i].lobbyJoin) && playerEntered[i] == false )
            {
                //Player connecting
                switch (i)
                {
                    case 0:
                        if(panelItem[0])
                        {
                            panels[0].GetComponent<Image>().color = Color.green;
                            panelItem[0].SetActive(false);
                            panelItem[1].GetComponent<Text>().text = "Player 1 Connected \n Ready?";
                            panelItem[1].GetComponent<Text>().transform.Translate(new Vector3(0.5f,0.5f,0));
                            playerEntered[i] = true;
                            playersConnected++;
                        }
                        break;
                    case 1:
                        if (panelItem[2])
                        {
                            panels[1].GetComponent<Image>().color = Color.green;
                            panelItem[2].SetActive(false);
                            panelItem[3].GetComponent<Text>().text = "Player 2 Connected \n Ready?";
                            panelItem[3].GetComponent<Text>().transform.Translate(new Vector3(0.5f, 0.5f, 0));
                            playerEntered[i] = true;
                            playersConnected++;
                        }
                        break;
                    case 2:
                        if (panelItem[4])
                        {
                            panels[2].GetComponent<Image>().color = Color.green;
                            panelItem[4].SetActive(false);
                            panelItem[5].GetComponent<Text>().text = "Player 3 Connected \n Ready?";
                            panelItem[5].GetComponent<Text>().transform.Translate(new Vector3(0.5f, 0.5f, 0));
                            playerEntered[i] = true;
                            playersConnected++;
                        }
                        break;
                    case 3:
                        if (panelItem[6])
                        {
                            panels[3].GetComponent<Image>().color = Color.green;
                            panelItem[6].SetActive(false);
                            panelItem[7].GetComponent<Text>().text = "Player 4 Connected \n Ready?";
                            panelItem[7].GetComponent<Text>().transform.Translate(new Vector3(0.5f, 0.5f, 0));
                            playerEntered[i] = true;
                            playersConnected++;
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (Input.GetKeyDown(playerControllers[i].lobbyLeave) && playerEntered[i] == true)
            {
                //Reset Panel to original state (Player left)
                playersConnected--;
                playerEntered[i] = false;
                //UnityEditor.PrefabUtility.ResetToPrefabState(panels[i]);

                GameObject newPanel = Instantiate(panelPrefab, new Vector3(panels[i].transform.position.x, panels[i].transform.position.y, -59.604f), Quaternion.identity);
                newPanel.transform.SetParent(canvas.transform, false); 
                Destroy(panels[i]);
                print("Player " + i + " has left");
            }

            Debug.Log(playersConnected);
        }

    }

    public void LoadGame()
    {
        //When all players are ready then load the game passing appropriate variables
    }
}
