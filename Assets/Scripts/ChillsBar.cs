using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChillsBar : MonoBehaviour
{

    // NOTE: Not all of these will stay Serialize Fields its like this for the purpose of easy debugging

    [SerializeField] float chillsRate = 1f;


    [SerializeField] float positiveModifier = 1f;

    [SerializeField] float completedTaskSubtraction = 30f;

    [SerializeField] bool timerDemonModifier = false;

    [SerializeField] bool completedOneTask = false;


    [SerializeField] bool dead = false;

    [SerializeField] bool deathForcasted = false;

    [SerializeField] float deathTimer;

    Slider chillsSlider; // Reference to the chills slider


    // Start is called before the first frame update
    void Start()
    {
        chillsSlider = GetComponent<Slider>();
        deathTimer = 30f;
    }


    void FixedUpdate()
    {
        

        // If we enable this modifier the rate at which the meter increases is faster
        if(timerDemonModifier){
            chillsSlider.value += chillsRate + positiveModifier;
        }

        // Otherwise it remains a constant rate
        else{
            chillsSlider.value += chillsRate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Whenever you complete a task it subtracts a small amount from the chills meter
        if(completedOneTask && deathForcasted == false){
            chillsSlider.value -= completedTaskSubtraction;
            completedOneTask = false;
        }


        // Once the meter hits its max value the death will be forecasted which means that a random number will be generated and the player will die once the timer ends
        // Once the death is forecasted they cannot lower 
        if(chillsSlider.value >= chillsSlider.maxValue && deathForcasted == false){
            deathTimer = Random.Range(1f,30f);
            deathForcasted = true;
            Debug.Log(deathTimer);
        }

        if(deathForcasted && !dead)
        {
            dead = true;
            StartCoroutine(death());
        }
    }

    IEnumerator death()
    {
        // Wait [Whatever number is generated from the random function] before killing the player
        yield return new WaitForSeconds(deathTimer);


        // Replace this with whatever you want to happen once the player dies
        Debug.Log("YOU DIED");
    }
}
