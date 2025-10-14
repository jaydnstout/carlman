using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // References
    [Header("References")]
    PlayerStats playerStats;
    Rigidbody rb;
    public GameObject cameraObject;
    AudioSource audioSource;
    PlayerSFX playerSFX;

    // State Parameters
    [Header("State Parameters")]
    public bool isActive = true;
    public bool canMove = true;
    public bool canLook = true;
    bool isGrounded = true;

    // Movement Parameters
    [Header("Movement Parameters")]
    public float moveSpeed;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    float moveSoundTimer = 0.5f;
    public float lookSensitivity = 2f;
    public float jumpForce = 5f;

    void Start()
    {
        // Get references
        playerStats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerSFX = GetComponent<PlayerSFX>();

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        // Check if player is grounded
        if (collision.gameObject.tag == "Ground" && !isGrounded)
        {
            isGrounded = true;

            // Jumping
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                audioSource.PlayOneShot(playerSFX.jumpSound, 6);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if player is no longer grounded
        if (collision.gameObject.tag == "Ground" && isGrounded)
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        // Movement
        if (isActive && canMove)
        {
            // Determine move speed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (playerStats.stamina > 0 && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
                {
                    // If moving and has stamina, use run speed and drain stamina
                    moveSpeed = runSpeed;
                    playerStats.stamina -= 10 * Time.deltaTime;
                }
                else
                {
                    // If out of stamina, revert to walk speed
                    moveSpeed = walkSpeed;
                }
            }
            else
            {
                moveSpeed = walkSpeed;
            }

            // Get input and move player
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            Vector3 velocity = transform.TransformDirection(movement) * moveSpeed;
            velocity.y = rb.linearVelocity.y; // Preserve existing vertical velocity
            rb.linearVelocity = velocity;

            // Play walking/running sounds
            if (isGrounded)
            {
                if (moveHorizontal != 0 || moveVertical != 0)
                {
                    moveSoundTimer -= Time.deltaTime;

                    if (moveSoundTimer <= 0)
                    {
                        if (Input.GetKey(KeyCode.LeftShift) && playerStats.stamina > 0)
                        {
                            audioSource.PlayOneShot(playerSFX.runSounds[Random.Range(0, playerSFX.runSounds.Length)], 2);
                            moveSoundTimer = 0.33f;
                        }
                        else
                        {
                            audioSource.PlayOneShot(playerSFX.walkSounds[Random.Range(0, playerSFX.walkSounds.Length)], 2);
                            moveSoundTimer = 0.5f;
                        }
                    }
                }
                else
                {
                    moveSoundTimer = 0;
                }

                
            }

            // Looking around
            if (isActive && canLook && cameraObject != null)
            {
                // Get mouse input and rotate player and camera
                float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;
                transform.Rotate(0, mouseX, 0);
                cameraObject.transform.Rotate(-mouseY, 0, 0);

                // Clamp camera vertical rotation
                float cameraRotationX = cameraObject.transform.eulerAngles.x;
                float playerRotationY = transform.eulerAngles.y;
                cameraObject.transform.eulerAngles = new Vector3(cameraRotationX, playerRotationY, 0);
            }
        }
    }
}
