using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : MonoBehaviour
{
    public bool inPast = true;
    //public GameObject Past;
    //public GameObject Present;

    private void Start() 
    {
        SetTimeline();
    }

    public void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            SetTimeline();
        }
    }

    private void SetTimeline()
    {
        inPast = !inPast;
        Camera.main.cullingMask = 1 | 1 << (inPast ? 9 : 10);

        Physics2D.IgnoreLayerCollision(0, 9, !inPast);
        Physics2D.IgnoreLayerCollision(0, 10, inPast);
    }
}
