using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EZCameraShake;


public class TimeTravel : MonoBehaviour
{
    private bool holdDimension;

    //TODO: Find and add rule tiles to the scene
    public bool inPast = true;

    public EnemyChase[] chaseEnemiesInPast;

    // Sets the present time line on level start.
    private void Start() 
    {
        // Finds all instances of Enemy Chase that is in the layer "Past"
        chaseEnemiesInPast = GameObject.FindObjectsOfType<EnemyChase>().Where(enemy => enemy.gameObject.layer == LayerMask.NameToLayer("Past")).ToArray();
        OpenRift();
    }

    public void OpenRift()
    {
        StartCoroutine(DimensionSwitch());
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
        // To add another layer to the culling mask add "| 1 << LayerNumber"
        Camera.main.cullingMask = 1 | 1 << (inPast ? 9 : 10) | 1 << 8;

        // If the player is in the past, disable any colliders in the layer "Present"
        Physics2D.IgnoreLayerCollision(0, 9, !inPast);
        Physics2D.IgnoreLayerCollision(0, 10, inPast);

        AudioManager.current.Play("Shift");
        CameraShaker.Instance.ShakeOnce(2f, 3f, 0.1f, 1f);
    }

    private IEnumerator DimensionSwitch()
    {
        while(holdDimension == false)
        {
            SetTimeline();
            yield return new WaitForSeconds(5f);
        }
    }
}
