using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject cameraObject;

    // Player state variables
    public bool isActive = true;
    public bool canMove = true;
    public bool canLook = true;

    // Movement parameters
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float lookSensitivity = 2f;
    public float jumpForce = 5f;

    // Player stats
    public int health = 100;
    public int stamina = 100;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movement
        if (isActive && canMove)
        {
            float moveSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = runSpeed;
            }
            else
            {
                moveSpeed = walkSpeed;
            }

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

            // Jumping
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        // Mouse look
        if (isActive && canLook && cameraObject != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

            transform.Rotate(0, mouseX, 0);
            cameraObject.transform.Rotate(-mouseY, 0, 0);

            float cameraRotationX = cameraObject.transform.eulerAngles.x;
            float playerRotationY = transform.eulerAngles.y;
            cameraObject.transform.eulerAngles = new Vector3(cameraRotationX, playerRotationY, 0);
        }
    }
}
