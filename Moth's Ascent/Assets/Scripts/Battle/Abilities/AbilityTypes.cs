using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ability
{
    RAISE_ARMS,
    EXOSKELETON,
    FILTER_FLUFF
}

public static class AbilityTypes
{
    // Getter for the name of attack
    public static string GetName(this Ability ability)
    {
        return ability switch
        {
            Ability.RAISE_ARMS => "- Raise Arms",
            Ability.EXOSKELETON => "- Exoskeleton",
            Ability.FILTER_FLUFF => "- Filter Fluff",
            _ => string.Empty,
        };
    }

    public static string GetDescription(this Ability ability)
    {
        return ability switch
        {
            Ability.RAISE_ARMS => "...",
            Ability.EXOSKELETON => "...",
            Ability.FILTER_FLUFF => "...",
            _ => string.Empty,
        };
    }

    public static Sprite GetIcon(this Ability ability)
    {
        return ability switch
        {
            Ability.RAISE_ARMS => null,
            Ability.EXOSKELETON => null,
            Ability.FILTER_FLUFF => null,
            _ => null,
        };
    }
}