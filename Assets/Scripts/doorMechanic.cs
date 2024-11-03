using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorMechanic : MonoBehaviour
{
    public Animator doorAnimator;
    public float toggleInterval = 3f; // Interval in seconds

    void Start()
    {
        // Start the coroutine to toggle the door every 3 seconds
        StartCoroutine(ToggleDoorEveryInterval());
    }

    private IEnumerator ToggleDoorEveryInterval()
    {
        while (true)
        {
            ToggleDoor(); // Toggle the door state
            yield return new WaitForSeconds(toggleInterval); // Wait for 3 seconds
        }
    }

    private void ToggleDoor()
    {
        bool isOpen = doorAnimator.GetBool("isdoorOpen");
        doorAnimator.SetBool("isdoorOpen", !isOpen);
    }
}
