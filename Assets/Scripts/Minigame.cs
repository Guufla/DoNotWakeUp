using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : Interactable
{
    [Header("Minigame")]
    public GameObject minigameUI;

    Transform canvasParent;

    // Start is called before the first frame update
    public override void StartEvents()
    {
        canvasParent = GameManager.instance.minigameCanvas;
    }

    public void StartMinigame()
    {

    }

    public void LeaveMinigame()
    {

    }
}
