using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    //code for the inv slots
    public void OnDrop(PointerEventData eventData)
    {
        //lets you change the item into a different inventory slot
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;

        }

    }
   



}
