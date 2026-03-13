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
    // Defense stat – modified by Shell/Block/Glittering/Acid effects
    public int defenseStat = 0;
    // List of active status effects on the battler
    public List<StatusEffectInstance> activeStatusEffects = new List<StatusEffectInstance>();

    /// <summary> 
    /// Sets the current health and
    /// mana while ensuring that the battle system is in a valid state and initializes
    /// any abilities
    /// </summary>
    protected virtual void Awake()
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
    /// Calls the BattleSystem to begin the Battler's turn. Processes
    /// the status effects that are currently active
    /// </summary>
    public void BeginTurn()
    {
        ProcessStatusEffects();
        if (!IsDead)
        {
            StartTurn();
        }
    }

    /// <summary> 
    /// Calls the overriden methods in the Battler's children classes
    /// </summary>
    public abstract void TakeDamage(int amount);
    protected abstract void StartTurn();
    protected abstract void EndTurn();

    /// <summary>
    /// Damage that bypasses the defense stat (Poison, etc.).
    /// </summary>
    public void TakeDamageIgnoreDefense(int amount)
    {
        currentHP = Mathf.Max(0, currentHP - amount);
        if(healthBar != null) {
            healthBar.value = currentHP;
        }
    }

    /// <summary>
    /// Called at the start of each turn to tick down status effects
    /// and apply their damage to the inflicted battler. If the battler dies,
    /// the status effect is removed and battler's death is handled by TakeDamage()
    /// </summary>
    protected void ProcessStatusEffects()
    {
        foreach(StatusEffectInstance se in activeStatusEffects.ToList())
        {
            // Tick damage
            if (se.damagePerTurn > 0)
            {
                TakeDamage(se.damagePerTurn);
                Debug.Log(gameObject.name + " takes " + se.damagePerTurn + " damage from " + se.type + " and has " + currentHP + " HP left!");
            }

            // Decrement remaining turns and remove if expired
            se.turnsRemaining--;
            if (se.turnsRemaining <= 0)
            {
                activeStatusEffects.Remove(se);
                Debug.Log(se.type + " on " + gameObject.name + " has wore off!");
            }
        }
    }

    /// <summary>
    /// Reverse any stat changes when a status effect expires.
    /// </summary>
    protected virtual void OnStatusEffectExpired(StatusEffectInstance se)
    {
        switch (se.type)
        {
            case StatusEffectType.SHELL:
                defenseStat -= 2;
                break;
            case StatusEffectType.ACID:
                defenseStat += 2;   // restore the reduction
                break;
            case StatusEffectType.GLITTERING:
                defenseStat *= 2;   // restore halved defense
                break;
            // BLOCK and SPORE_SICKNESS reversals are handled in PlayerBattler/Enemy subclasses
        }
    }

    /// <summary>
    /// Applies a new status effect to the battler, ensuring that the same effect type cannot stack. 
    /// If the same effect type is already active, the new effect will not be applied
    /// </summary>
    /// <param name="newEffect">The new status effect to apply</param>
    public void ApplyStatusEffect(StatusEffectInstance newEffect)
    {
        // Filter blocks Poison and Spore Sickness
        if((newEffect.type == StatusEffectType.POISON || newEffect.type == StatusEffectType.SPORE_SICKNESS)
            && hasStatusEffect(StatusEffectType.FILTER))
        {
            Debug.Log($"{gameObject.name} is protected by Filter – {newEffect.type} blocked!");
            return;
        }

        // Make sure the same effect type cannot stack, instead make it do nothing
        foreach (var existing in activeStatusEffects)
        {
            if (existing.type == newEffect.type)
            {
                Debug.Log(gameObject.name + " already has " + newEffect.type + " and cannot be applied again!");
                return;
            }
        }

        // Apply the new effect
        activeStatusEffects.Add(newEffect);

        // Immediate stat side-effects on application
        switch (newEffect.type)
        {
            case StatusEffectType.SHELL:
                defenseStat += 2;
                Debug.Log($"{gameObject.name} Shell: +2 defense (now {defenseStat})");
                break;
            case StatusEffectType.ACID:
                defenseStat -= 2;
                Debug.Log($"{gameObject.name} Acid: -2 defense (now {defenseStat})");
                break;
            case StatusEffectType.GLITTERING:
                defenseStat = defenseStat / 2;
                Debug.Log($"{gameObject.name} Glittering: defense halved (now {defenseStat})");
                break;
        }
    }

    /// <summary>
    /// Helper method to check if the battler has a specific status effect type active. 
    /// Returns true if the effect is active, false otherwise.
    /// </summary>
    /// <param name="type">The type of status effect to check for</param>
    /// <returns>True if the effect is active, false otherwise</returns>
    public bool hasStatusEffect(StatusEffectType type)
    {
        return activeStatusEffects.Any(se => se.type == type);
    }
}