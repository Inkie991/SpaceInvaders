using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighestScoreSwitcher : MonoBehaviour
{
    private BestScore _bestScore;

    private void Start()
    {
        _bestScore = Resources.Load<BestScore>("ScriptableObjects/BestScoreData");
        Text scoreText = GetComponent<Text>();
        scoreText.text = _bestScore.bestScore.ToString();
    }
}
