using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EZCameraShake;
using UnityEngine.Events;


public class TimeTravel : MonoBehaviour
{
    public bool inPast = true;
    public float switchTime;
    public EnemyChase[] chaseEnemiesInPast;
    [Tooltip("Set how many shifts the player has in this level")]
    public int dimensionShifts = 10;
    public int maxShifts = 10;
    public Image[] battery;
    public Sprite batteryFull;
    public Sprite batteryEmpty;

    public TextMeshProUGUI countdown;
    public float timeleft;
	public UnityAction TimelineShift;
	
    // Sets the present time line on level start.
    private void Start() 
    {
        // Finds all instances of Enemy Chase that is in the layer "Past"
        chaseEnemiesInPast = GameObject.FindObjectsOfType<EnemyChase>().Where(enemy => enemy.gameObject.layer == LayerMask.NameToLayer("Past")).ToArray();
        StartCoroutine(DimensionSwitch());
        timeleft = switchTime;
        Debug.Log(timeleft);
    }

    // Shows a countdown timer to the next dimensions change.
    void Countdown()
    {
        timeleft -= Time.deltaTime;
        if ( timeleft < 0 )
        {
            timeleft = switchTime;
        }
        countdown.SetText(Mathf.CeilToInt(timeleft).ToString());
    }

    void SwitchDimension()
    {
        // Changes the timeline by culling a layer from the camera render.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Checks of the player has any more dimenson shifts before switching dimensions.
            if(dimensionShifts > 0)
            {
                SetTimeline();
                dimensionShifts --;
            } else {
                AudioManager.current.Play("OutofShifts");
                Debug.Log("No more shifts remaining!");
            }
        }
    }

    void PrintUI()
    {
        // Caps the number of times the player can change dimensions
        if(dimensionShifts > maxShifts)
        {
            dimensionShifts = maxShifts;
        }

        // This controls UI to show how many shifts we have remaining
        for (int i = 0; i < battery.Length; i++)
        {
            if(i < dimensionShifts)
        {
            battery[i].sprite = batteryFull;
        } else {
            battery[i].sprite = batteryEmpty;
        }
            if (i < maxShifts)
            {
                battery[i].enabled = true;
            } else {
                battery[i].enabled = false;
            }
        }
    }

    void StopChasing()
    {
        
        // Swear to god this is the dumbest thing that I've ever seen work.
        if(inPast)
        {
            // Because apparently the AI get's confused if it moves a few units on the Z axis.
            foreach(EnemyChase chase in chaseEnemiesInPast)
            {
                chase.transform.position = new Vector3(chase.transform.position.x, chase.transform.position.y, -5);
                Physics2D.IgnoreLayerCollision(10, 9, inPast);
            }
        } else {
            // Moves them back in position and they can start working again.
            foreach(EnemyChase chase in chaseEnemiesInPast)
            {
                chase.transform.position = new Vector3(chase.transform.position.x, chase.transform.position.y, 0);
            }
        }
    }
    
    public void Update()
    {
        
        Countdown();
        SwitchDimension();
        PrintUI();
        StopChasing();

        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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

    // Automatically switches dimensions after a certain amount of time.
    private IEnumerator DimensionSwitch()
    {
        while(true)
        {
            SetTimeline();
            yield return new WaitForSeconds(switchTime);
        }
    }
}
