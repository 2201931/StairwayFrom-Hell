using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;  // Reference to a Dialog object containing dialogue text
    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed, starting dialog...");
            // Trigger dialog only if player is nearby and presses 'E'
            DialogManager.Instance.StartDialog(dialog);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered NPC range.");
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left NPC range.");
            isPlayerNearby = false;
            DialogManager.Instance.EndDialog();
        }
    }
}
