using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    [Header("Chills Bar")]
    public Slider chillsSlider;
    [SerializeField] float chillsRate = 1f;

    [SerializeField] float positiveModifier = 1f;

    [SerializeField] float completedTaskSubtraction = 30f;

    [SerializeField] bool timerDemonModifier = false;

    public bool completedOneTask = false;

    [SerializeField] bool dead = false;

    [SerializeField] bool deathForcasted = false;

    [SerializeField] float deathTimer;

    [SerializeField] GameObject jumpScare;

    GameObject jumpScareSound;


    [Header("Task Bar")]
    public Slider taskSlider;

    [SerializeField] float maxTasks = 0f;

    public float currentTasksCompleted;
    
    [SerializeField] bool canSleep; // WIN CONDITION

    public GameObject[] taskList;

    [SerializeField] TextMeshProUGUI taskText; 



    // Start is called before the first frame update
    void Start()
    {
        interactPrompt.SetActive(false);
        exitPrompt.SetActive(false);

        taskSlider.maxValue = maxTasks + 1;
        
        shuffleTasks(taskList);


        jumpScareSound = player.transform.GetChild(2).gameObject;
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

            Debug.Log("DEATH TIMER: " + deathTimer );

            Debug.Log(deathTimer);
        }

        if (deathForcasted && !dead)
        {
            dead = true;
            StartCoroutine(Death());
        }

        if (taskList.Length > 0)
        {
            // Task list code
            taskText.text = taskList[(int)currentTasksCompleted].GetComponent<Interactable>().taskName;

            taskSlider.value = currentTasksCompleted;

            taskList[(int)currentTasksCompleted].GetComponent<Interactable>().SetActive(true);
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

    void shuffleTasks(GameObject[] taskList){
        if (taskList.Length != 0)
        {
            for (int i = 0; i < maxTasks; i++)
            {

                int r = (int)Random.Range(i, maxTasks);

                (taskList[r], taskList[i]) = (taskList[i], taskList[r]);
            }
        }
    }

    IEnumerator Death()
    {
        // Wait [Whatever number is generated from the random function] before killing the player
        yield return new WaitForSeconds(deathTimer);


        // Replace this with whatever you want to happen once the player dies
        jumpScare.SetActive(true);
        jumpScareSound.SetActive(true);
    }
}
