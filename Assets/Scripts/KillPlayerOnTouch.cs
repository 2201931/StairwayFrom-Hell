using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour
{
    // Assign the RespawnPoint GameObject in the Inspector
    public Transform respawnPoint;

    void Start()
    {
        // Initial debug to confirm the script is running
        Debug.Log("KillPlayerOnTouch script attached and running.");
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug to confirm OnTriggerEnter is being called
        Debug.Log("OnTriggerEnter called on " + gameObject.name);

        // Check if the object colliding is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with hazard. Respawning...");
            RespawnPlayer(other.gameObject);
        }
        else
        {
            Debug.Log("Non-player object collided: " + other.name);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        // Debug to confirm RespawnPlayer function is being called
        Debug.Log("RespawnPlayer function called for " + player.name);

        // Set player position to the respawn point
        player.transform.position = respawnPoint.position;

        // If the player has a Rigidbody, reset its velocity
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Debug.Log("Player Rigidbody velocity reset.");
        }
    }
}