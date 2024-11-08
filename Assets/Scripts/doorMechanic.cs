using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorMechanic : Interactable
{
    public Animator doorAnimator;
    private bool isAnimating = false;

    public void ToggleDoor()
    {
        if (!isAnimating)
        {
            bool isOpen = doorAnimator.GetBool("isdoorOpen");
            doorAnimator.SetBool("isdoorOpen", !isOpen);
            if (isOpen)
            {
                doorAnimator.Play("openDoor");
            }
            else
            {
                doorAnimator.Play("openDoor");
            }

            //isAnimating = true;
            //StartCoroutine(ResetAnimationFlag(doorAnimator.GetCurrentAnimatorStateInfo(0).length));
        }
    }
    //fixed
    private IEnumerator ResetAnimationFlag(float duration)
    {
        yield return new WaitForSeconds(duration);
        isAnimating = false;
    }
}
