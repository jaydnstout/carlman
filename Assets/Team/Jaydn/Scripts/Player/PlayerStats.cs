using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // References
    PlayerController pc;
    Rigidbody rb;

    // Player Stats
    [Header("Player Stats")]
    public float health = 100;
    public float maxHealth = 100;
    bool dead = false;
    public float stamina = 100;
    public float maxStamina = 100;

    private void Start()
    {
        // Get references to PlayerController and Rigidbody components
        pc = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Clamp health and stamina to their maximum values
        health = Mathf.Clamp(health, 0, maxHealth);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        // Regenerate stamina over time
        if (stamina < maxStamina && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            stamina += 5 * Time.deltaTime;
        }

        // Determine fall direction for death animation
        float fallDirection;
        if (Input.GetAxis("Vertical") != 0)
        {
            fallDirection = Input.GetAxis("Vertical");
        }
        else
        {
            fallDirection = 1;
        }

        // Check for player death
        if (health <= 0 && !dead)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddRelativeForce(Vector3.forward * (pc.moveSpeed / 3) * fallDirection, ForceMode.Impulse);
            pc.isActive = false;
            dead = true;
            Debug.Log("Player has died");
        }
    }
}
