using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    // References
    Interactable interactableComponent;
    PlayerPointer playerPointer;
    DoorSFX doorSFX;

    // Door settings
    [Header("Door Settings")]
    public float rotationYStart;
    public float rotationYOffset;
    public bool isLocked;
    public GameObject key;
    float scaleX;
    bool isOpen = false;

    void Start()
    {
        // Get references
        interactableComponent = GetComponent<Interactable>();
        playerPointer = GameObject.Find("Pointer").GetComponent<PlayerPointer>();
        doorSFX = GetComponent<DoorSFX>();

        // Set initial rotation and scale
        transform.localRotation = Quaternion.Euler(0, rotationYStart + rotationYOffset, 0);
        scaleX = transform.localScale.x;
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Jose" && !isLocked && !isOpen)
        {
            doorSFX.audioSource.PlayOneShot(doorSFX.doorOpen);
            isOpen = true;
        }
    }

    void Update()
    {
        // Toggle door state on mouse click if it can be opened
        if (Input.GetMouseButtonDown(0) && interactableComponent.interactable)
        {
            if (!isLocked)
            {
                if (isOpen)
                {
                    doorSFX.audioSource.PlayOneShot(doorSFX.doorClose);
                }
                else
                {
                    doorSFX.audioSource.PlayOneShot(doorSFX.doorOpen);
                }

                isOpen = !isOpen;
            }
            else
            {
                // Play appropriate sound effects based on door state
                if (isOpen)
                {
                    // Close the door
                    doorSFX.audioSource.PlayOneShot(doorSFX.doorClose);
                    isOpen = false;
                }
                else
                {
                    // Attempt to open a locked door
                    doorSFX.audioSource.PlayOneShot(doorSFX.doorLocked);
                }
            }
        }

        // Lock or unlock the door if the player is holding the correct key
        if (Input.GetMouseButtonDown(1) && interactableComponent.interactable && playerPointer.itemHolding == key)
        {
            isLocked = !isLocked;
            doorSFX.audioSource.PlayOneShot(doorSFX.doorLocked);
        }

        // Smoothly rotate the door to open or closed position
        if (isOpen)
        {
            // Rotate the door to open position
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, rotationYStart - (90 * scaleX), 0), Time.deltaTime * 10);
        }
        else
        {
            // Rotate the door to closed position
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, rotationYStart, 0), Time.deltaTime * 10);
        }
    }
}
