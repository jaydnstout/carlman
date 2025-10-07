using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{

   
    //all this gives the items actions and help me sort them- cristian
    [Header("Only gameplay")]
    public TileBase tile;
    public ItemType type;
    public ActionType actionType;
    
    [Header("Only UI")]
    
    
    [Header("Both")]
    public Sprite image;


    public enum ItemType
    {
        Flashlight,
        Consumable,
        Key
    }

    public enum ActionType{

        Flash,
        Heal,
        UnlockDoors

         }


  }
