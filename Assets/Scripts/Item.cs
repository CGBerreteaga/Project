using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite icon;  // Make these public to allow access from UItem
    public string itemName;

    public virtual void Use(Player player)
    {
        // Default implementation or abstract method
         Debug.Log($"{itemName} used.");
    }
}
