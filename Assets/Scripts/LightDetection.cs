using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightDetection : MonoBehaviour
{

    [SerializeField] bool isInLight = false;
    [SerializeField] Transform player;

    [SerializeField] private LightIndividual islight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        isInLight = true;
        if (other.gameObject == player.gameObject && islight.isLight && isInLight)
        {
            RenderSettings.fog = false;
            Debug.Log("Player is in the light");
            Debug.Log(islight.isLight);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInLight = false;
        if (other.gameObject == player.gameObject && !isInLight)
        {
            RenderSettings.fog = true;
            Debug.Log("Player is out of the Light");
            Debug.Log(islight.isLight);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player.gameObject && isInLight && islight.isLight)
        {
            RenderSettings.fog = false;
        }
        else if (!islight.isLight && other.gameObject == player.gameObject)
        {
            RenderSettings.fog = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
