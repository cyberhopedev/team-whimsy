using System;
using System.Collections;
using UnityEngine;

public class PlayerBattler : Battler
{
    // UI Events

    // BattleSystem Events
    public delegate void StartTurnEventHandler(PlayerBattler player);
    public event StartTurnEventHandler OnStartTurn;
    public delegate void EndTurnEventHandler();
    public event EndTurnEventHandler OnEndTurn;
    // CameraManager Events
    
    // Player Info
    public PlayerData data;
    public int AttackDMG => data.attackDamage;

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

    // Call when the player needs to attack
    public void Attack(Enemy enemy)
    {
        enemy.TakeDamage(AttackDMG);
        EndTurn();
    }

    // Call when enemy attacks
    public override void TakeDamage(int amount)
    {
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
        if (currentHP + amount >= maxHP)
        {
            currentHP = maxHP;
        } else
        {
            currentHP += amount;
        }
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
}