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

    // Raise Arms: one-hit 50% damage reduction
    private bool _shieldActive = false;
    // Exoskeleton: flat -5 damage reduction per turn (tracks remaining turns)
    private int _exoskeletonTurns = 0;
    // Sturdy Branch: 3 uses per instance
    private int _sturdyBranchUses = 0;

    // Reset to full health each battle
    protected override void Awake()
    {
        base.Awake();
        data.currentHP = data.maxHP;
        currentHP = data.currentHP;
        maxHP = data.maxHP;
        speedStat = data.speedStat;
        defenseStat = 0;
    }


    // When the turn for the player starts, invoke this method
    protected override void StartTurn()
    {
        // Tick down exoskeleton turns
        if(_exoskeletonTurns > 0)
        {
            _exoskeletonTurns--;
        }
        OnStartTurn?.Invoke(this);
    }

    // When the turn for the player ends, invoke this method
    protected override void EndTurn()
    {
        OnEndTurn?.Invoke();
    }

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
            // Offensive Moves
            case Ability.STRUGGLE:
            case Ability.CLAW:
                enemy.TakeDamage(baseDamage);
                break;

            case Ability.BITE:
                enemy.TakeDamage(baseDamage);
                break;

            case Ability.ACID_SPIT:
                enemy.TakeDamage(baseDamage);
                // Applies Acid: -2 defense to enemy
                enemy.ApplyStatusEffect(new StatusEffectInstance(
                    StatusEffectType.ACID,
                    damagePerTurn: 0,
                    duration: StatusEffectInstance.GetDefaultDuration(StatusEffectType.ACID)));
                break;

            case Ability.GLITTER:
                // Halves ALL enemies' defense
                foreach (Enemy e in BattleSystem.Instance.enemies)
                {
                    e.ApplyStatusEffect(new StatusEffectInstance(
                        StatusEffectType.GLITTERING,
                        damagePerTurn: 0,
                        duration: StatusEffectInstance.GetDefaultDuration(StatusEffectType.GLITTERING)));
                }
                break;

            case Ability.DRAIN:
                // Deal damage and heal player for same amount
                int drainAmt = Mathf.Max(0, baseDamage - enemy.defenseStat);
                enemy.TakeDamage(drainAmt);
                RestoreHealth(drainAmt);
                Debug.Log($"Drain: dealt {drainAmt} and healed {drainAmt}.");
                break;

            // Defensive Moves(no target needed, fall through to self-use)
            case Ability.RAISE_ARMS:
            case Ability.EXOSKELETON:
            case Ability.FILTER_FLUFF:
                UseAbility(move);
                return; // EndTurn already called inside UseAbility(Ability)
        }

        // TODO: Add SFX and/or animation here?
        EndTurn();
    }

    /// <summary>
    /// Uses a self-targeted / defensive ability.
    /// </summary>
    public void UseAbility(Ability ability)
    {
        switch (ability)
        {
            case Ability.RAISE_ARMS:
                _shieldActive = true;
                ApplyStatusEffect(new StatusEffectInstance(StatusEffectType.BLOCK, damagePerTurn: 0, duration: 1));
                Debug.Log("Raise Arms: 50% damage reduction active for next hit.");
                break;

            case Ability.EXOSKELETON:
                _exoskeletonTurns = 5; // lasts the fight effectively (matches design doc "duration of fight")
                ApplyStatusEffect(new StatusEffectInstance(StatusEffectType.SHELL, damagePerTurn: 0, duration: 5));
                Debug.Log("Exoskeleton: flat -5 damage reduction active.");
                break;

            case Ability.FILTER_FLUFF:
                // Remove existing Poison and Spore Sickness
                activeStatusEffects.RemoveAll(e =>
                    e.type == StatusEffectType.POISON ||
                    e.type == StatusEffectType.SPORE_SICKNESS);
                // Apply Filter to block future afflictions
                ApplyStatusEffect(new StatusEffectInstance(StatusEffectType.FILTER, damagePerTurn: 0, duration: 3));
                Debug.Log("Filter Fluff: Poison and Spore Sickness cleared. Protected for 3 turns.");
                break;
        }

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

        // Defense stat (Shell, Acid, Glittering affect this)
        amount = Mathf.Max(0, amount - Mathf.Max(0, defenseStat));

        data.currentHP = Mathf.Max(0, data.currentHP - amount);
        currentHP = data.currentHP;
        if(healthBar != null)
        {
            healthBar.value = currentHP;
        }
        Debug.Log("Player takes " + amount + " amount of damage!");
        
        // Die condition
        if (currentHP <= 0)
        {
            Debug.Log("Player has died.");
            // change sprite, destroy object ?
        }
    }

    // Call when character gets healed?
    public void RestoreHealth(int amount)
    {
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

    protected override void OnStatusEffectExpired(StatusEffectInstance se)
    {
        base.OnStatusEffectExpired(se);
        if (se.type == StatusEffectType.SPORE_SICKNESS)
        {
            speedStat += 10; // restore the speed reduction
            Debug.Log($"Spore Sickness wore off. Speed restored to {speedStat}.");
        }
    }

    public override void ApplyStatusEffect(StatusEffectInstance newEffect)
    {
        if (newEffect.type == StatusEffectType.SPORE_SICKNESS)
        {
            if (hasStatusEffect(StatusEffectType.FILTER))
            {
                Debug.Log("Filter active – Spore Sickness blocked!");
                return;
            }
            if (!hasStatusEffect(StatusEffectType.SPORE_SICKNESS))
            {
                speedStat = Mathf.Max(0, speedStat - 10);
                Debug.Log($"Spore Sickness applied. Speed reduced to {speedStat}.");
            }
        }
        base.ApplyStatusEffect(newEffect);
    }

    /// <summary>
    /// Uses an item from the inventory during battle.
    /// </summary>
    public void UseItem(Items item, Enemy enemy = null)
    {
        switch (item)
        {
            case Items.MEALBERRY:
                RestoreHealth(15);
                Debug.Log("Mealberry: restored 15 HP.");
                break;

            case Items.MEDICINAL_ROOT:
                activeStatusEffects.RemoveAll(e => e.type == StatusEffectType.POISON);
                Debug.Log("Medicinal Root: all Poison removed.");
                break;

            case Items.POISON_SHROOM:
                if (enemy != null)
                {
                    enemy.TakeDamage(5);
                    enemy.ApplyStatusEffect(new StatusEffectInstance(
                        StatusEffectType.POISON, damagePerTurn: 1,
                        duration: StatusEffectInstance.GetDefaultDuration(StatusEffectType.POISON)));
                    Debug.Log("Poison Shroom: 5 damage + Poison applied to enemy.");
                }
                break;

            case Items.STURDY_BRANCH:
                if (_sturdyBranchUses <= 0) _sturdyBranchUses = Items.STURDY_BRANCH.GetMaxUses();
                if (_sturdyBranchUses > 0 && enemy != null)
                {
                    enemy.TakeDamage(15);
                    _sturdyBranchUses--;
                    Debug.Log($"Sturdy Branch: 15 damage. Uses left: {_sturdyBranchUses}.");
                }
                break;
        }

        EndTurn();
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