using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] Light lightSource;   // Reference to the Light component
    private bool playerInRange; // Flag to check if player is within the trigger area

    // Reference to the player GameObject
    [SerializeField] Transform player;

    [SerializeField] Collider lightCollision;
    // Method to toggle the light on/off
    public void ToggleLight()
    {
        if (playerInRange) // Only toggle if the player is in range
        {
            lightSource.enabled = !lightSource.enabled;
            lightCollision.enabled = !lightCollision.enabled;
        }
    }

    // Detect when the player enters the trigger area
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the player
        if (other.gameObject == player.gameObject)
        {
            playerInRange = true;
            
            Debug.Log("Player in range of light switch.");
        }
    }

    // Detect when the player exits the trigger area
    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is the player
        if (other.gameObject == player.gameObject)
        {
            playerInRange = false;
            Debug.Log("Player out of range of light switch.");
        }
    }

    // Update method to detect input for toggling the light
    private void Update()
    {
        // Check if player is in range and presses the interaction key (e.g., "E")
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleLight();
        }
    }
}
