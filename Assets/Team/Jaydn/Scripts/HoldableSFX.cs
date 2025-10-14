using UnityEngine;

public class HoldableSFX : MonoBehaviour
{
    [Header("Holdable SFX")]
    public AudioClip holdSound;
    public AudioClip dropSound;
    public AudioClip useSound;

    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
