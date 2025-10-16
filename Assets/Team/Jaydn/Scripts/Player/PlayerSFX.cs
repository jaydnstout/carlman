using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    // References
    AudioSource audioSource;

    [Header("Ambience SFX")]
    public AudioClip ambienceLoop;
    public AudioClip[] randomAmbience;
    float randomAmbienceTimer;
    public float ambienceTimerMinimum;
    public float ambienceTimerMaximum;

    [Header("Walking SFX")]
    public AudioClip[] walkSounds;

    [Header("Running SFX")]
    public AudioClip[] runSounds;

    [Header("Jumping SFX")]
    public AudioClip jumpSound;
    public AudioClip landSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        randomAmbienceTimer = Random.Range(ambienceTimerMinimum, ambienceTimerMaximum);
    }

    void Update()
    {
        randomAmbienceTimer -= Time.deltaTime;

        if (randomAmbienceTimer <= 0)
        {
            audioSource.PlayOneShot(randomAmbience[Random.Range(0, randomAmbience.Length)], Random.Range(0.25f, 0.5f));
            randomAmbienceTimer = Random.Range(ambienceTimerMinimum, ambienceTimerMaximum);
        }
    }
}