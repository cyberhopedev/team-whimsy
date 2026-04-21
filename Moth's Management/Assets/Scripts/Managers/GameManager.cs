using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    // Instance of the GameManager
    public static GameManager Instance { get; private set; }

    // The tick settings for the game to help manage passive generation of resources/corruption
    [Header("Tick Settings")]
    public float tickInterval = 2f;
    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tickInterval)
        {
            RunTick();
            timer = 0f;
        }
    }

    /// <summary>
    /// Runs all of the game logic that is based off ticks
    /// </summary>
    void RunTick()
    {
        // Notify all systems
        EventBus.OnTick?.Invoke();

        // Corruption system
        CorruptionManager.Instance.ProcessTick();

        // If we add monsters
        // MonsterManager.Instance.ProcessTick();

        // Win/Loss check
        if(TileManager.Instance.HasLost())
        {
            EventBus.OnGameOver?.Invoke(false);
        }
    }
}