using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSystemHigher : MonoBehaviour
{
    //get all the lights
    [SerializeField] LightIndividual[] lights;

    [SerializeField] bool isAllLights;


    void Start()
    {
        //get all the lights
        lights = GetComponentsInChildren<LightIndividual>();
    }

    
    public void CheckLightStates()
    {
        //consoel log all light states
        foreach(var lightObj in lights)
        {
            //Debug.Log($"{lightObj.name} is locked: {lightObj.isLight}");
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
