using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Holdable : MonoBehaviour
{
    Interactable interactableComponent;
    HoldableSFX holdableSFX;

    void Start()
    {
        interactableComponent = GetComponent<Interactable>();
        holdableSFX = GetComponent<HoldableSFX>();
    }

    void Update()
    {
        // Pick up the item if the player clicks while the key is interactable
        if (Input.GetMouseButtonDown(0) && interactableComponent.interactable && interactableComponent.pointer.itemHolding == null)
        {
            interactableComponent.pointer.itemHolding = gameObject;
            Debug.Log("Grabbable item grabbed");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            holdableSFX.audioSource.PlayOneShot(holdableSFX.dropSound);
        }
    }
}
