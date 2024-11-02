using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public SphereCollider col;
    [Tooltip("Layer player is on")]
    public LayerMask playerLayer;
    [Tooltip("Actual interactable model")]
    public Transform interactable;

    bool hasPlayer = false;
    Transform player;
    GameObject promptCanvas;
    bool canInteract = false; // Allows player to start minigame
    bool minigameStarted = false;

    private void Start()
    {
        player = GameManager.instance.player.GetComponentInChildren<Camera>().transform;
        promptCanvas = GameManager.instance.interactPrompt;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            hasPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            hasPlayer = false;
            promptCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (hasPlayer)
        {
            Vector3 direction = (interactable.position - player.position).normalized;
            Ray directRay = new Ray(player.position, direction);
            RaycastHit obstacle;
            if (Physics.Raycast(directRay, out obstacle, col.radius * 2f, ~playerLayer) && !minigameStarted) // Make sure player is looking at interactable
            {
                if (Vector3.Dot(player.transform.TransformDirection(transform.forward), direction) > 0.5f)
                {
                    promptCanvas.SetActive(true);
                    canInteract = true;

                }
                else
                {
                    promptCanvas.SetActive(false);
                    canInteract = false;
                }
            }
        }

        if (canInteract && Input.GetKey(KeyCode.E))
        {
            StartMinigame();
        }
    }

    public virtual void StartMinigame()
    {
        minigameStarted = true;
        promptCanvas.SetActive(false);
    }
}
