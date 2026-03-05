using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attack
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
    VINE
}

public static class AttackAbilityTypes
{
    // Getter for the name of attack
    public static string GetName(this Attack attack)
    {
        return attack switch
        {
            Attack.CLAW => "Claw",
            Attack.BITE => "Bite",
            Attack.STRUGGLE => "Struggle", 
            Attack.ACID_SPIT => "Acid Spit",
            Attack.RAISE_ARMS => "Raise Arms",
            Attack.EXOSKELETON => "Exoskeleton",
            Attack.FILTER_FLUFF => "Filter Fluff",
            Attack.GLITTER => "Glitter",
            Attack.DRAIN => "Drain",
            Attack.VINE => "Vine",
            _ => string.Empty,
        };
    }

    public static int GetDamageAmount(this Attack attack)
    {
        return attack switch
        {
            Attack.CLAW => 0,
            Attack.BITE => 0,
            Attack.STRUGGLE => 0, 
            Attack.ACID_SPIT => 0,
            Attack.RAISE_ARMS => 0,
            Attack.EXOSKELETON => 0,
            Attack.FILTER_FLUFF => 0,
            Attack.GLITTER => 0,
            Attack.DRAIN => 0,
            Attack.VINE => 0,
            _ => 0,
        };
    }

    public static int GetHealthAmount(this Attack attack)
    {
        return attack switch
        {
            Attack.CLAW => 0,
            Attack.BITE => 0,
            Attack.STRUGGLE => 0, 
            Attack.ACID_SPIT => 0,
            Attack.RAISE_ARMS => 0,
            Attack.EXOSKELETON => 0,
            Attack.FILTER_FLUFF => 0,
            Attack.GLITTER => 0,
            Attack.DRAIN => 0,
            Attack.VINE => 0,
            _ => 0,
        };
    }
}