using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DemoScript : MonoBehaviour
{

    // test for inv being full-cristian
    public InventoryManager inventoryManager;
    public Item[] itemsToPickUp;


    public void PickUpItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickUp[id]);
        if (result == true)
        {
            Debug.Log("Item Added");
        }
        else
        {
            Debug.Log("Item NOT Added");
        }
    }


}
