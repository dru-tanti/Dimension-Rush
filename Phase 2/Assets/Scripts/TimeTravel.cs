using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : MonoBehaviour
{
    bool inPast = true;

    public void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            if(!inPast)
            {
                Camera.main.cullingMask = 1 | 1 << 10;
                inPast = true;
            } else
            {
                Camera.main.cullingMask = 1 | 1 << 9;
                inPast = false;
            }
        }
    }
}
