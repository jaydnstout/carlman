using Unity.VisualScripting;
using UnityEngine;

public class DoorSFX : MonoBehaviour
{
    // References
    public AudioSource audioSource;

    // Door SFX
    [Header("Door SFX")]
    public AudioClip doorOpen;
    public AudioClip doorClose;
    public AudioClip doorLocked;

    void Start()
    {
        // Get references
        audioSource = GetComponent<AudioSource>();
    }
}
