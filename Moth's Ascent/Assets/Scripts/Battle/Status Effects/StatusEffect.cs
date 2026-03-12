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
}