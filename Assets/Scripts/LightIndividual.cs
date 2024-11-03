using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIndividual : Interactable
{
    [SerializeField] Light lightSource;   // Reference to the Light component
    private bool playerInRange; // Flag to check if player is within the trigger area

    // Reference to the player GameObject
    [SerializeField] Transform player;

    [SerializeField] Collider lightCollision;
    // Method to toggle the light on/off
    [SerializeField] public bool isLight = false;

    public void ToggleLight()
    {   
        
        lightSource.enabled = !lightSource.enabled;
        lightCollision.enabled = !lightCollision.enabled;
        isLight = !isLight;
    }


/*
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
*/
}
