using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    
    [SerializeField] bool isInLight = false;
    [SerializeField] Transform player;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == player.gameObject)
        {
            isInLight = true;
            Debug.Log("Player is in the light");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject == player.gameObject)
        {
            isInLight = false;
            Debug.Log("Player is out of the Light");
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
