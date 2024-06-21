using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Item : MonoBehaviour
{
    //This is a 2D image for the item
    public Texture2D icon;
    //This is an event for using the item
    public Action<Item> onUse;
    //This is the prefab for the 3D item with the Pickup component
    public GameObject worldPrefab;
    //The amount that this item affects an attribute
    [SerializeField] float amount;
    public float Amount => amount;
    //The attribute that the item affects
    [SerializeField] protected AttributeScriptableObject attribute;
    public AttributeScriptableObject Attribute => attribute;
    //Determines what happens when using an item
    public abstract void Use(Player player);
}
