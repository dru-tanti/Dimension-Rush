using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Phase 2/LevelSettings", order = 0)]
public class LevelSettings : ScriptableObject 
{
    [Tooltip("The time it will take to switch dimensions in seconds")]
    public float switchTime;
    [Tooltip("The speed the player can move")]
    public float playerSpeed;
    
}