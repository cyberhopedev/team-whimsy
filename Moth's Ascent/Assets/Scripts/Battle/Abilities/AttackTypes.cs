using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attack
{
    CLAW,
    BITE,
    STRUGGLE, 
    ACID_SPIT,
    GLITTER,
    DRAIN,
    VINE
}

public static class AttackTypes
{
    public static string GetName(this Attack attack)
    {
        return attack switch
        {
            Attack.CLAW => "Claw",
            Attack.BITE => "Bite",
            Attack.STRUGGLE => "Struggle", 
            Attack.ACID_SPIT => "Acid Spit",
            Attack.GLITTER => "Glitter",
            Attack.DRAIN => "Drain",
            Attack.VINE => "Vine",
            _ => string.Empty,
        };
    }

    public static string GetDescription(this Attack attack)
    {
        return attack switch
        {
            Attack.CLAW => "...",
            Attack.BITE => "...",
            Attack.STRUGGLE => "...", 
            Attack.ACID_SPIT => "...",
            Attack.GLITTER => "...",
            Attack.DRAIN => "...",
            Attack.VINE => "...",
            _ => string.Empty,
        };
    }

    public static int GetDamageAmount(this Attack attack)
    {
        return attack switch
        {
            Attack.CLAW => 5,
            Attack.BITE => 12,
            Attack.STRUGGLE => 14, 
            Attack.ACID_SPIT => 18,
            Attack.GLITTER => 20,
            Attack.DRAIN => 10,
            Attack.VINE => 13,
            _ => 0,
        };
    }

    public static Sprite GetIcon(this Attack attack)
    {
        return attack switch
        {
            Attack.CLAW => null,
            Attack.BITE => null,
            Attack.STRUGGLE => null, 
            Attack.ACID_SPIT => null,
            Attack.GLITTER => null,
            Attack.DRAIN => null,
            Attack.VINE => null,
            _ => null,
        };
    }
}