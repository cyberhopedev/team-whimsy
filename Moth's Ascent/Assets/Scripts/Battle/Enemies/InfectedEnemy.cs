using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Subclass of Enemy.cs, utilizes blueprint in order to create a infected enemy
/// class. These will have higher defense and damage reduction due to exoskeleton, 
/// but will be slower.
/// 
/// Attack types will include biting and clawing, and will release poisounous spores
/// when attacked physically.
/// </summary>
public class InfectedEnemy : Enemy
{
    // 20% damage reduction due to exoskeleton
    [SerializeField] private float physicalDamageReduction = 0.2f;
    // Slower movement speed due to exoskeleton
    // [SerializeField] private float movementSpeedReduction = 0.3f;

    protected override void Awake()
    {
        base.Awake();
        // Apply movement speed and damage reduction
        // movementSpeed *= 1 - movementSpeedReduction;
        // physicalDamage *= 1 - physicalDamageReduction;

        // Set max HP lower than other enemies due to slower movement and reliance on defense
        maxHP = 80;
        currentHP = maxHP;
    }

    /// <summary>
    /// When the enemy attacks, it will deal damage to the player and then end its turn after a delay
    /// </summary>
    /// <param name="p"> The player to attack </param>
    protected override void Attack(PlayerBattler p)
    {
        Attack[] currentMoves = {global::Attack.BITE, global::Attack.CLAW};
        Attack chosenMove = currentMoves[Random.Range(0, currentMoves.Length)];
        int attackDamage = chosenMove.GetDamageAmount();

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
        // Apply exoskeleton damage reduction
        int reduced = Mathf.CeilToInt(amount * (1f - physicalDamageReduction));
        currentHP = Mathf.Max(0, currentHP - reduced);
        if (healthBar != null)
        {
            healthBar.value = currentHP;    
        }

        // Spore release effect when taking damage
        PlayerBattler p = BattleSystem.Instance?.Player;
        if (p != null)
        {
            p.ApplyStatusEffect(new StatusEffectInstance(StatusEffectType.SPORE_SICKNESS, damagePerTurn: 3, duration: 3));
            Debug.Log("Infected released spores! Player inflicted with Spore Sickness.");
        }

        if (currentHP <= 0)
        {
            // Die condition - disable sprite, trigger animation etc.
            gameObject.SetActive(false);
        }
    }
}