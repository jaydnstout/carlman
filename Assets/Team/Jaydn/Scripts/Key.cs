using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Door settings
    [Header("Key Settings")]
    public string keyName;
    bool interactable = false;
    public bool playerHas = false;
    PlayerPointer pp;

    void Start()
    {
        pp = GameObject.Find("Pointer").GetComponent<PlayerPointer>();
    }

    void OnTriggerStay(Collider other)
    {
        // Allow the key to be picked up when the pointer enters the trigger
        if (other.gameObject.tag == "Pointer")
        {
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Stop the key from being pickable when the pointer leaves the trigger
        if (other.gameObject.tag == "Pointer")
        {
            interactable = false;
        }
    }

    void Update()
    {
        // Pick up the key if the player clicks while the key is interactable
        if (Input.GetMouseButtonDown(0) && interactable)
        {
            pp.itemHolding = gameObject;
        }
    }
}
