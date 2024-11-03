using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour
{
    [SerializeField] private Image[] images;  // Assign the images in the Inspector in the desired order

    private float progress = 0;
    private bool completed = false;
    [SerializeField] float subtractprogress =  0.001f;
    public GameObject celebrate;
    [SerializeField] private Slider progressBar;  // Assign this in the Inspector
    public float winDelay = 2f;  // Delay time in seconds before ending the mini-game

    void Start()
    {
        // Initialize the UI
        celebrate.SetActive(false);
        progressBar.value = 0;

        // Make sure only the first image is visible at the start
        UpdateImageDisplay();
    }

    void Update()
    {
        if (!completed)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                progress += 0.05f;  // Adjust increment as needed
                progressBar.value = progress;

                // Update the image based on the current progress
                UpdateImageDisplay();
            }else{
                if(progress >= 0.06){
                progress -= subtractprogress * Time.deltaTime;  // Adjust increment as needed
                progressBar.value = progress;
                }
            }

        }

        if (progressBar.value >= 1 && !completed)
        {
            completed = true;
            celebrate.SetActive(true);
            StartCoroutine(DelayRemove());
        }
    }

    // Method to update which image is displayed based on progress
    private void UpdateImageDisplay()
    {
        // Determine which image to show based on progress range
        int imageIndex = Mathf.FloorToInt(progress * images.Length);

        // Ensure only the current image in the range is active, others are hidden
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(i == imageIndex);
        }
    }

    private IEnumerator DelayRemove()
    {
        yield return new WaitForSeconds(winDelay);
        transform.parent.parent.GetComponent<Minigame>().CompleteMinigame();
    }
}
