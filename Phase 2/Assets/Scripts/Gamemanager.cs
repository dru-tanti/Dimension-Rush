using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Defines the GameManager as a singleton.
    public static GameManager current { get; private set; }

    [Header("Level Settings")]
    public int startingLevel = 1;
    private int _currentLevel;
    void Awake()
    {
        // Check that the instance for GameManager exists, if not set to this class.
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        } else {
            DestroyImmediate(gameObject);
            return;
        }


    }


}