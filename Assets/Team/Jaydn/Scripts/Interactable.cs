using UnityEngine;

public class Interactable : MonoBehaviour
{
    public PlayerPointer pointer;
    public bool interactable = false;

    void Start()
    {
        pointer = GameObject.Find("Pointer").GetComponent<PlayerPointer>();
    }

    void OnTriggerStay(Collider other)
    {
        // Allow the item to be interacted with when the pointer enters the trigger
        if (other.gameObject.tag == "Pointer")
        {
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Stop the item from being interected with when the pointer leaves the trigger
        if (other.gameObject.tag == "Pointer")
        {
            interactable = false;
        }
    }
}
