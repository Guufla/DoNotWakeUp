using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    
    [SerializeField] bool isInLight = false;
    [SerializeField] Transform player;

    [SerializeField] private LightIndividual islight;
    private void OnTriggerEnter(Collider other)
    {
        isInLight = true;
        if (other.gameObject == player.gameObject && islight.isLight && isInLight )
        {
            RenderSettings.fog = !RenderSettings.fog;
            Debug.Log("Player is in the light");
            Debug.Log(islight.isLight);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInLight = false;
        if (other.gameObject == player.gameObject && !islight.isLight && isInLight)
        {
            RenderSettings.fog = !RenderSettings.fog;
            Debug.Log("Player is out of the Light");
            Debug.Log(islight.isLight);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
