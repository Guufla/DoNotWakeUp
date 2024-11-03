using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toothbrush : MonoBehaviour
{
    [Tooltip("Time to display celebration")]
    public float winDelay = 2;
    [Tooltip("Name of the toothbrush image object")]
    public string toothbrushName = "Toothbrush";
    public Image toothbrush;
    public float requirement = 100f;
    public Slider progressBar;
    public GameObject celebrate;

    bool brushing = false;
    float progress = 0;
    bool completed = false;

    Vector3 mouseDelta = Vector3.zero;
    Vector3 lastMousePos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        celebrate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!completed)
        {
            toothbrush.rectTransform.position = Input.mousePosition;
            mouseDelta = Input.mousePosition - lastMousePos;
            lastMousePos = Input.mousePosition;

            if (brushing)
            {
                progress += (mouseDelta.magnitude * Time.deltaTime);
                progressBar.value = progress / requirement;
            }
        }

        if (progressBar.value == 1 && !completed)
        {
            completed = true;
            celebrate.SetActive(true);
            StartCoroutine(DelayRemove());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.name.Equals(toothbrushName))
        {
            brushing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        brushing = false;
    }

    IEnumerator DelayRemove()
    {
        yield return new WaitForSeconds(winDelay);
        transform.parent.parent.GetComponent<Minigame>().CompleteMinigame();
    }
}
