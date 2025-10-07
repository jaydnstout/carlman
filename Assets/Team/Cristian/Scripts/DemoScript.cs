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
        inventoryManager.AddItem(itemsToPickUp[id]);
    }


    //fix ITEMSSSS
}
