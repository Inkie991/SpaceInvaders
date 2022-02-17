using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessManager : MonoBehaviour, IGameManager
{
    [SerializeField] private BestScore bestScoreObject;
    public ManagerStatus Status { get; private set; }
    public int score;

    // Init score
    public void Startup()
    {
        Debug.Log("Game process manager starting...");
        score = 0;
        Status = ManagerStatus.Started;
    }

    // Increase the scoe and update UI
    public void ChangeScore()
    {
        score++;
        Managers.UI.UpdateScore();
    }
    
    // Call PlayerLose method
    public void GameOver()
    {
        Managers.UI.PlayerLose();
    }
}
