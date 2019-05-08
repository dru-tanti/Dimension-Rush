using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int points;
    public Text scoreUI;

    private void Update() 
    {
        scoreUI.text = ("Score: " + points);
    }
}
