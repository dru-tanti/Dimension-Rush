using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Inventory))]
public class Pickup : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    public GameObject item;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                
                if (inventory.isFull[i] == false)
                {
                    // Sets inventory slot as true
                    inventory.isFull[i] = true;
                    // Creates a copy of the item in the UI
                    Instantiate(item, inventory.slots[i].transform, false);

                    AudioManager.current.Play("Pickup");
                    Destroy(gameObject);
                    break;
                }
            }

        }
    }
}
