using UnityEngine;
using UnityEngine.UI;

public class PlayerPointer : MonoBehaviour
{
    // Reference to the hand image UI element
    RawImage handImage;
    
    void Start()
    {
        // Find the hand image in the scene and get its RawImage component
        handImage = GameObject.Find("Hand").GetComponent<RawImage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Show the hand image when the pointer enters an interactable object's trigger
        if (other.gameObject.tag == "Interactable")
        {
            handImage.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Hide the hand image when the pointer exits an interactable object's trigger
        if (other.gameObject.tag == "Interactable")
        {
            handImage.enabled = false;
        }
    }
}
