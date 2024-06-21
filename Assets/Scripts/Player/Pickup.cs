using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pickup : MonoBehaviour
{
    // Serialized field to specify the item type in the Inspector
    [SerializeField] private EnumsScriptableObject item;
    

    // This method is called when another collider enters the trigger collider attached to the object this script is attached to
    private void OnTriggerEnter(Collider other)
    {
        // Try to get the Inventory component from the GameObject that entered the trigger
        var inventory = other.GetComponentInChildren<Inventory>();
        
        // If an Inventory component is found
        if (inventory != null)
        {
            // Add the item to the inventory
            inventory.Add(item);

            // Destroy the item GameObject after picking it up
            Destroy(gameObject);
        }
    }
}