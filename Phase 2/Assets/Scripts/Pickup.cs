﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject item;

    private void Start() 
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    // ITEM CAN BE PICKED UP AND ADDED TO INVENTORY
                    inventory.isFull[i] = true;
                    Instantiate(item, inventory.slots[i].transform, false);

                    FindObjectOfType<AudioManager>().Play("Pickup");
                    Destroy(gameObject);
                    break;
                }
            }

        }
    }
}
