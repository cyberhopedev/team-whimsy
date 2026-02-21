using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State of the battle
public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    // Current battle state
    public BattleState state;
    // Player reference
    private PlayerBattler _player;
    public PlayerBattler Player => _player;
    // List of enemies
    public List<Enemy> enemies;
    // Makes instance of the system accesible to child classes
    public static BattleSystem Instance {get; private set;}

    /// <summary> 
    /// Ensure that the battle system is a singleton instance
    /// </summary>
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary> 
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        state =  BattleState.START;
        SetupBattle();
    }

    // TODO: Complete the BattleSystem and it's associated relationship for basic attacking damage

    /// <summary> 
    /// SetupBattle is called at the start, allowing the battle mechanics
    /// </summary>
    void SetupBattle()
    {
        
    }
}
