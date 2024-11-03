using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSystem : MonoBehaviour
{

    [SerializeField] bool isAllLocked = false;

    [SerializeField] LockIndividual[] locks;
    // Start is called before the first frame update
    void Start()
    {
        //get all the locks
        locks = GetComponentsInChildren<LockIndividual>();
    }


    public void CheckLockStates()
    {
        // checks the lock states inthe prompt
        foreach(var lockObj in locks)
        {
            //Debug.Log($"{lockObj.name} is locked: {lockObj.isLocked}");
        }
    }
    //returns bool value to see if all the locks area locked
    public bool AreAllLocksLocked()
    {
        foreach (var lockObj in locks)
        {
            if (!lockObj.isLocked)
                return false;
                //isAllLocked = false; // If any lock is unlocked, return false
        }
        return true;
        //isAllLocked = true; // All locks are locked
    }
    // Update is called once per frame
    void Update()
    {
        isAllLocked = AreAllLocksLocked();
    }
}
