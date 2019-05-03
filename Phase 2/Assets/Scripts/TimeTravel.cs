using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : MonoBehaviour
{
    public bool inPast = true;

    // Sets the present time line on level start.
    private void Start() 
    {
        SetTimeline();
    }

    public void Update()
    {
        // Changes the timeline by culling a layer from the camera render.
        if(Input.GetButtonDown("Fire2"))
        {
            SetTimeline();
        }
    }

    private void SetTimeline()
    {
        // Checks if inPast is true or false and determines what layer to cull
        inPast = !inPast;
        Camera.main.cullingMask = 1 | 1 << (inPast ? 9 : 10);

        // If the player is in the past, disable any colliders in the layer "Present"
        Physics2D.IgnoreLayerCollision(0, 9, !inPast);
        Physics2D.IgnoreLayerCollision(0, 10, inPast);
    }
}
