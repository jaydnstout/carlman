using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Variables to manage door state and rotation
    [Header("Door Settings")]
    float rotationYStart;
    bool touchingPointer = false;
    public bool isLocked;
    public GameObject key;
    bool isOpen = false;

    private void Start()
    {
        // Store the initial Y rotation of the door
        rotationYStart = transform.rotation.y;
    }

    void OnTriggerEnter(Collider other)
    {
        // Allow the door to be opened when the pointer enters the trigger
        if (other.gameObject.tag == "Pointer")
        {
            touchingPointer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Stop the door from being openable when the pointer leaves the trigger
        if (other.gameObject.tag == "Pointer")
        {
            touchingPointer = false;
        }
    }

    void Update()
    {
        // Toggle door state on mouse click if it can be opened
        if (Input.GetMouseButtonDown(0) && touchingPointer)
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
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, rotationYStart - 180, 0), Time.deltaTime * 5);
        }
        else
        {
            // Rotate the door to closed position
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, rotationYStart - 90, 0), Time.deltaTime * 5);
        }
    }
}
