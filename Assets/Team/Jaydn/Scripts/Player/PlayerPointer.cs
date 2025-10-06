using UnityEngine;
using UnityEngine.UI;

public class PlayerPointer : MonoBehaviour
{
    // References
    GameObject collision;
    GameObject trigger;
    GameObject camera;
    bool canInteract;
    RawImage handImage;

    void Start()
    {
        // Get references
        camera = GameObject.Find("Main Camera");
        canInteract = false;
        handImage = GameObject.Find("Hand").GetComponent<RawImage>();
    }

    void OnCollisionStay(Collision other)
    {
        // Store the object the pointer is colliding with
        collision = other.gameObject;
    }

    void OnCollisionExit(Collision other)
    {
        // Clear the collision reference when the pointer exits the collision
        collision = null;
    }

    void OnTriggerStay(Collider other)
    {
        // Store the object whose trigger the pointer is inside
        trigger = other.gameObject;
        float distance1 = Vector3.Distance(camera.transform.position, collision.transform.position);
        float distance2 = Vector3.Distance(camera.transform.position, trigger.transform.position);

        // Show the hand image when the pointer enters an interactable object's trigger
        if (trigger.tag == "Interactable" && distance2 < distance1)
        {
            canInteract = true;
            handImage.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Clear the trigger reference when the pointer exits the trigger
        trigger = null;

        // Hide the hand image when the pointer exits an interactable object's trigger
        if (other.gameObject.tag == "Interactable")
        {
            canInteract = false;
            handImage.enabled = false;
        }
    }
}
