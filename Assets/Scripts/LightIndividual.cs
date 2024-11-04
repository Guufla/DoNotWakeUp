using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIndividual : Interactable
{
    [SerializeField] Light lightSource;   // Reference to the Light component
    private bool playerInRange; // Flag to check if player is within the trigger area

    // Reference to the player GameObject

    [SerializeField] Collider lightCollision;
    // Method to toggle the light on/off
    public bool isLight = false;

    public void ToggleLight()
    {   
        lightSource.enabled = !lightSource.enabled;
        isLight = !isLight;
    }
}
