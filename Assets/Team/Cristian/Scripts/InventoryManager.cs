using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
  
    
    
    //selected slot colors and changing slots with number keys 1, 2, 3 - cristian
    int selectedSlot = -1;
    //last part 26:45
    private void Start()
    {
        ChangeSelectedSlot(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
        } ChangeSelectedSlot(0);{
          } ChangeSelectedSlot(1);{
        } ChangeSelectedSlot(2); {
       
        }
    }
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }


    public bool AddItem(Item item)
    {
        //finds the empty slot- cristian
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;  
            }

        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }



}
