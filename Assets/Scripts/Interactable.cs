using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public SphereCollider col;
    [Tooltip("Layer player is on")]
    public LayerMask playerLayer;
    [Tooltip("Actual interactable model")]
    public Transform interactable;
    [Tooltip("Events to run when interacting")]
    public UnityEvent interactEvent;
    [Tooltip("Event that happens when you exit interactable")]
    public UnityEvent leaveEvent;

    bool hasPlayer = false;
    Transform player;
    GameObject promptCanvas;
    bool canInteract = false; // Allows player to start minigame
    bool interacted = false;

    void Start()
    {
        player = GameManager.instance.player.GetComponentInChildren<Camera>().transform;
        promptCanvas = GameManager.instance.interactPrompt;
        StartEvents();
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
            if (Physics.Raycast(directRay, out obstacle, col.radius * 2f, ~playerLayer) && !interacted) // Make sure player is looking at interactable
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

        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
            promptCanvas.SetActive(false);
        }

        UpdateEvents();
    }

    public virtual void StartEvents()
    {
        // Stuff that's called when the game starts
    }

    public virtual void UpdateEvents()
    {
        // stuff that's called on Update
    }

    public void Interact()
    {
        if (!interacted)
        {
            interactEvent.Invoke();
            Debug.Log("Interacted");
            interacted = true;
        }
        else
        {
            leaveEvent.Invoke();
            Debug.Log("Uninteract");
            interacted = false;
        }
    }
}
