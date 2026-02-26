using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary> 
/// Subclass of Enemy.cs, utilizes blueprint in order to create a basic enemy for
/// testing
/// </summary>
public class TestEnemy : Enemy
{
    public int attackDamage = 8;

    private PlayerBattler player;

    protected override void Start()
    {
        base.Start();
        maxHP = 50;
        currentHP = maxHP;
        player = BattleSystem.Instance.Player;
    }

    protected void Attack()
    {
        player.TakeDamage(attackDamage);
        StartCoroutine(DelayEndTurn(turnDuration));
    }

    public override void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(0, currentHP - amount);
        if (healthBar != null) healthBar.value = currentHP;

        if (currentHP <= 0)
        {
            // Die condition - disable sprite, trigger animation etc.
            gameObject.SetActive(false);
        }
    }
}