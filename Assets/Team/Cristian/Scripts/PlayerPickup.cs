using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public KeyCode pickupKey = KeyCode.E;
    public float pickupRange = 3f;

    private ItemHolder itemHolder;

    void Start()
    {
        itemHolder = GetComponentInChildren<ItemHolder>();
    }

    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            TryPickupItem();
        }
    }

    void TryPickupItem()
    {
        // Cast a ray forward from the camera or player to find items
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, pickupRange))
        {
            ItemNames itemNames = hit.collider.GetComponent<ItemNames>();
            if (itemNames != null)
            {
                // Call the ItemHolder to manage the item
                itemHolder.SetHeldItem(itemNames.gameObject);
            }
        }
    }
}