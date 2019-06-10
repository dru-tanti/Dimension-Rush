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
    public float switchTime;
    public EnemyChase[] chaseEnemiesInPast;

    // Sets the present time line on level start.
    private void Start() 
    {
        // Finds all instances of Enemy Chase that is in the layer "Past"
        chaseEnemiesInPast = GameObject.FindObjectsOfType<EnemyChase>().Where(enemy => enemy.gameObject.layer == LayerMask.NameToLayer("Past")).ToArray();
        StartCoroutine(DimensionSwitch());
    }


    public void Update()
    {
        // Changes the timeline by culling a layer from the camera render.
        if(Input.GetButtonDown("Fire2"))
        {
            SetTimeline();
        }

        if(inPast)
        {
            foreach(EnemyChase chase in chaseEnemiesInPast)
            {
                chase.gameObject.SetActive(false);
            }
        } else {
            foreach(EnemyChase chase in chaseEnemiesInPast)
            {
                chase.gameObject.SetActive(true);
            }
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            Camera.main.cullingMask = 1 | 1 <<  9  | 1 << 10;
        } else {
            // To add another layer to the culling mask add "| 1 << LayerNumber"
            Camera.main.cullingMask = 1 | 1 << (inPast ? 9 : 10) | 1 << 8;
        }
    }

    private void SetTimeline()
    {
        // Checks if inPast is true or false and determines what layer to cull
        inPast = !inPast;
     
        // If the player is in the past, disable any colliders in the layer "Present"
        Physics2D.IgnoreLayerCollision(0, 9, !inPast);
        Physics2D.IgnoreLayerCollision(0, 10, inPast);

        AudioManager.current.Play("Shift");
        CameraShaker.Instance.ShakeOnce(2f, 3f, 0.1f, 1f);
    }

    private IEnumerator DimensionSwitch()
    {
        while(true)
        {
            SetTimeline();
            yield return new WaitForSeconds(switchTime);
        }
    }
}
