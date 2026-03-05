using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    POISON_SHROOM,
    MEALBERRY,
    STURDY_BRANCH,
    WEAPON,
    OVERGROWN_LOOT,
    MEDICINAL_ROOT
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
            Items.WEAPON => "Weapon",
            Items.OVERGROWN_LOOT => "Loot",
            Items.MEDICINAL_ROOT => "Medicinal Root",
            _ => string.Empty,
        };
    }

    public static string GetDescription(this Items item)
    {
        return item switch
        {
            Items.POISON_SHROOM => "...",
            Items.MEALBERRY => "...",
            Items.STURDY_BRANCH => "...", 
            Items.WEAPON => "...",
            Items.OVERGROWN_LOOT => "...",
            Items.MEDICINAL_ROOT => "...",
            _ => string.Empty,
        };
    }
}