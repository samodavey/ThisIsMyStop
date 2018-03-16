using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Controls", menuName = "Player Controls", order = 1)]
public class PlayerControlsSO : ScriptableObject
{
    public string horizontal;
    public string vertical;
}
