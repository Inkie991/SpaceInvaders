using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MySceneManager))]
[RequireComponent(typeof(EnemyManager))]
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(GameProcessManager))]
public class Managers : MonoBehaviour
{
    public static MySceneManager Scene { get; private set; }
    public static EnemyManager Enemy { get; private set; }
    public static UIManager UI { get; private set; }
    public static AudioManager Audio { get; private set; }
    public static GameProcessManager GameProcess { get; private set; }

    private List<IGameManager> _startSequence;
    
    // Add all managers to the list
    void Awake()
    {
        Enemy = GetComponent<EnemyManager>();
        UI = GetComponent<UIManager>();
        Scene = GetComponent<MySceneManager>();
        Audio = GetComponent<AudioManager>();
        GameProcess = GetComponent<GameProcessManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Enemy);
        _startSequence.Add(UI);
        _startSequence.Add(Scene);
        _startSequence.Add(Audio);
        _startSequence.Add(GameProcess);

        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartupManagers());
    }

    // Startup all managers
    private IEnumerator<Object> StartupManagers()
    {
        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence)
            {
                if (manager.Status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
                Debug.Log("Progress: " + numReady + "/" + numModules);
            
            yield return null;
        }

        Debug.Log("All managers started up");
    }
}