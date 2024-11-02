using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public SphereCollider col;
    [Tooltip("Layer to ignore when raycasting")]
    public LayerMask interactLayer;
    [Tooltip("Actual interactable model")]
    public Transform interactable;
    [Tooltip("Prompt to interact")]
    public GameObject promptCanvas;

    bool hasPlayer = false;
    Transform player;

    private void Start()
    {
        player = GameManager.instance.player.transform;
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
        }
    }

    private void Update()
    {
        if (hasPlayer)
        {
            Vector3 direction = (interactable.position - player.position).normalized;
            Ray directRay = new Ray(player.position, direction);
            RaycastHit obstacle;
            if (Physics.Raycast(directRay, out obstacle, col.radius * 2, ~interactLayer))
            {
                Player hit = obstacle.collider.transform.GetComponent<Player>();
                if (player && Vector3.Dot(player.transform.TransformDirection(transform.forward), direction) > 0.5f)
                {
                    promptCanvas.SetActive(true);

                }
                else
                {
                    promptCanvas.SetActive(false);
                }
            }
        }
        else
        {
            promptCanvas.SetActive(false);
        }
    }
}
