using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }

    // Load menu after 3 seconds delay
    public void Startup()
    {
        Debug.Log("My scene manager starting...");
        Status = ManagerStatus.Started;

        Invoke("LoadMenu", 3f);
    }

    // Load menu scene
    public void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }

    // Load options scene
    public void LoadOptions()
    {
        SceneManager.LoadScene(2);
    }

    // Load game scene
    public void LoadGame()
    {
        SceneManager.sceneLoaded += Managers.UI.FindUIElementsInGame;
        SceneManager.LoadScene(3);
        Invoke("StartGame", 0.2f);
    }

    // Spawn enemies on start
    private void StartGame()
    {
        Managers.Enemy.SpawnFirstEnemies();
    }

    // Close the game
    public void ExitFromApp()
    {
        Application.Quit();
    }

    // Pause the game
    public void StopGame()
    {
        Time.timeScale = 0;
    }

    // Resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
