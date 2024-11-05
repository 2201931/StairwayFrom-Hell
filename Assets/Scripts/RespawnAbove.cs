using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAbove : MonoBehaviour
{
    public float respawnHeightOffset = 1.5f; // Height above the player to respawn
    private Vector3 initialPosition;
    private PlayerJump playerJump; // Reference to the PlayerJump component

    void Start()
    {
        // Get the PlayerJump component attached to the player
        playerJump = GetComponent<PlayerJump>();
        initialPosition = transform.position;
    }

    void Update()
    {
        // Example condition: if the player is unable to move or stuck, you can define your own condition
        if (Input.GetKeyDown(KeyCode.R)) // Press "R" to respawn slightly above
        {
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        // Move player slightly above current position
        Vector3 respawnPosition = transform.position + Vector3.up * respawnHeightOffset;

        // Optional: Check for ground and avoid colliding immediately
        if (Physics.CheckSphere(respawnPosition, 0.5f)) // Adjust the radius if necessary
        {
            // If there is something in the way, adjust height until clear (could add more logic for safety)
            respawnPosition.y += respawnHeightOffset;
        }

        // Set the new position
        transform.position = respawnPosition;

        // Reset PlayerJump state
        if (playerJump != null)
        {
            playerJump.isCharging = false; // Ensure not charging
            playerJump.isGrounded = true; // Set grounded to true after respawn

            // Reset jump count if necessary (optional)
            playerJump.jumpCount = 0; // Reset jump count after respawn
            playerJump.chargeMeter.fillAmount = 0f; // Reset charge meter UI
            playerJump.directionIndicator.positionCount = 0; // Hide direction indicator
        }

        // Reset Rigidbody if it exists
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Reset velocity
            rb.angularVelocity = 0; // Reset angular velocity
            rb.isKinematic = false; // Ensure it's not set to kinematic
        }
    }
}
