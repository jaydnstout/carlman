using UnityEngine;
using TMPro; // Important for using TextMeshPro

public class ItemHolder : MonoBehaviour
{
    // Reference to the UI Text component
    public TextMeshProUGUI itemNameText;

    // Reference to the currently held item
    private GameObject currentHeldItem;

    public void SetHeldItem(GameObject newItem)
    {
        // Drop the old item if one exists
        if (currentHeldItem != null)
        {
            Destroy(currentHeldItem);
        }

        // Set the new item
        currentHeldItem = newItem;
        if (currentHeldItem != null)
        {
            // Display the new item's name on the UI
            itemNameText.text = newItem.name;

            // Make the new item a child of this holder for proper positioning
            currentHeldItem.transform.SetParent(this.transform);
        }
        else
        {
            // If no item is held, clear the text
            itemNameText.text = "";
        }
    }
}
