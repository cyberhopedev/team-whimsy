using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine.UI;

/// <summary> 
/// A parent class for each battler type that sets up basic attributes and
/// calls the overriden methods from its children (PlayerBattler and Enemy)
/// </summary>
public abstract class Battler : MonoBehaviour
{
    // Initial max health points
    public int maxHP = 100;
    // Current health points value
    public int currentHP;
    // Checks if the battler has died based on if the current health points are < or = 0
    public bool IsDead => currentHP <= 0;
    // Health Bar Slider
    public Slider healthBar;
    // Speed Stat
    public int speedStat;

    /// <summary> 
    /// Constructs a Battler with their assosciated stats
    /// </summary>
    public Battler()
    {
        
    }

    /// <summary> 
    /// Start is called before the first frame update; Sets the current health and
    /// mana while ensuring that the battle system is in a valid state and initializes
    /// any abilities
    /// </summary>
    protected virtual void Start()
    {
        // At the first frame, make sure the current health points are max
        currentHP = maxHP;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHP;
            healthBar.value = currentHP;
        }
    }

    /// <summary> 
    /// Calls the BattleSystem to begin the Battler's turn
    /// </summary>
    public void BeginTurn()
    {
        StartTurn();
    }

    /// <summary> 
    /// Calls the overriden methods in the Battler's children classes
    /// </summary>
    public abstract void TakeDamage(int amount);
    protected abstract void StartTurn();
    protected abstract void EndTurn();
}