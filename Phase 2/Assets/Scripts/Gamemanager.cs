// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class GameManager : MonoBehaviour
// {
//     // Defines the GameManager as a singleton.
//     public static GameManager current { get; private set; }

//     [Header("Level Settings")]
//     public int startingLevel = 1;
//     private int _currentLevel;
//     public LevelSettings[] settings;
//     private TimeTravel timeTravel;
//     private PlayerControl player;

//     void Awake()
//     {
//         // Check that the instance for GameManager exists, if not set to this class.
//         if (current == null)
//         {
//             current = this;
//             DontDestroyOnLoad(gameObject);
//         } else {
//             DestroyImmediate(gameObject);
//             return;
//         }

//         player = GetComponent<PlayerControl>();
//         timeTravel = GetComponent<TimeTravel>();
//     }

//     void Start()
//     {
//         if(settings.Length == 0)
//         {
//             Debug.Log("No Settings Found!");
//             enabled = false;
//             return;
//         }
//     }

//     public void StartGame()
//     {
//         _currentLevel = startingLevel;
//         if(_currentLevel > settings.Length) _currentLevel = 1;
//         player.SetSettings(settings[_currentLevel - 1]);
//         timeTravel.SetSettings(settings[_currentLevel - 1]);
//     }

//     public void NextLevel()
//     {
//         _currentLevel++;
//         if(_currentLevel > settings.Length) _currentLevel = 1;
//         player.SetSettings(settings[_currentLevel - 1]);
//         timeTravel.SetSettings(settings[_currentLevel - 1]);
//     }
// }