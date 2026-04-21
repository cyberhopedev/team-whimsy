using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum for holding the specifc types of tiles
/// </summary>
public enum TileType
{
    // Base Terrain
    ForestPure,
    ForestImpure,
    CorruptedForest,
    Lake,

    // Buildings
    Cottage,
    RitualCircle,
    CorruptedRitualCircle,
    MagicCircle,
    MagicCircleWard,
    Anthill,
    BerryBush,
    BerryBushPlus,

    // Special/etc.
    MonsterDen
}

public static class TileTypes
{
    // Getter for the name of attack
    // public static string GetName(this Items item)
    // {
    //     return item switch
    //     {
    //         Items.POISON_SHROOM => "Poison Shroom",
    //         Items.MEALBERRY => "Mealberry",
    //         Items.STURDY_BRANCH => "Sturdy Branch", 
    //         Items.MEDICINAL_ROOT => "Medicinal Root",
    //         _ => string.Empty,
    //     };
    // }

    // public static string GetDescription(this Items item)
    // {
    //     return item switch
    //     {
    //         Items.POISON_SHROOM  => "Throw at an enemy: deals 5 damage and applies Poison.",
    //         Items.MEALBERRY      => "Eat to restore 15 HP.",
    //         Items.MEDICINAL_ROOT => "Removes all Poison afflictions from the player.",
    //         Items.STURDY_BRANCH  => "Consumable weapon. 15 damage (uses a turn) or 5 damage (instant). 3 uses.",
    //         _                    => string.Empty,
    //     };
    // }

    public static Sprite GetIcon(this TileType tileType)
    {
        return tileType switch
        {
            TileType.ForestPure => Resources.Load<Sprite>("Sprites/forest"),
            TileType.ForestImpure => Resources.Load<Sprite>("Sprites/corruptForest"),
            TileType.CorruptedForest => Resources.Load<Sprite>("Sprites/corruptForest2"),
            TileType.Lake => Resources.Load<Sprite>("Sprites/lake"),
            TileType.Cottage => Resources.Load<Sprite>("Sprites/cabinOne"),  //cabinTwo, cabinThree
            TileType.RitualCircle => Resources.Load<Sprite>("Sprites/ritualCircle"),
            TileType.CorruptedRitualCircle => Resources.Load<Sprite>("Sprites/corruptRitualCircle"),
            TileType.MagicCircle => Resources.Load<Sprite>("Sprites/magicCircle"), 
            TileType.MagicCircleWard => Resources.Load<Sprite>("Sprites/magicCircleTwo"), //magicCircleThree
            TileType.Anthill => Resources.Load<Sprite>("Sprites/antHill"),
            TileType.BerryBush => Resources.Load<Sprite>("Sprites/berryBush1"),
            TileType.BerryBushPlus => Resources.Load<Sprite>("Sprites/berryBush2"),
            TileType.MonsterDen => Resources.Load<Sprite>("Sprites/monsterDen"),
            _ => null,
        };
    }    
}