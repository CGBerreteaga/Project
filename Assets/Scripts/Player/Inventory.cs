using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] List<EnumsScriptableObject> items = new();

    public Action<EnumsScriptableObject> onAddItem, onUseItem, onRemoveItem;
    public void Add(EnumsScriptableObject item)
    {
        items.Add(item);
        Debug.Log(item);
        //onAddItem?.Invoke(item);
    }

    public void Remove(EnumsScriptableObject item)
    {
        items.Remove(item);
        //onRemoveItem?.Invoke(item);
    }

    internal void Add(object item)
    {
        throw new NotImplementedException();
    }

    // public void Use(Item item)
    // {
    //     item.Use(player);
    //     onUseItem?.Invoke(item);
    // }
}

