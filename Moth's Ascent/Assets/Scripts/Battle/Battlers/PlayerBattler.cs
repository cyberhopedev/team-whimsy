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
    public int attackDMG = 10;

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
}