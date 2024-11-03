using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : Interactable
{
    public bool completed = false;
    [Header("Minigame")]
    public GameObject minigameUI;
    public AudioSource minigameWin;

    GameObject instantiatedMinigame;

    

    public void StartMinigame()
    {
        Cursor.lockState = CursorLockMode.None;
        instantiatedMinigame = Instantiate(minigameUI, transform);
        GameManager.instance.player.canMove = false;
    }

    public void LeaveMinigame()
    {
        Destroy(instantiatedMinigame);
        GameManager.instance.player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CompleteMinigame()
    {
        GameManager.instance.currentTasksCompleted += 1;
        completed = true;
        if (minigameWin)
        {
            minigameWin.Play();
        }
        SetActive(false);
        Interact();
    }
}
