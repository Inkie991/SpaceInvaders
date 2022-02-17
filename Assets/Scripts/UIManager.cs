using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour, IGameManager
{
    [SerializeField] private List<Sprite> healthSprites;
    public ManagerStatus Status { get; private set; }
    private Image healthBar;
    private Text scoreBar;
    private GameObject endGamePopup;

    // Set status 'started'
    public void Startup()
    {
        Debug.Log("UI manager starting...");
        Status = ManagerStatus.Started;
    }

    // Show popup and stop the game, update best score
    public void PlayerLose()
    {
        endGamePopup.SetActive(true);
        GameObject.FindGameObjectWithTag("Total Score").GetComponent<Text>().text = Managers.GameProcess.score.ToString();
        UpdateBestScore();
        Managers.Scene.StopGame();
    }

    // Update the score on UI
    public void UpdateScore()
    {
        scoreBar.text = Managers.GameProcess.score.ToString();
    }

    // Update best score if needed
    private void UpdateBestScore()
    {
        BestScore _bestScore = Resources.Load<BestScore>("ScriptableObjects/BestScoreData");
        
        if (Managers.GameProcess.score > _bestScore.bestScore)
        {
            _bestScore.bestScore = Managers.GameProcess.score;
        }
    }

    // Update health bar
    public void ChangeHealth(int health)
    {
        switch (health)
        {
            case 0:
            {
                healthBar.sprite = healthSprites[0];
                break;
            }
            case 1:
            {
                healthBar.sprite = healthSprites[1];
                break;
            }
            case 2:
            {
                healthBar.sprite = healthSprites[2];
                break;
            }
            case 3:
            {
                healthBar.sprite = healthSprites[3];
                break;
            }
            case 4:
            {
                healthBar.sprite = healthSprites[4];
                break;
            }
            case 5:
            {
                healthBar.sprite = healthSprites[5];
                break;
            }
        }
    }

    // Find UI elements by tags
    public void FindUIElementsInGame(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 3)
        {
            endGamePopup = GameObject.FindGameObjectWithTag("End Game Popup");
            endGamePopup.SetActive(false);
            healthBar = GameObject.FindGameObjectWithTag("Health Bar").GetComponent<Image>();
            scoreBar = GameObject.FindGameObjectWithTag("Score Bar").GetComponent<Text>();
        }
    }
}
