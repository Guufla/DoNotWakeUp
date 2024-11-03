using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    #region Singleton

    public static SceneChanger instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    [Header("Transitions")]
    public bool startCalm = true;
    public Transform slowFade;
    public Transform flashFade;
    public bool isGame = false;

    Animator slowFadeAnimator;
    Animator flashFadeAnimator;

    bool isCalm = false;
    bool isFlash = false;

    int sceneIndex = 0;

    private void Start()
    {
        if (!isGame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (startCalm)
        {
            slowFade.gameObject.SetActive(true);
            flashFade.gameObject.SetActive(false);
        }
        else
        {
            slowFade.gameObject.SetActive(false);
            flashFade.gameObject.SetActive(true);
        }

        slowFadeAnimator = slowFade.GetComponent<Animator>();
        flashFadeAnimator = flashFade.GetComponent<Animator>();
    }

    private void Update()
    {
        AnimatorStateInfo startInfo = slowFadeAnimator.GetCurrentAnimatorStateInfo(0);
        if (startInfo.IsName("FadeOut") && startInfo.normalizedTime > 1)
        {
            slowFade.gameObject.SetActive(false);
        }
        AnimatorStateInfo flashStartInfo = flashFadeAnimator.GetCurrentAnimatorStateInfo(0);
        if (flashStartInfo.IsName("FlashOut") && flashStartInfo.normalizedTime > 1)
        {
            flashFade.gameObject.SetActive(false);
        }

        if (isCalm)
        {
            AnimatorStateInfo info = slowFadeAnimator.GetCurrentAnimatorStateInfo(0);
            if (info.IsName("FadeIn") && info.normalizedTime > 1)
            {
                isCalm = false;
                ChangeScene(sceneIndex);
            }
        }

        if (isFlash)
        {
            AnimatorStateInfo info = flashFadeAnimator.GetCurrentAnimatorStateInfo(0);
            if (info.IsName("FlashIn") && info.normalizedTime > 1)
            {
                isFlash = false;
                ChangeScene(sceneIndex);
            }
        }
    }

    public void CalmTransition(int index)
    {
        slowFade.gameObject.SetActive(true);
        slowFadeAnimator.Play("FadeIn");
        isCalm = true;
        sceneIndex = index;
    }
    public void FlashTransition(int index)
    {
        flashFade.gameObject.SetActive(true);
        flashFadeAnimator.Play("FlashIn");
        isFlash = true;
        sceneIndex = index;
    }

    void ChangeScene(int index)
    {
        StartCoroutine(LoadAsync(index));
    }


    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            // Progress text/slider value
            yield return null;
        }
    }
}
