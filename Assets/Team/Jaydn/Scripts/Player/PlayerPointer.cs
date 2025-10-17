using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointer : MonoBehaviour
{
    // References
    RawImage handImage;
    public bool canInteract;
    public GameObject itemHolding;
    PlayerSFX playerSFX;
    AudioSource audioSource;

    void Start()
    {
        // Get references
        handImage = GameObject.Find("Hand").GetComponent<RawImage>();
        playerSFX = GameObject.Find("Player").GetComponent<PlayerSFX>();
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    void OnTriggerStay(Collider other)
    {
        Interactable interactableComponent = other.gameObject.GetComponent<Interactable>();

        // Show the hand image when the pointer enters an interactable object's trigger
        if (other.gameObject.tag == "Interactable" && canInteract && interactableComponent.interactable)
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
        // Check if the player is holding an item
        if (itemHolding != null)
        {
            Interactable interactableComponent = itemHolding.GetComponent<Interactable>();
            interactableComponent.interactable = false;
            Rigidbody ihRigidbody = itemHolding.GetComponent<Rigidbody>();
            ihRigidbody.useGravity = false;
            ihRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            

            // Throw the item when Q is pressed
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ihRigidbody.useGravity = true;
                ihRigidbody.constraints = RigidbodyConstraints.None;
                ihRigidbody.linearVelocity = (transform.forward * 20) + (transform.up * 5);
                itemHolding = null;
                audioSource.PlayOneShot(playerSFX.jumpSound, 6f);
            }
        }
    }

    private void FixedUpdate()
    {
        itemHolding.transform.position = Vector3.Lerp
            (
                itemHolding.transform.position,
                transform.position - (transform.forward * 0.5f) + (transform.right * 0.5f) - (transform.up * 0.25f),
                Time.deltaTime * 20f
            );

        itemHolding.transform.rotation = Quaternion.Lerp
        (
            itemHolding.transform.rotation,
            transform.rotation * Quaternion.Euler(-90, 90, 0),
            Time.deltaTime * 20f
        );
    }
}
