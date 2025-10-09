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
        // Check if the player is holding an item
        if (itemHolding != null)
        {
            itemHolding.GetComponent<Rigidbody>().useGravity = false;
            itemHolding.transform.position = Vector3.Lerp(itemHolding.transform.position, transform.localPosition + (Vector3.forward * 0.5f), Time.deltaTime * 10);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                itemHolding.GetComponent<Rigidbody>().useGravity = true;
                itemHolding = null;
            }
        }
    }
}
