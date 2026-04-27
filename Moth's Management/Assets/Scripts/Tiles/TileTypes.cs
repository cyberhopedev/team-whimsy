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
    Cottage2,
    Cottage3,
    RitualCircle,
    CorruptedRitualCircle,
    MagicCircle,
    MagicCircleA,
    MagicCircleB,
    Anthill,
    BerryBush,
    BerryBushPlus,

    // Special/etc.
    MonsterDen
}

public static class TileTypes
{
    // Getter for the name of tile
    public static string GetName(this TileType tileType)
    {
        return tileType switch
        {
            TileType.ForestPure => "Forest (Pure)",
            TileType.ForestImpure => "Forest (Impure)",
            TileType.CorruptedForest => "Forest (Corrupted)",
            TileType.Lake => "Lake",
            TileType.Cottage => "Cottage", 
            TileType.Cottage2 => "Cottage", 
            TileType.Cottage3 => "Cottage", 
            TileType.RitualCircle => "Ritual Circle",
            TileType.CorruptedRitualCircle => "Ritual Circle (Corrupted)",
            TileType.MagicCircle => "Magic Circle", 
            TileType.MagicCircleA => "Magic Circle +",
            TileType.MagicCircleB => "Magic Circle +",
            TileType.Anthill => "Anthill",
            TileType.BerryBush => "Berry Bushes",
            TileType.BerryBushPlus => "Berry Bushes +",
            TileType.MonsterDen => "Monster Den",
            _ => string.Empty,
        };
    }  

    // Getter for the tile description
    public static string GetDescription(this TileType tileType)
    {
        return tileType switch
        {
            TileType.Lake => "Cannot be corrupted or purified.\n\nPlace berry bushes adjacent to increase berry production.",
            TileType.RitualCircle => "Build all the ritual circles on the map to stop the corruption!",
            TileType.CorruptedRitualCircle => "Purify this ritual circle!\n\nWarning: 3 or more corrupted ritual circles at a time will cause irreparable damage.",
            TileType.MonsterDen => "Produces Monster with 10% chance each time step",
            _ => string.Empty,
        };
    }  

    public static string GetTierLvl(this TileType tileType)
    {
        return tileType switch
        {
            TileType.ForestPure => "Tier 1",
            TileType.Cottage => "Tier 1",  
            TileType.Cottage2 => "Tier 2",  
            TileType.Cottage3 => "Tier 3", 
            TileType.MagicCircle => "Tier 1", 
            TileType.MagicCircleA => "Tier 2", 
            TileType.MagicCircleB => "Tier 3",
            TileType.BerryBush => "Tier 1",
            TileType.BerryBushPlus => "Tier 2",
            _ => string.Empty,
        };
    } 

    public static Sprite GetIcon(this TileType tileType)
    {
        return tileType switch
        {
            TileType.ForestPure => Resources.Load<Sprite>("Sprites/forest"),
            TileType.ForestImpure => Resources.Load<Sprite>("Sprites/corruptForest"),
            TileType.CorruptedForest => Resources.Load<Sprite>("Sprites/corruptForest2"),
            TileType.Lake => Resources.Load<Sprite>("Sprites/lake"),
            TileType.Cottage => Resources.Load<Sprite>("Sprites/cabinOne"),  
            TileType.Cottage2 => Resources.Load<Sprite>("Sprites/cabinTwo"),  
            TileType.Cottage3 => Resources.Load<Sprite>("Sprites/cabinThree"),  
            TileType.RitualCircle => Resources.Load<Sprite>("Sprites/ritualCircle"),
            TileType.CorruptedRitualCircle => Resources.Load<Sprite>("Sprites/corruptRitualCircle"),
            TileType.MagicCircle => Resources.Load<Sprite>("Sprites/magicCircle"), 
            TileType.MagicCircleA => Resources.Load<Sprite>("Sprites/magicCircleTwo"),
            TileType.MagicCircleB => Resources.Load<Sprite>("Sprites/magicCircleThree"), 
            TileType.Anthill => Resources.Load<Sprite>("Sprites/antHill"),
            TileType.BerryBush => Resources.Load<Sprite>("Sprites/berryBush1"),
            TileType.BerryBushPlus => Resources.Load<Sprite>("Sprites/berryBush2"),
            TileType.MonsterDen => Resources.Load<Sprite>("Sprites/monsterDen"),
            _ => null,
        };
    }    
}