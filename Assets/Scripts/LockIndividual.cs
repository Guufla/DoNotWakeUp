using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockIndividual : Interactable
{
    //bool to hold if lock is locked
    [SerializeField] public bool isLocked = false;
    [Tooltip("Chance to play a visual out of 100")]
    public float chanceOfVisual = 50f;
    public GameObject visualObj;
    public AudioSource visualAudio;

    bool canPlayVisual = true;

    public override void StartEvents()
    {
        visualObj.SetActive(false);
        base.StartEvents();
    }

    // lock/unlock this door
    public void togglelockDoor()
    {
        isLocked = !isLocked;
    }

    public void VisualChance()
    {
        float rand = Random.Range(0f, 100f);
        if (rand <= chanceOfVisual && canPlayVisual)
        {
            Debug.Log("visual!");
            visualObj.SetActive(true);
            if (visualAudio)
            {
                visualAudio.Play();
            }
        }

        canPlayVisual = false;
    }
}