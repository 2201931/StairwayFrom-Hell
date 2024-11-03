using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVisibilityZone : MonoBehaviour
{
    public GameObject npc; // Assign the NPC GameObject you want to make visible
    public GameObject player; // Assign the Player GameObject
    public Vector3 visibilityZoneCenter; // Center of the visibility zone
    public Vector3 visibilityZoneSize; // Size of the visibility zone

    private Bounds visibilityZoneBounds;

    void Start()
    {
        // Initialize the visibility zone bounds based on the center and size
        visibilityZoneBounds = new Bounds(visibilityZoneCenter, visibilityZoneSize);

        // Ensure the NPC is hidden initially
        if (npc != null)
        {
            npc.SetActive(false);
        }
    }

    void Update()
    {
        // Update the visibility zone bounds to reflect changes made in the Inspector
        visibilityZoneBounds.center = visibilityZoneCenter + transform.position;
        visibilityZoneBounds.size = visibilityZoneSize;

        // Check if the player's position is within the visibility zone bounds
        if (npc != null && visibilityZoneBounds.Contains(player.transform.position))
        {
            Debug.Log("Player is within the visibility zone. Making NPC visible.");
            npc.SetActive(true); // Make the NPC visible
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the visibility zone in the Scene view for visualization
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(visibilityZoneCenter + transform.position, visibilityZoneSize);
    }
}

