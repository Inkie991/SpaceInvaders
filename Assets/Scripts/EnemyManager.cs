using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IGameManager
{
    [SerializeField] private GameObject enemyLinePrefab;
    [SerializeField] private List<GameObject> enemiesLines;
    
    public ManagerStatus Status { get; private set; }
    
    private const float enemyLineOffsetY = 0.4f;
    private const float enemyLineOffsetX = 0.25f;
    private readonly Vector3 enemyLineSpawnPoint = new Vector3(0, 3.7f, 0);
    [SerializeField] private bool gameStarted = false;
    [SerializeField] private int moveDirection = 1;
    [SerializeField] private int movesToStartCount = 0;
    

    // Initialize the list
    public void Startup()
    {
        Debug.Log("Enemy manager starting...");
        enemiesLines = new List<GameObject>();
        Status = ManagerStatus.Started;
    }

    // Spawn the first enemies
    public void SpawnFirstEnemies()
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
                
                foreach (var line in enemiesLines)
                {
                    MoveLineY(line);
                }
                
                enemiesLines.Add((enemyLine));
            }
        }

        StartCoroutine(LinesMoving());
        gameStarted = true;
    }

    // Spawn a line of enemies
    private void SpawnNewLine()
    {
        GameObject enemyLine = Instantiate(enemyLinePrefab);
        enemyLine.transform.position = enemyLineSpawnPoint;
        enemiesLines.Add(enemyLine);
    }

    // Move enemies to the right or the left
    private void MoveLineX(GameObject line, int direction)
    {
        line.transform.Translate(Vector2.left * (direction * enemyLineOffsetX) );
    }

    // Move enemies down
    private void MoveLineY(GameObject line)
    {
        line.transform.Translate(Vector2.down * enemyLineOffsetY );
    }

    // Update enemies position and remove empty lines
    void Update()
    {
        if (gameStarted)
        {
            if (enemiesLines[0].transform.position.x < -1.2)  
            {
                moveDirection = -1;
            } else if (enemiesLines[0].transform.position.x > 1.2)
            {
                moveDirection = 1;
            }
            
            if (enemiesLines[0].transform.childCount == 0 && enemiesLines.Count > 1)
            {
                Destroy(enemiesLines[0]);
                enemiesLines.RemoveAt(0);
            }
        }
    }

    // Move lines with delay
    private IEnumerator LinesMoving()
    {
        while (true)
        {
            
            foreach (var line in enemiesLines)
            {
                MoveLineX(line, moveDirection);
            }
            
            yield return null;

            Enemy[] enemiesInFirstLine = enemiesLines[0].GetComponentsInChildren<Enemy>();
            foreach (var enemy in enemiesInFirstLine)
            {
                enemy.TryShoot();
            }

            yield return null;
            
            if (enemiesLines[0].transform.position.x == 0) movesToStartCount++;

            if (movesToStartCount == 2)
            {
                foreach (var line in enemiesLines)
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
