using System;
using System.Collections;
using UnityEngine;

public class PlayerBattler : Battler
{
    // BattleSystem Events
    public delegate void StartTurnEventHandler(PlayerBattler player);
    public event StartTurnEventHandler OnStartTurn;
    public delegate void EndTurnEventHandler();
    public event EndTurnEventHandler OnEndTurn;
    
    // Player Info
    public PlayerData data;
    public int AbilityDMG => data.attackDamage;
    private bool _shieldActive = false;
    private int _exoskeletonTurns = 0;

    // Reset to full health each battle
    protected override void Awake()
    {
        base.Awake();
        data.currentHP = data.maxHP; 
        currentHP = data.currentHP;
        maxHP = data.maxHP;
    }


    // When the turn for the player starts, invoke this method
    protected override void StartTurn()
    {
        OnStartTurn?.Invoke(this);
    }

    // When the turn for the player ends, invoke this method
    protected override void EndTurn()
    {
        OnEndTurn?.Invoke();
    }

    // // Call when the player needs to Ability
    // public void Ability(Enemy enemy)
    // {
    //     enemy.TakeDamage(AbilityDMG);
    //     EndTurn();
    // }

    /// <summary>
    /// Used to apply the effects of the chosen Ability move, then ends the player's turn
    /// </summary>
    /// <param name="move">The Ability move to use</param>
    /// <param name="enemy">The enemy to Ability</param>
    public void UseAbility(Ability move, Enemy enemy)
    {
        int baseDamage = move.GetDamageAmount();

        switch (move)
        {
            case Ability.CLAW:
                // No special effects, just damage
                enemy.TakeDamage(baseDamage);
                break;
            case Ability.BITE:
                // No special effects, just damage
                enemy.TakeDamage(baseDamage);
                break;
            case Ability.STRUGGLE:
                // No special effects, just damage
                enemy.TakeDamage(baseDamage);
                break;
            case Ability.ACID_SPIT:
                // Change to apply poison stats effect
                enemy.TakeDamage(baseDamage);
                enemy.ApplyStatusEffect(new StatusEffectInstance(StatusEffectType.POISON, damagePerTurn: 3, duration: 3));
                break;
            case Ability.GLITTER:
                // Hits all enemies for the same damage
                foreach (Enemy e in BattleSystem.Instance.enemies)
                {
                    e.TakeDamage(baseDamage);
                }
                break;
            case Ability.RAISE_ARMS:
                Debug.Log("status effect: block");
                break;
            case Ability.EXOSKELETON:
                Debug.Log("status effect: shell");
                break;
            case Ability.FILTER_FLUFF:
                Debug.Log("status effect: filter");
                break;
            case Ability.TESTING_CHEAT:
                enemy.TakeDamage(100);
                break;
        }

        // TODO: Add SFX and/or animation here?
        EndTurn();
    }

    public void UseAbility(Ability ability)
    {
        switch(ability)
        {
            case Ability.RAISE_ARMS:
                _shieldActive = true;
                Debug.Log("Raise Arms: damage reduction active next hit.");
                break;

            case Ability.EXOSKELETON:
                // Temporary flat defense buff — stored as turns
                _exoskeletonTurns = 2;
                Debug.Log("Exoskeleton active for 2 turns.");
                break;
            
            case Ability.FILTER_FLUFF:
                // Cleanse one poison/spore effect
                activeStatusEffects.RemoveAll(e =>
                e.type == StatusEffectType.POISON ||
                e.type == StatusEffectType.SPORE_SICKNESS);
                Debug.Log("Filter Fluff: status effects cleared.");
                break;
        }

        // TODO: Add SFX and/or animation here?
        EndTurn();
    }

    // Call when enemy Abilitys
    public override void TakeDamage(int amount)
    {   
        // If Raise Arms is active, damage reduction of 50%
        if (_shieldActive)
        {
            amount = Mathf.CeilToInt(amount / 2f);
            _shieldActive = false; // Reset shield after one hit
            Debug.Log("Raise Arms active: damage reduced to " + amount);
        }

        // If Exoskeleton is active, flat damage reduction of 5
        if (_exoskeletonTurns > 0)
        {
            amount = Mathf.Max(0, amount - 5);
            Debug.Log("Exoskeleton active: damage reduced by 5 to " + amount);
        }

        Debug.Log("Player takes " + amount + " amount of damage!");
        data.currentHP = Mathf.Max(0, data.currentHP - amount);
        currentHP = data.currentHP; 
        if (healthBar != null)
        {
            healthBar.value = currentHP;
        }
        // Die condition
        if (currentHP <= 0)
        {
            // change sprite, destroy object ?
        }
    }

    // Call when character gets healed?
    public void RestoreHealth(int amount)
    {
        // Make sure not to go over max health
        // if (currentHP + amount >= maxHP)
        // {
        //     currentHP = maxHP;
        // } else
        // {
        //     currentHP += amount;
        // }
        data.currentHP = Mathf.Min(data.currentHP + amount, data.maxHP);
        currentHP = data.currentHP;
        if (healthBar != null)
        {
            healthBar.value = currentHP;
        }
    }

    // Call to get health back to 100%
    public void RefillHealth()
    {
        data.currentHP = data.maxHP;
        currentHP = maxHP;
        if (healthBar != null)
        {
            healthBar.value = currentHP;
        }
    }

    /// <summary>
    /// Force the end of the player's turn, used for fleeing failure 
    /// or if an ability forces them to skip their next turn
    /// </summary>
    public void ForceEndPlayerTurn()
    {
        EndTurn();
    }
}