using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    // References
    [Header("References")]
    public RectTransform healthBar;
    public RectTransform staminaBar;

    void Update()
    {
        // Update health and stamina bars based on player stats
        healthBar.sizeDelta = new Vector2(GetComponent<PlayerStats>().health * 5.4f, healthBar.sizeDelta.y);
        staminaBar.sizeDelta = new Vector2(GetComponent<PlayerStats>().stamina * 5.4f, healthBar.sizeDelta.y);
    }
}
