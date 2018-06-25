﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {

    public Text scoreText;
    public GameObject PauseMenu;
    public GameObject IGMenu;

    public delegate void Loop();
    Loop updateLoop;

    // Use this for initialization
    void Start () {
        GameManager.instance.OnPauseGame += OnPause;
        GameManager.instance.OnResumeGame += OnResume;
        GameManager.instance.OnStartGame += OnStart;
    }

    private void OnStart()
    {
        ShowIGMenu();
    }

    private void OnPause()
    {
        HideIGMenu();
        ShowPauseMenu();
    }

    private void OnResume()
    {
        HidePauseMenu();
        ShowIGMenu();
    }

    void ShowIGMenu()
    {
        IGMenu.SetActive(true);
        updateLoop = LoopIGMenu;
    }

    void HideIGMenu()
    {
        IGMenu.SetActive(false);
    }

    void ShowPauseMenu()
    {
        PauseMenu.SetActive(true);
        updateLoop = LoopPauseMenu;
    }

    public void HidePauseMenu()
    {
        PauseMenu.SetActive(false);
    }

    void LoopPauseMenu()
    {
    }

    void LoopIGMenu()
    {
        scoreText.text = ScoreManager.instance.score.ToString();
    }

    private void Update()
    {
        if(updateLoop != null)
            updateLoop();
    }
}
