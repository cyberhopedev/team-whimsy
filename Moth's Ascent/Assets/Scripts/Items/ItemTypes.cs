using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    POISON_SHROOM,
    MEALBERRY,
    STURDY_BRANCH,
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
            Items.MEDICINAL_ROOT => "Medicinal Root",
            _ => string.Empty,
        };
    }

    public static string GetDescription(this Items item)
    {
        return item switch
        {
            Items.POISON_SHROOM => "5 damage to target and applies poison",
            Items.MEALBERRY => "Heals player by 15 points",
            Items.STURDY_BRANCH => "Removes any poison you are suffering from", 
            Items.MEDICINAL_ROOT => "Consumable weapon. 15 damage but only 3 uses",
            _ => string.Empty,
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
}