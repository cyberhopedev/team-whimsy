using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
// TODO: Add/call UI handler to update UI based on battle events

// State of the battle
public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST, FLED}


/// <summary> 
/// Manager for the turn-based battle system of Moth's Ascent
/// </summary>
public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Transform playerBattlePosition;
    public const string battleScene = "BattleMenuScene";
    public const string overworldScene = "BetaScene";
    
    // Convenience accessor for the first enemy (expand later for multi-enemy targeting)
    public Enemy FirstEnemy => enemies.Count > 0 ? enemies[0] : null;

    // Current battle state
    public BattleState state;
    // Player reference
    [SerializeField] private PlayerBattler _player;
    public PlayerBattler Player => _player;
    // List of enemies
    public List<Enemy> enemies;
    // List of battlers to follow a consistent order
    private List<Battler> _turnOrder = new List<Battler>();
    // Initial turn index
    private int _currentTurnIndex = 0;
    // Initial turn delay in seconds
    private float _turnDelaySeconds = 0.5f;
    // Custom unity event to assign listeners for the active turn
    [System.Serializable]
    public class ActiveTurnEvent : UnityEvent<BattleState> {}
    public static ActiveTurnEvent OnActiveTurnChanged = new ActiveTurnEvent();
    
    // Makes instance of the system accesible to child classes
    public static BattleSystem Instance {get; private set;}

    /// <summary> 
    /// Ensure that the battle system is a singleton instance
    /// </summary>
    private void Awake()
    {
        Instance = this;
        Debug.Log("BattleSystem instance ID: " + GetInstanceID());
        Debug.Log("BattleSystem Awake in scene: " + SceneManager.GetActiveScene().name);
    }

    /// <summary> 
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        state =  BattleState.START;
        _currentTurnIndex = -1; // AdvanceTurn adds one, so this will start it at 0
        if(BattleTransitionData.EncounterEnemies != null &&  BattleTransitionData.EncounterEnemies.Count > 0)
        {
            StartBattle(BattleTransitionData.EncounterEnemies);
            BattleTransitionData.EncounterEnemies = null;  // clear after done
        }
        else if (enemies.Count > 0)
        {
            if (_player != null && playerBattlePosition != null)
            {
                _player.transform.position = playerBattlePosition.position;
            }
            SetupBattle();
        }
        else
        {
            Debug.LogError("BattleSystem: No encounter data found!");
        }
    }

    /// <summary> 
    /// SetupBattle is called at the start, initializing listeners for events for enemies and the player
    /// </summary>
    void SetupBattle()
    {
        // Turn order setup where player goes first, then any enemies
        _turnOrder.Clear();
        _turnOrder.Add(_player);
        _turnOrder.AddRange(enemies);

        _player.OnStartTurn += StartTurn;
        _player.OnEndTurn += EndTurn;

        foreach(Enemy e in enemies) {
            e.OnStartTurn += StartTurn;
            e.OnEndTurn += EndTurn;
        }

        // Move on to the next turn once done
        AdvanceTurn();
    }

    /// <summary>
    /// Called by EnemyEncounter to begin a battle with a specific set of enemy prefabs
    /// </summary>
    public void StartBattle(List<GameObject> encounterEnemies)
    {
        // Clean up any existing enemies from a previous battle
        foreach (Enemy e in enemies)
        {
            e.OnStartTurn -= StartTurn;
            e.OnEndTurn -= EndTurn;
            Destroy(e.gameObject);
        }
        enemies.Clear();

        // Spawn new enemies from the encounter trigger
        foreach (GameObject prefab in encounterEnemies)
        {
            GameObject obj = Instantiate(prefab);
            Enemy e = obj.GetComponent<Enemy>();
            if (e != null) enemies.Add(e);
        }

        state = BattleState.START;
        _currentTurnIndex = -1; // AdvanceTurn adds one, so this will start it at 0
        SetupBattle();
    }

    /// <summary> 
    /// ChoseAttack(Enemy enemy) is used when it is the player's turn and the regular attack option is chosen
    /// </summary>
    public void ChoseAttack(Enemy enemy)
    {
        if(state == BattleState.PLAYERTURN)
        {
            Player.Attack(enemy);
        }
    }

    /// <summary> 
    /// StartTurn(PlayerBattler p) is used when it is the player's turn to set it up, in the future add's a UI handler event
    /// </summary>
    private void StartTurn(PlayerBattler p)
    {
        state = BattleState.PLAYERTURN;
        OnActiveTurnChanged?.Invoke(state);
    }

    /// <summary> 
    /// StartTurn(Enemy e) is used when it is the enemy's turn to set it up, in the future add's a UI handler event
    /// </summary>
    private void StartTurn(Enemy e)
    {
        state = BattleState.ENEMYTURN;
        OnActiveTurnChanged?.Invoke(state);
    }

    /// <summary> 
    /// Ends the turn for the current entity while allowing any animations to play
    /// </summary>
    private void EndTurn()
    {
        // Delay to allow for any remaining animations to finish.
        StartCoroutine(EndTurnDelay(_turnDelaySeconds));
    }

    /// <summary> 
    /// Initializes the end turn delay with seconds, then advances to the next turn
    /// </summary>
    private IEnumerator EndTurnDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        AdvanceTurn();
    }

    // Exit if player chooses to flee
    public void Flee()
    {
        {
            state = BattleState.FLED;
            // Get back to overworld scene
            StartCoroutine(ReturnToOverworld(2f));
            return;
        }
    }

    /// <summary> 
    /// Checks each possible state to see if the next turn is ready to be called and who goes next
    /// </summary>
    private void AdvanceTurn()
    {
        // Check win/loss conditions before continuing
        if(AllEnemiesDead())
        {
            state = BattleState.WON;
            OnActiveTurnChanged?.Invoke(state);
            // Get back to overworld scene
            StartCoroutine(ReturnToOverworld(2f));
            return;
        }

        if (_player.IsDead)
        {
            state = BattleState.LOST;
            OnActiveTurnChanged?.Invoke(state);
            // Get back to overworld scene
            StartCoroutine(ReturnToOverworld(2f));
            return;
        }

        // Cycle through the turn order, skipping dead enemies
        Battler next = null;
        int attempts = 0;

        while (attempts < _turnOrder.Count)
        {
            _currentTurnIndex = (_currentTurnIndex + 1) % _turnOrder.Count;
            Battler candidate = _turnOrder[_currentTurnIndex];

            if (!candidate.IsDead)
            {
                next = candidate;
                break;
            }
            attempts++;
        }

        next?.BeginTurn();
    }

    /// <summary> 
    /// Helper method that sets up the coroutine in order to return to the overworld dungeon crawler
    /// </summary>
    private IEnumerator ReturnToOverworld(float delay)
    {
        // Add item to inventory
        // InventoryManager.AddItem();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(overworldScene);
    }

    /// <summary> 
    /// Helper method to check if all of the enemies are dead for the PLAYERWON condition to be met
    /// </summary>
    private bool AllEnemiesDead()
    {
        // Don't count as won if no enemies have been set up yet
        if(enemies == null || enemies.Count == 0)
        {
            return false;
        }

        // For each enemy in the encounter, check if they are alive
        foreach (Enemy e in enemies)
        {
            if (!e.IsDead)
            {
                return false;
            }
        }
        return true;
    }

    // figure out why BattleSystem is getting destroyed when BattleMenu pops up
    private void OnDestroy()
    {
        Debug.Log("BattleSystem destroyed! Stack trace: " + System.Environment.StackTrace);
        // Remove listeners to prevent memory leaks
        OnActiveTurnChanged.RemoveAllListeners();
        if(Instance == this)
        {
            Instance = null;
        }
    }
}
