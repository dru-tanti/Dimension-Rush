using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    private bool levelFinished;
    public GameObject Portal;
    public Transform portalSpawn;


    private void Update() 
    {
        if(!levelFinished)
        {
            if(isFull[isFull.Length - 1] == true)
            {
                Invoke("EndLevel", 0.5f);
                levelFinished = true;
            }
        }

    }
    void EndLevel()
    {
         Instantiate(Portal, portalSpawn.transform, false);
         AudioManager.current.Play("EndLevel");
    }
}
