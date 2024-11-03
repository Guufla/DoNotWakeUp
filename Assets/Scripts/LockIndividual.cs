using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockIndividual : Interactable
{
    //bool to hold if lock is locked
    [SerializeField] public bool isLocked = false;

    //bool to hold if player is in range
    private bool playerInRange = false;

    //player reference
    [SerializeField] Transform player;


    //trigger to see if the player is in range of locking the door 

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            Debug.Log("Player is in range");
            //set player range  to true
            playerInRange = true;
        }
    }

    //When the player moves away from the range set to false
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            Debug.Log("Player is in out fo range");
            //set player range to flase
            playerInRange = false;
        }
    }
    */

    //lock the this door
    public void togglelockDoor()
    {

        isLocked = !isLocked;
        Debug.Log($"Toggled lock state: {(isLocked ? "Locked" : "Unlocked")} door.");
    }

    void Update()
    {
        //if player is in ragne and player presses E key then lock the door
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            togglelockDoor();
        }
    }
}