using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    POISON_SHROOM,
    MEALBERRY,
    MEDICINAL_ROOT,
    STURDY_BRANCH
}

public static class ItemTypes
{
    // Getter for the name of attack
    public static string GetName(this Items item)
    {
        return item switch
        {
            Items.POISON_SHROOM => "Poison Shroom",
            Items.MEALBERRY => "Mealberry",
            Items.STURDY_BRANCH => "Sturdy Branch", 
            Items.MEDICINAL_ROOT => "Medicinal Root",
            _ => string.Empty,
        };
    }

    public static string GetDescription(this Items item)
    {
        return item switch
        {
            Items.POISON_SHROOM  => "Throw at an enemy: deals 5 damage and applies Poison.",
            Items.MEALBERRY      => "Eat to restore 15 HP.",
            Items.MEDICINAL_ROOT => "Removes all Poison afflictions from the player.",
            Items.STURDY_BRANCH  => "Consumable weapon. 15 damage (uses a turn) or 5 damage (instant). 3 uses.",
            _                    => string.Empty,
        };
    }

    public static Sprite GetIcon(this Items item)
    {
        return item switch
        {
            Items.POISON_SHROOM => Resources.Load<Sprite>("Sprites/poisenshroom"),
            Items.MEALBERRY => Resources.Load<Sprite>("Sprites/mealberry"),
            Items.STURDY_BRANCH => Resources.Load<Sprite>("Sprites/thornswall"),
            Items.MEDICINAL_ROOT => Resources.Load<Sprite>("Sprites/bush"),
            _ => null,
        };
    }    

    /// <summary>
    /// How many uses an item has before it is consumed.
    /// </summary>
    public static int GetMaxUses(this Items item)
    {
        return item switch
        {
            Items.STURDY_BRANCH => 3,
            _                   => 1,
        };
    }
}