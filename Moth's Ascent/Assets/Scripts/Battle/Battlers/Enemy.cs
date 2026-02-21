using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Battler
{
    protected PlayerBattler player;

    public delegate void StartTurnEventHandler(Enemy enemy);
    public event StartTurnEventHandler OnStartTurn;
    public delegate void EndTurnEventHandler();
    public event EndTurnEventHandler OnEndTurn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        player = BattleSystem.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Attacks the player, causing them to take damage
    protected virtual void Attack(PlayerBattler p)
    {
        
    }

    // When the turn for the enemy starts, invoke this method
    protected override void StartTurn()
    {
        OnStartTurn?.Invoke(this);
        Attack(player);
    }

    // When the turn for the enemy ends, invoke this method
    protected override void EndTurn()
    {
        OnEndTurn?.Invoke();
    }

    // If the battle is still going, slowly decrement the turn timer
    protected void OnDestroy()
    {
        
    }

    public IEnumerator DelayEndTurn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        EndTurn();
    }
}
