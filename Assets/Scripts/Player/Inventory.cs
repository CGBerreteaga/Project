using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] List<ItemScriptableObject> items = new();

    public Action<ItemScriptableObject> onAddItem, onUseItem, onRemoveItem;

    // Change the Add method to accept ItemScriptableObject
    public void Add(ItemScriptableObject item)
    {
        if (item != null)
        {
            items.Add(item);
            Debug.Log($"Item added: {item}");
            onAddItem?.Invoke(item);
        }
        else
        {
            Debug.LogError("Item is null.");
        }
    }

    public void Remove(ItemScriptableObject item)
    {
        if (items.Remove(item))
        {
            onRemoveItem?.Invoke(item);
        }
        else
        {
            Debug.LogError("Item not found in inventory.");
        }
    }
}
