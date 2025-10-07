using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointer : MonoBehaviour
{
    // References
    RawImage handImage;
    public bool canInteract;
    public GameObject itemHolding;

    void Start()
    {
        // Get references
        canInteract = false;
        handImage = GameObject.Find("Hand").GetComponent<RawImage>();
    }

    void OnTriggerStay(Collider other)
    {
        // Show the hand image when the pointer enters an interactable object's trigger
        if (other.gameObject.tag == "Interactable" && canInteract)
        {
            handImage.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Hide the hand image when the pointer exits an interactable object's trigger
        if (other.gameObject.tag == "Interactable" && canInteract)
        {
            handImage.enabled = false;
        }
    }

    void Update()
    {
        
    }
}
