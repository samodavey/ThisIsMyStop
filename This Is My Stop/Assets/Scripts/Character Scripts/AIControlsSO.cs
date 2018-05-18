using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Controls", menuName = "AI Controls", order = 2)]
public class AIControlsSO : ScriptableObject
{
    public int health;
    public Transform aiRagdoll;
}