using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; 
    public Item[] startItem;

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;




    private void Awake()
    {
        Instance = this;
    }

    //start item(flashlight

    public void Start()
    {
        foreach (var item in startItem)
        {
            AddItem(item);
        }
    }


    //selected slot colors and changing slots with number keys 1, 2, 3 - cristian
    int selectedSlot = -1;
    
    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 4)
            {
                ChangeSelectedSlot(number - 1);
            }
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

    public Item GetSelectedItem( )
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            return itemInSlot.item;
        }

        return null;
    }




}
