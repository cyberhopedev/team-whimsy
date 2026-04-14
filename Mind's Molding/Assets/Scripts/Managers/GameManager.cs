using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event System.Action<float> OnInfectionChanged;
    public event System.Action<GameState> OnGameStateChanged;

    // Game state
    public enum GameState { Playing, Paused, GameOver, Victory }
    public GameState CurrentState { get; private set; }

    // Infection progress (0 = clean, 1 = fully consumed)
    public float InfectionLevel { get; private set; }

    private void Awake()
    {
        // Singleton instance
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
    }

    private void SetState(GameState newState)
    {
        CurrentState = newState;
        OnGameStateChanged?.Invoke(newState);
    }

    public void SetInfectionLevel(float value)
    {
        
    }

    public void TriggerVictory()
    {
        
    }

    private void TriggerGameOver()
    {
        
    }
}