using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Subclass of Enemy.cs, utilizes blueprint in order to create a overgrown enemy
/// class. These will have a lower defense and vine themed attacks.
/// 
/// Attacks will be vine-themed and the overgrown can consume corpses to heal and grow.
/// </summary>
public class OvergrownEnemy : Enemy
{
    // Corpse consumption healing amount
    [SerializeField] private int corpseHealAmount = 20;

    protected override void Awake()
    {
        base.Awake();

        // Set max HP lower than other enemies due to slower movement and reliance on defense
        maxHP = 60;
        currentHP = maxHP;
    }

    // /// <summary>
    // /// When the enemy attacks, it will deal damage to the player and then end its turn after a delay
    // /// </summary>
    // /// <param name="p"> The player to attack </param>
    // protected override void Attack(PlayerBattler p)
    // {
    //     Ability[] currentMoves = {global::Attack.VINE, global::Attack.DRAIN};
    //     Ability chosenMove = currentMoves[Random.Range(0, currentMoves.Length)];
        
    //     // If the chosen move is the drain attack, heal self before dealing damage to the player
    //     if(chosenMove == global::Attack.DRAIN)
    //     {
    //         // Heal self for drain attack
    //         currentHP = Mathf.Min(maxHP, currentHP + corpseHealAmount);
    //         if (healthBar != null)
    //         {
    //             healthBar.value = currentHP;    
    //         }
    //     }

    //     int attackDamage = chosenMove.GetDamageAmount();

    //     p.TakeDamage(attackDamage);
    //     StartCoroutine(DelayEndTurn(turnDuration));
    // }

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

        // // Spore release effect when taking damage
        // PlayerBattler p = BattleSystem.Instance?.Player;
        // if (p != null)
        // {
        //     p.ApplyStatusEffect(new StatusEffectInstance(StatusEffectType.POISON, damagePerTurn: 5, duration: 3));
        // }

        if (currentHP <= 0)
        {
            // Die condition - disable sprite, trigger animation etc.
            gameObject.SetActive(false);
        }
    }
}