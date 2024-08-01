using UnityEngine;
using UnityEngine.UI;

public class UInventory : MonoBehaviour
// The UInventory class will handle updating the UI when items are added.
// I made sure the 'onAddItem' event from the 'Inventory'
{
    public GameObject uItemPrefab;  // Assign this in the Inspector
    public GridLayoutGroup gridLayoutGroup;  // Assign this in the Inspector

    private void Start()
    {
        var inventory = FindObjectOfType<Inventory>();  // Ensure this correctly finds the Inventory component
        if (inventory != null)
        {
            inventory.onAddItem += UpdateUI;
        }
        else
        {
            Debug.LogError("Inventory component not found.");
        }
    }

    private void OnDestroy()
    {
        var inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            inventory.onAddItem -= UpdateUI;  // Unsubscribe from the event when this object is destroyed
        }
    }

    private void UpdateUI(ItemScriptableObject itemScriptableObject)
    {

        GameObject newUItem = Instantiate(uItemPrefab, gridLayoutGroup.transform);

        
            
            newUItem.GetComponent<UItem>().SetItem(itemScriptableObject);
        
    }
}
