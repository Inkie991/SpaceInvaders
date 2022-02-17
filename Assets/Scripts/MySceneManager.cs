using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }

    public void Startup()
    {
        Debug.Log("My scene manager starting...");
        Status = ManagerStatus.Started;

        Invoke("LoadMenu", 3f);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(3);
        SceneManager.sceneLoaded += Managers.UI.FindUIElementsInGame;
        Invoke("StartGame", 0.2f);
    }

    private void StartGame()
    {
        //Managers.UI.FindUIElementsInGame();
        Managers.Enemy.SpawnFirstEnemys();
    }

    public void ExitFromApp()
    {
        Application.Quit();
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

}
