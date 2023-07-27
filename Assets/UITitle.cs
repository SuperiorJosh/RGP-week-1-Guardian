using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITitle : MonoBehaviour
{
    public Image FadeImage;
    public CanvasGroup ButtonsCanvasGroup;
    
    private void Awake()
    {
    }

    private void Start()
    {
        FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, 1f);
        ButtonsCanvasGroup.alpha = 0;
        ButtonsCanvasGroup.interactable = false;
        Invoke(nameof(ShowTitle),2f);
    }

    void ShowTitle()
    {
        FadeImage.DOFade(0f, 1f).OnComplete(() =>
        {
            ButtonsCanvasGroup.DOFade(1f, 1f);
            ButtonsCanvasGroup.interactable = true;
        });
    }
    
    public void StartGame(string sceneName)
    {
        FadeImage.DOFade(1f, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
    
    public void Quit()
    {
        FadeImage.DOFade(1f, 1f).OnComplete(Application.Quit);
    }
}
