using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSystem : Interactable
{

    [SerializeField] bool isAllLocked = false;

    [SerializeField] LockIndividual[] locks;
    bool canCheck = true;
    
    public override void StartEvents()
    {
        //get all the locks
        locks = GetComponentsInChildren<LockIndividual>();
        SetLockChildren(false);
    }

    public void SetLockChildren(bool state)
    {
        foreach (LockIndividual lockObj in locks)
        {
            lockObj.SetActive(state);
        }
    }

    //returns bool value to see if all the locks area locked
    public bool AreAllLocksLocked()
    {
        foreach (LockIndividual lockObj in locks)
        {
            if (!lockObj.isLocked)
            {
                // If any lock is unlocked, return false
                return false;
            }
        }

        return true;
        // All locks are locked
    }

    // Update is called once per frame
    void Update()
    {
        if (canCheck)
        {
            isAllLocked = AreAllLocksLocked();
        }

        if (isAllLocked && canCheck)
        {
            canCheck = false;
            interactEvent.Invoke();
        }
    }
}
