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
    public int deathScreen = 2;
    public LightSystemHigher lightManager;

    [Header("Chills Bar")]
    public float scareVariance = 30f;
    public Slider chillsSlider;
    [SerializeField] float chillsRate = 1f;

    public float lightModRate = 0.01f;

    [SerializeField] float positiveModifier = 1f;

    [SerializeField] float completedTaskSubtraction = 30f;

    [SerializeField] bool timerDemonModifier = false;

    [SerializeField] bool dead = false;

    [SerializeField] bool deathForcasted = false;

    [SerializeField] float deathTimer;

    [SerializeField] GameObject jumpScare;

    GameObject jumpScareSound;


    [Header("Task Bar")]
    public Slider taskSlider;

    float maxTasks = 0f;

    public float currentTasksCompleted;
    
    [SerializeField] bool canSleep; // WIN CONDITION

    public GameObject[] taskList;

    [SerializeField] TextMeshProUGUI taskText; 



    // Start is called before the first frame update
    void Start()
    {
        interactPrompt.SetActive(false);
        exitPrompt.SetActive(false);

        maxTasks = taskList.Length - 1;
        taskSlider.maxValue = maxTasks + 1;
        
        shuffleTasks(taskList);


        jumpScareSound = player.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Once the meter hits its max value the death will be forecasted which means that a random number will be generated and the player will die once the timer ends
        // Once the death is forecasted they cannot lower 
        if (chillsSlider.value >= chillsSlider.maxValue && deathForcasted == false)
        {
            deathForcasted = true;
            deathTimer = Random.Range(1f, scareVariance);

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

        // Switch scene when jumpscare ends
        if (jumpScare)
        {
            Animator scareAnimator = jumpScare.GetComponent<Animator>();
            AnimatorStateInfo info = scareAnimator.GetCurrentAnimatorStateInfo(0);
            if (info.IsName("JumpScare") && info.normalizedTime > 1)
            {
                SceneChanger.instance.FlashTransition(deathScreen);
            }
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

            if (!lightManager.canSleep) // Increase rate if lights are on
            {
                chillsSlider.value += lightModRate;
            }
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

    public void CompletedTask()
    {
        // Whenever you complete a task it subtracts a small amount from the chills meter
        if (deathForcasted == false)
        {
            chillsSlider.value -= completedTaskSubtraction;
            currentTasksCompleted += 1;
        }
    }
}
