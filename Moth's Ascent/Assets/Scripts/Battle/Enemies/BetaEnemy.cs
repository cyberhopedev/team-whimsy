using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary> 
/// Subclass of Enemy.cs, utilizes blueprint in order to create a basic enemy for
/// testing
/// </summary>
public class TestEnemy : Enemy
{
    /// <summary>
    /// Damage dealt to the player when this enemy attacks
    /// </summary>
    public int attackDamage = 8;

    /// <summary>
    /// Once the enemy is instantiated, set its max HP and current HP
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        maxHP = 50;
        currentHP = maxHP;
    }

    /// <summary>
    /// When the enemy attacks, it will deal damage to the player and then end its turn after a delay
    /// </summary>
    /// <param name="p"> The player to attack </param>
    protected override void Attack(PlayerBattler p)
    {
        p.TakeDamage(attackDamage);
        StartCoroutine(DelayEndTurn(turnDuration));
    }

    /// <summary>
    /// When the enemy takes damage, it reduces its current HP and updates the health bar. 
    /// If HP drops to 0 or below, it deactivates itself to simulate death.
    /// </summary>
    /// <param name="amount"> The amount of damage to take </param>
    public override void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(0, currentHP - amount);
        if (healthBar != null)
        {
            healthBar.value = currentHP;    
        }

        if (currentHP <= 0)
        {
            // Die condition - disable sprite, trigger animation etc.
            gameObject.SetActive(false);
        }
    }
}