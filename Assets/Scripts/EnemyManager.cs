using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IGameManager
{
    private const float enemyLineOffsetY = 0.4f;
    private const float enemyLineOffsetX = 0.25f;
    private readonly Vector3 enemyLineSpawnPoint = new Vector3(0, 3.7f, 0);
    
    public ManagerStatus Status { get; private set; }
    [SerializeField] private GameObject enemyLinePrefab;
    [SerializeField] private List<GameObject> _enemysLines;
    private bool gameStarted = false;
    private int moveDirection = 1;
    private int movesToStartCount = 0;
    

    public void Startup()
    {
        Debug.Log("Enemy manager starting...");
        _enemysLines = new List<GameObject>();
        Status = ManagerStatus.Started;
    }

    public void SpawnFirstEnemys()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                SpawnNewLine();

            }
            else
            {
                GameObject enemyLine = Instantiate(enemyLinePrefab);
                enemyLine.transform.position = enemyLineSpawnPoint;
                foreach (var line in _enemysLines)
                {
                    MoveLineY(line);
                }
                _enemysLines.Add((enemyLine));
            }
        }

        //_enemysLines[0].name = "0";

        StartCoroutine(LinesMoveing());
        gameStarted = true;
    }

    private void SpawnNewLine()
    {
        GameObject enemyLine = Instantiate(enemyLinePrefab);
        enemyLine.transform.position = enemyLineSpawnPoint;
        _enemysLines.Add((enemyLine));
    }

    private void MoveLineX(GameObject line, int direction)
    {
        line.transform.Translate(Vector2.left * (direction * enemyLineOffsetX) );
    }

    private void MoveLineY(GameObject line)
    {
        line.transform.Translate(Vector2.down * enemyLineOffsetY );
    }

    void Update()
    {
        if (gameStarted)
        {
            if (_enemysLines[0].transform.position.x < -1.2)  
            {
                moveDirection = -1;
            } else if (_enemysLines[0].transform.position.x > 1.2)
            {
                moveDirection = 1;
            }
            
            if (_enemysLines[0].transform.childCount == 0 && _enemysLines.Count > 1)
            {
                Destroy(_enemysLines[0]);
                _enemysLines.RemoveAt(0);
            }
        }
    }

    private IEnumerator LinesMoveing()
    {
        while (true)
        {
            
            foreach (var line in _enemysLines)
            {
                MoveLineX(line, moveDirection);
            }
            
            yield return null;

            Enemy[] enemiesInFirstLine = _enemysLines[0].GetComponentsInChildren<Enemy>();
            foreach (var enemy in enemiesInFirstLine)
            {
                enemy.TryShoot();
            }

            yield return null;
            
            if (_enemysLines[0].transform.position.x == 0) movesToStartCount++;

            if (movesToStartCount == 2)
            {
                foreach (var line in _enemysLines)
                {
                    MoveLineY(line);
                }
                SpawnNewLine();
                movesToStartCount = 0;
            }

            yield return new WaitForSeconds(1f);
        }
    }
    
    
}
