using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    [Header("References")]
    public Player player;
    public GameObject interactPrompt;
    public GameObject exitPrompt;
    public Transform minigameCanvas;

    [Header("Chills Bar")]
    public Slider chillsSlider;
    [SerializeField] float chillsRate = 1f;

    [SerializeField] float positiveModifier = 1f;

    [SerializeField] float completedTaskSubtraction = 30f;

    [SerializeField] bool timerDemonModifier = false;

    [SerializeField] bool completedOneTask = false;

    [SerializeField] bool dead = false;

    [SerializeField] bool deathForcasted = false;

    [SerializeField] float deathTimer;

    // Start is called before the first frame update
    void Start()
    {
        interactPrompt.SetActive(false);
        exitPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Whenever you complete a task it subtracts a small amount from the chills meter
        if (completedOneTask && deathForcasted == false)
        {
            chillsSlider.value -= completedTaskSubtraction;
            completedOneTask = false;
        }


        // Once the meter hits its max value the death will be forecasted which means that a random number will be generated and the player will die once the timer ends
        // Once the death is forecasted they cannot lower 
        if (chillsSlider.value >= chillsSlider.maxValue && deathForcasted == false)
        {
            deathForcasted = true;
            deathTimer = Random.Range(1f, 30f);
            Debug.Log(deathTimer);
        }

        if (deathForcasted && !dead)
        {
            dead = true;
            StartCoroutine(Death());
        }
    }
    void FixedUpdate()
    {
        // If we enable this modifier the rate at which the meter increases is faster
        if (timerDemonModifier)
        {
            chillsSlider.value += chillsRate + positiveModifier;
        }

        // Otherwise it remains a constant rate
        else
        {
            chillsSlider.value += chillsRate;
        }
    }

    IEnumerator Death()
    {
        // Wait [Whatever number is generated from the random function] before killing the player
        yield return new WaitForSeconds(deathTimer);


        // Replace this with whatever you want to happen once the player dies
        Debug.Log("YOU DIED");
    }
}
