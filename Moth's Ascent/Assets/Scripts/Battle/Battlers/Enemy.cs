using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Battler
{
    protected PlayerBattler player;
    protected float turnDuration = 1f;
    public delegate void StartTurnEventHandler(Enemy enemy);
    public event StartTurnEventHandler OnStartTurn;
    public delegate void EndTurnEventHandler();
    public event EndTurnEventHandler OnEndTurn;
    public PlayerData data;
    public int AttackDMG => data.attackDamage / 2;

    // Temporary(?) fix for player being null
    private void OnEnable()
    {
        if (BattleSystem.Instance != null)
        {
            player = BattleSystem.Instance.Player;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        // player = BattleSystem.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Attacks the player, causing them to take damage
    protected virtual void Attack(PlayerBattler p)
    {
        player = BattleSystem.Instance.Player;

        player.TakeDamage(10);
    }

    // When the turn for the enemy starts, invoke this method
    protected override void StartTurn()
    {
        Debug.Log("Enemy turn starts here");
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

    // Call when losing HP
    public override void TakeDamage(int amount)
    {
        currentHP -= amount;
        healthBar.value = currentHP;
        Debug.Log("current enemy health val: " + healthBar.value);
        Debug.Log("Enemy health: " + healthBar.value);

        // Die condition
        if (currentHP <= 0)
        {
            // change sprite, destroy object ?
        }
    }
}
