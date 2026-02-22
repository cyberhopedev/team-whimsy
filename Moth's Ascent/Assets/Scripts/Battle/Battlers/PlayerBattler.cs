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

    // Call when enemy attacks
    public override void TakeDamage(int amount)
    {
        currentHP -= amount;
        healthBar.value = currentHP;

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
        healthBar.value = currentHP;
    }

    // Call to get health back to 100%
    public void RefillHealth()
    {
        currentHP = maxHP;
        healthBar.value = currentHP;
    }
}