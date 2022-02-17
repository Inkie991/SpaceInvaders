using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }
    [SerializeField] private BestScore bestScoreObject;
    public int score;

    public void Startup()
    {
        Debug.Log("Game process manager starting...");
        score = 0;
        Status = ManagerStatus.Started;
    }

    public void ChangeScore()
    {
        score++;
        Managers.UI.UpdateScore();
    }

    public void GameOver()
    {
        Managers.UI.PlayerLose();
    }

    public void CheckBestScore()
    {
        if (bestScoreObject.bestScore == score)
        {
            bestScoreObject.bestScore = score;
        }
    }
    
    
}
