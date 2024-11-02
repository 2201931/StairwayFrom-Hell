using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsKillZone : MonoBehaviour
{
    public Transform respawnPoint;         // The point where the player will respawn
    public GameObject player;               // Reference to the player GameObject
    public Vector2 killZoneCenter;          // Center of the kill zone
    public Vector2 killZoneSize;            // Size of the kill zone

    private Bounds killZoneBounds;          // Bounds representing the kill zone

    void Start()
    {
        // Initialize the bounds based on the center and size
        killZoneBounds = new Bounds(killZoneCenter, killZoneSize);
    }

    void Update()
    {
        // Check if the player is within the bounds
        if (killZoneBounds.Contains((Vector2)player.transform.position))
        {
            Debug.Log("Player is within kill zone. Respawning...");
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        // Move the player to the respawn point
        player.transform.position = respawnPoint.position;

        // Reset the player's Rigidbody velocity if it has one
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;          // Reset linear velocity
            rb.angularVelocity = Vector2.zero;   // Reset angular velocity
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the bounds in the Scene view for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(killZoneCenter, killZoneSize); // Draw the kill zone as a wire cube
    }
}