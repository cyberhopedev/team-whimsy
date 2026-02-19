using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // State of the battle
    public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    // Current battle state
    public BattleState state;
    // List of enemies
    public List<Enemy> enemies;

    /// <summary> 
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        state =  BattleState.START;
        SetupBattle();
    }

    /// <summary> 
    /// SetupBattle is called at the start, allowing the battle mechanics
    /// </summary>
    void SetupBattle()
    {
        
    }
}
