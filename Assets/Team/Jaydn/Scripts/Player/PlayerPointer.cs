using UnityEngine;
using UnityEngine.UI;

public class PlayerPointer : MonoBehaviour
{
    // References
    GameObject collision;
    GameObject trigger;
    public bool canInteract;
    RawImage handImage;

    void Start()
    {
        // Get references
        canInteract = false;
        handImage = GameObject.Find("Hand").GetComponent<RawImage>();
    }

    void OnTriggerStay(Collider other)
    {
        // Show the hand image when the pointer enters an interactable object's trigger
        if (other.gameObject.tag == "Interactable" )
        {
            canInteract = true;
            handImage.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Hide the hand image when the pointer exits an interactable object's trigger
        if (other.gameObject.tag == "Interactable")
        {
            canInteract = false;
            handImage.enabled = false;
        }
    }
}
