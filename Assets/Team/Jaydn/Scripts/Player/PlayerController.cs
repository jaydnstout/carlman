using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // References
    [Header("References")]
    PlayerStats playerStats;
    Rigidbody rigidbody;
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
        rigidbody = GetComponent<Rigidbody>();
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
            audioSource.PlayOneShot(playerSFX.landSound);
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
            velocity.y = rigidbody.linearVelocity.y; // Preserve existing vertical velocity
            rigidbody.linearVelocity = velocity;

            // Play walking/running sounds
            if (isGrounded)
            {
                if (moveHorizontal != 0 || moveVertical != 0)
                {
                    int number;
                    AudioClip clip;

                    moveSoundTimer -= Time.deltaTime;

                    if (moveSoundTimer <= 0)
                    {
                        number = Random.Range(0, 9);
                        if (Input.GetKey(KeyCode.LeftShift) && playerStats.stamina > 0)
                        {
                            clip = playerSFX.runSounds[number];
                            moveSoundTimer = 0.33f;
                        }
                        else
                        {
                            clip = playerSFX.walkSounds[number];
                            moveSoundTimer = 0.66f;
                        }

                        audioSource.PlayOneShot(clip);
                        
                    }
                }
                else
                {
                    moveSoundTimer = 0;
                }

                // Jumping
                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    audioSource.PlayOneShot(playerSFX.jumpSound);
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
