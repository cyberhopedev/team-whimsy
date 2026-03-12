using UnityEngine;

/// <summary>
/// Enum for status effects that can be applied to player or enemy
/// in battle
/// </summary>
public enum StatusEffect
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
    public StatusEffect type;
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
    public StatusEffectInstance(StatusEffect type, int damagePerTurn, int duration)
    {
        this.type = type;
        this.damagePerTurn = damagePerTurn;
        this.turnsRemaining = duration;
    }

    public static string GetName(this StatusEffect statusEffect)
    {
        return statusEffect switch
        {
            StatusEffect.NONE => "None",
            StatusEffect.POISON => "Poison",
            StatusEffect.SPORE_SICKNESS => "Spore Sickness",
            StatusEffect.GLITTERING => "Glittering",
            StatusEffect.ACID => "Acid",
            StatusEffect.BLOCK => "Block",
            StatusEffect.SHELL => "Shell",
            StatusEffect.FILTER => "Filter",
            _ => string.Empty,
        };
    }

    public static string GetDescription(this StatusEffect statusEffect)
    {
        return statusEffect switch
        {
            StatusEffect.NONE => "No status effect.",
            StatusEffect.POISON => "Deals -1 damage at the start of each turn, ignoring target's defense",
            StatusEffect.SPORE_SICKNESS => "Reduces player speed by -10",
            StatusEffect.GLITTERING => "Halfs the defense of the person effected",
            StatusEffect.ACID => "Lowers defense by -2",
            StatusEffect.BLOCK => "Reduces incoming damage for one turn by adding +6 to defense",
            StatusEffect.SHELL => "Increases defense by =2",
            StatusEffect.FILTER => "Protects against poison and spores for 3 turns",
            _ => string.Empty,
        };
    }

    public static int GetDamageAmount(this StatusEffect statusEffect)
    {
         return statusEffect switch
        {
            StatusEffect.POISON => 1,
            StatusEffect.SPORE_SICKNESS => 0,
            StatusEffect.ACID => 0,
            StatusEffect.GLITTERING => 0,
            StatusEffect.BLOCK => 0,
            StatusEffect.SHELL => 0,
            StatusEffect.FILTER => 0,
            StatusEffect.NONE => 0,
            _ => 0,
        };
    }

    public static Boolean IsDefensive(this StatusEffect statusEffect)
    {
        return attack switch
        {
            StatusEffect.BLOCK => true,
            StatusEffect.SHELL => true,
            StatusEffect.FILTER => true,
            _ => false,
        };
    }

    public static Sprite GetIcon(this StatusEffect statusEffect)
    {
        return ability switch
        {
            StatusEffect.POISON => Resources.Load<Sprite>("Sprites/"),
            StatusEffect.SPORE_SICKNESS => Resources.Load<Sprite>("Sprites/"),
            StatusEffect.ACID => Resources.Load<Sprite>("Sprites/"),
            StatusEffect.GLITTERING => Resources.Load<Sprite>("Sprites/"),
            StatusEffect.BLOCK => Resources.Load<Sprite>("Sprites/"),
            StatusEffect.SHELL => Resources.Load<Sprite>("Sprites/"),
            StatusEffect.FILTER => Resources.Load<Sprite>("Sprites/"),
            StatusEffect.NONE => Resources.Load<Sprite>("Sprites/"),
            _ => null,
        };
    }
}