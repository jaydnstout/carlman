using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public Transform player;
    public float sightRange = 10f;
    public LayerMask sightMask; // Set this to include obstacles and the player
    public float moveSpeed = 3f;

    private Vector3? lastSeenPosition = null;

    void Update()
    {
        // Check if player is within sight range
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Raycast to check for line of sight
        if (distanceToPlayer <= sightRange)
        {
            Ray ray = new Ray(transform.position, directionToPlayer.normalized);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, sightRange, sightMask))
            {
                if (hit.transform == player)
                {
                    // Player is visible
                    Debug.Log("Player spotted!");
                    lastSeenPosition = player.position;
                    // Add follow logic here
                }
            }
        }

        // Move towards last seen position if it exists
        if (lastSeenPosition.HasValue)
        {
            Vector3 target = lastSeenPosition.Value;
            Vector3 moveDirection = (target - transform.position).normalized;
            float step = moveSpeed * Time.deltaTime;

            // Move enemy towards the target position
            if (GetComponent<Rigidbody>() != null)
            {
                Vector3 newPosition = Vector3.MoveTowards(transform.position, target, step);
                GetComponent<Rigidbody>().MovePosition(newPosition);
            }

            // Rotate enemy to face the player (Y axis only)
            Vector3 lookDirection = player.position - transform.position;
            lookDirection.y = 0; // Ignore vertical difference
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = targetRotation;
            }

            // Optionally, clear lastSeenPosition when reached
            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                lastSeenPosition = null;
            }
        }
    }
}