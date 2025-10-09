using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [Header("Walking")]
    public AudioClip[] walkSounds;
    [Header("Running")]
    public AudioClip[] runSounds;
    [Header("Jumping")]
    public AudioClip jumpSound;
    public AudioClip landSound;
    AudioSource audioSource;
}
