using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsKillZone : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject player;
    public Vector3 killZoneCenter;
    public Vector3 killZoneSize;

    private Bounds killZoneBounds;

    void Start()
    {
        // Initialize the bounds based on center and size
        killZoneBounds = new Bounds(killZoneCenter, killZoneSize);
    }

    void Update()
    {
        // Check if the player is within the bounds
        if (killZoneBounds.Contains(player.transform.position))
        {
            Debug.Log("Player is within kill zone. Respawning...");
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        player.transform.position = respawnPoint.position;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = Vector2.zero;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the bounds in the Scene view for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(killZoneCenter, killZoneSize);
    }
}
