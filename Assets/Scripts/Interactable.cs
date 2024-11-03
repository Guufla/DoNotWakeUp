using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public enum InteractableType
    {
        minigame,
        interact,
        pickup
    }
    public InteractableType type;

    public SphereCollider col;
    [Tooltip("Layer player is on")]
    public LayerMask playerLayer;
    [Tooltip("Actual interactable model")]
    public Transform interactable;
    [Tooltip("Events to run when interacting")]
    public UnityEvent interactEvent;

    bool hasPlayer = false;
    Transform player;
    GameObject promptCanvas;
    bool canInteract = false; // Allows player to start minigame
    bool interacted = false;

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

        if (canInteract && Input.GetKey(KeyCode.E))
        {
            if (type == InteractableType.minigame)
            {
                StartMinigame();
            }
            else if (type == InteractableType.interact)
            {
                Interact();
            }
            else
            {
                Pickup();
            }

            interacted = true;
            promptCanvas.SetActive(false);
        }
    }

    public virtual void StartMinigame()
    {
        Debug.Log("Started minigame");
    }

    public virtual void Interact()
    {
        interactEvent.Invoke();
        Debug.Log("Interacted");
    }

    public virtual void Pickup()
    {
        Debug.Log("Interacted");
    }
}
