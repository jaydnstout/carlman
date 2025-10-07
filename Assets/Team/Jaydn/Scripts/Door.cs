using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Door settings
    [Header("Door Settings")]
    public float rotationYStart;
    public bool isLocked;
    public GameObject key;

    float scaleX;
    bool interactable = false;
    bool isOpen = false;
    
    void Start()
    {
        // Set initial rotation and find the pointer object
        scaleX = transform.localScale.x;
    }

    void OnTriggerStay(Collider other)
    {
        // Allow the door to be opened when the pointer enters the trigger
        if (other.gameObject.tag == "Pointer")
        {
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Stop the door from being openable when the pointer leaves the trigger
        if (other.gameObject.tag == "Pointer")
        {
            interactable = false;
        }
    }

    void Update()
    {
        // Toggle door state on mouse click if it can be opened
        if (Input.GetMouseButtonDown(0) && interactable)
        {
            if (isLocked)
            {

            }
            else
            {
                isOpen = !isOpen;
            }
        }

        // Smoothly rotate the door to open or closed position
        if (isOpen)
        {
            // Rotate the door to open position
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, rotationYStart - (90 * scaleX), 0), Time.deltaTime * 5);
        }
        else
        {
            // Rotate the door to closed position
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, rotationYStart, 0), Time.deltaTime * 5);
        }
    }
}
