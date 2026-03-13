using UnityEngine;

/// <summary>
/// Enum for status effects that can be applied to player or enemy
/// in battle
/// </summary>
public enum StatusEffectType
{
    NONE,
    POISON,
    SPORE_SICKNESS,
    GLITTERING,
    ACID,
    BLOCK,
    SHELL,
    FILTER
}

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class StatusEffectInstance
{
    // Type of status effect
    public StatusEffectType type;
    // Amount of damage dealt to the affected battler at the end of each turn
    public int damagePerTurn;
    // Number of turns remaining for the status effect
    public int turnsRemaining;

    /// <summary>
    /// Constructor for a status effect instance. 
    /// Initializes the type, damage per turn, and duration of the effect.
    /// </summary>
    /// <param name="type">The type of the status effect</param>
    /// <param name="damagePerTurn">The amount of damage dealt per turn</param>
    /// <param name="duration">The number of turns the effect lasts</param>
    public StatusEffectInstance(StatusEffectType type, int damagePerTurn, int duration)
    {
        this.type = type;
        this.damagePerTurn = damagePerTurn;
        this.turnsRemaining = duration;
    }

    /// <summary>
    /// Default duration in turns for each status effect.
    /// </summary>
    public static int GetDefaultDuration(StatusEffectType statusEffect)
    {
        return statusEffect switch
        {
            StatusEffectType.POISON         => 3,
            StatusEffectType.SPORE_SICKNESS => 3,
            StatusEffectType.GLITTERING     => 3,
            StatusEffectType.ACID           => 3,
            StatusEffectType.BLOCK          => 1,   // removed at start of next turn
            StatusEffectType.SHELL          => 5,
            StatusEffectType.FILTER         => 3,
            _                               => 0,
        };
    }

    public static string GetName(StatusEffectType statusEffect)
    {
        return statusEffect switch
        {
            StatusEffectType.NONE           => "None",
            StatusEffectType.POISON         => "Poison",
            StatusEffectType.SPORE_SICKNESS => "Spore Sickness",
            StatusEffectType.GLITTERING     => "Glittering",
            StatusEffectType.ACID           => "Acid",
            StatusEffectType.BLOCK          => "Block",
            StatusEffectType.SHELL          => "Shell",
            StatusEffectType.FILTER         => "Filter",
            _                               => string.Empty,
        };
    }

    public static string GetDescription(StatusEffectType statusEffect)
    {
        return statusEffect switch
        {
            StatusEffectType.NONE           => "No status effect.",
            StatusEffectType.POISON         => "Deals 1 damage at the start of each turn, ignoring defense.",
            StatusEffectType.SPORE_SICKNESS => "Reduces player speed by 10.",
            StatusEffectType.GLITTERING     => "Halves the defense of the affected battler.",
            StatusEffectType.ACID           => "Reduces defense by 2 per stack.",
            StatusEffectType.BLOCK          => "Reduces incoming damage for one turn (+6 defense).",
            StatusEffectType.SHELL          => "Increases defense by 2 per stack.",
            StatusEffectType.FILTER         => "Protects against Poison and Spore Sickness for 3 turns.",
            _                               => string.Empty,
        };
    }

    /// <summary>
    /// How much damage per turn this effect deals (0 = no damage).
    /// </summary>
    public static int GetDamageAmount(StatusEffectType statusEffect)
    {
        return statusEffect switch
        {
            StatusEffectType.POISON => 1,
            _ => 0,
        };
    }

    public static bool IsDefensive(StatusEffectType statusEffect)
    {
        return statusEffect switch
        {
            StatusEffectType.BLOCK  => true,
            StatusEffectType.SHELL  => true,
            StatusEffectType.FILTER => true,
            _                       => false,
        };
    }
}