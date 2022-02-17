using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BestScoreData", menuName = "ScriptableObjects/Best Score", order = 51)]
public class BestScore : ScriptableObject
{
    // This class used for saving bestScore
    public int bestScore;
}
