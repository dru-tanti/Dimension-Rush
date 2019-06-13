using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private TimeTravel time;
    public int batterySize;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            time.dimensionShifts += batterySize;
            Destroy(gameObject);
            return;
        }
    }
}
