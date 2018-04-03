using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Controls", menuName = "Player Controls", order = 1)]
public class PlayerControlsSO : ScriptableObject
{
    public string horizontal;
    public string vertical;
    public string camHor;
    public int health;
    public KeyCode lightPunch;
    public KeyCode heavyPunch;
    public KeyCode kick;
    public KeyCode blocking;
    public KeyCode lobbyJoin;
    public KeyCode lobbyLeave;
    public Camera camera;
    public Transform playerTransform;
    public Transform playerRagdoll;
}
