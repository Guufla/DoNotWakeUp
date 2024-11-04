using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSystemHigher : MonoBehaviour
{
    //get all the lights
    public LightIndividual[] lights;
    public Interactable winInteract;
    public bool canSleep = true;

    [SerializeField] bool isAllLights;


    void Start()
    {
        //get all the lights
        lights = GetComponentsInChildren<LightIndividual>();
    }

    
    public void CheckLightStates()
    {
        bool testSleep = true;
        //consoel log all light states
        foreach(LightIndividual lightObj in lights)
        {
            if (lightObj.isLight)
            {
                testSleep = false;
                break;
            }
        }

        canSleep = testSleep;
        if (GameManager.instance.currentTasksCompleted == GameManager.instance.taskList.Length - 1)
        {
            winInteract.SetActive(canSleep);
        }
    }


    public void AreAllLightsOff()
    {
        foreach (var lightObj in lights)
        {
            if(!lightObj.isLight)
            {
                //all lights are not off
                isAllLights = false;
            }
        }
        //all lights are off
        isAllLights = true;
    }

    void Update()
    {
        CheckLightStates();
    }
}
