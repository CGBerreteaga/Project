using System;
using UnityEngine;
using UnityEngine.UI;

public class UItem : MonoBehaviour
{
    public Image iconImage;  // Assign this in the Inspector
    public Text itemNameText;  // Assign this in the Inspector

    // Method to set the item details in the UI
    public void SetItem(ItemScriptableObject item)
    {
        if (item != null)
        {
            iconImage.sprite = item.itemIcon;  // Set the item's icon
            itemNameText.text = item.itemName;  // Set the item's name
        }
        else
        {
            iconImage.sprite = null;  // Clear the icon
            itemNameText.text = string.Empty;  // Clear the name
        }
    }

    
}
