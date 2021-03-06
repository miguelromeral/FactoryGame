﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void InitGame()
    {
        SceneManager.LoadScene("Game");
        Player.InitPlayer();
        /*
        var p = Pause.instance;
        Debug.Log("Starting game");
        if (p != null)
        {
            p.IsPaused = false;

            Debug.Log("Auto pause false");
        }*/
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Player.Health -= 1;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Main");
        Pause.instance = null;
    }

    public void GoHelp()
    {
        SceneManager.LoadScene("Help");
    }

    public void GoHelp2()
    {
        SceneManager.LoadScene("Help2");
    }

    public void GoHistory()
    {
        SceneManager.LoadScene("History");
    }
    public void GoCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void GoHelpMovement()
    {
        SceneManager.LoadScene("HelpMovement");
    }
    public void ExitApplication()
    {
        Application.Quit();
    }


}
