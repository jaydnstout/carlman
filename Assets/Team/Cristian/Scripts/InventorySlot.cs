using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    // colors for selected item - cristian
    public Image image;
    public Color selectedColor, notSelectedColor;
    
    private void Awake()
    {
        Deselect();
    }
    public void Select()
    {
        image.color = selectedColor;
    }
    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    //code for the inv slots
    public void OnDrop(PointerEventData eventData)
    {
        //lets you change the item into a different inventory slot- cristian
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;

        }
    }
}
