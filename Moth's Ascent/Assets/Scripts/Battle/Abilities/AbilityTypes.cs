using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ability
{
    CLAW,
    BITE,
    STRUGGLE, 
    ACID_SPIT,
    RAISE_ARMS,
    EXOSKELETON,
    FILTER_FLUFF,
    GLITTER,
    DRAIN,
    NONE
}

// Ability types (attacks are now categorized as abilities too)
public static class AbilityTypes
{
    // Getter for the name of attack
    public static string GetName(this Ability ability)
    {
        return ability switch
        {
            Ability.CLAW => "Claw",
            Ability.BITE => "Bite",
            Ability.STRUGGLE => "Struggle", 
            Ability.ACID_SPIT => "Acid Spit",
            Ability.RAISE_ARMS => "Raise Arms",
            Ability.EXOSKELETON => "Exoskeleton",
            Ability.FILTER_FLUFF => "Filter Fluff",
            Ability.GLITTER => "Glitter",
            Ability.NONE => "None",
            _ => string.Empty,
        };
    }

    public static string GetDescription(this Ability ability)
    {
        return ability switch
        {
            Ability.CLAW => "Physical attack. Deal 10 damage",
            Ability.BITE => "Physical attack. Deal 20 damage",
            Ability.STRUGGLE => "Physical attack. Deal 5 damage", 
            Ability.ACID_SPIT => "Special Attack. Deal 8 damage and slightly reduce enemy's defense",
            Ability.GLITTER => "Magical Ability. Halves your opponents defense",
            Ability.RAISE_ARMS => "Defensive Ability. Raises defense greatly for one turn",
            Ability.EXOSKELETON => "Defensive Ability. Raises defense for the duration of the fight",
            Ability.FILTER_FLUFF => "Defensive Ability. Protects against poison and spores",
            Ability.NONE => "No more abilities",
            _ => string.Empty,
        };
    }

    public static int GetDamageAmount(this Ability attack)
    {
        return attack switch
        {
            Ability.CLAW => 10,
            Ability.BITE => 20,
            Ability.STRUGGLE => 5, 
            Ability.ACID_SPIT => 8,
            Ability.RAISE_ARMS => 0,
            Ability.EXOSKELETON => 0,
            Ability.FILTER_FLUFF => 0,
            Ability.GLITTER => 0,
            Ability.DRAIN => 10,
            _ => 0,
        };
    }

    public static Boolean IsDefensive(this Ability attack)
    {
        return attack switch
        {
            Ability.RAISE_ARMS => true,
            Ability.EXOSKELETON => true,
            Ability.FILTER_FLUFF => true,
            _ => false,
        };
    }

    /// <summary>
    /// Returns the status effect type this ability applies to the target (or NONE).
    /// </summary>
    public static StatusEffectType GetAppliedStatusEffect(this Ability ability)
    {
        return ability switch
        {
            Ability.ACID_SPIT    => StatusEffectType.ACID,
            Ability.RAISE_ARMS   => StatusEffectType.BLOCK,
            Ability.EXOSKELETON  => StatusEffectType.SHELL,
            Ability.FILTER_FLUFF => StatusEffectType.FILTER,
            Ability.GLITTER      => StatusEffectType.GLITTERING,
            _                    => StatusEffectType.NONE,
        };
    }


    public static Sprite GetIcon(this Ability ability)
    {
        return ability switch
        {
            Ability.CLAW => Resources.Load<Sprite>("Sprites/sharpenedhand 1"),
            Ability.BITE => Resources.Load<Sprite>("Sprites/bite 1"),
            Ability.ACID_SPIT => Resources.Load<Sprite>("Sprites/acidspit 1"),
            Ability.GLITTER => Resources.Load<Sprite>("Sprites/sparkles 1"),
            Ability.RAISE_ARMS => Resources.Load<Sprite>("Sprites/armsup 1"),
            Ability.EXOSKELETON => Resources.Load<Sprite>("Sprites/exoskeleton 1"),
            Ability.FILTER_FLUFF => Resources.Load<Sprite>("Sprites/leafmask 1"),
            Ability.NONE => Resources.Load<Sprite>("Sprites/lock 1"),
            _ => null,
        };
    }
}