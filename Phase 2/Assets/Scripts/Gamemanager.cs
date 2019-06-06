using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Defines the AudioManager as a singleton.
    public static GameManager current { get; private set; }

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
