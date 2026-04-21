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
    // Getter for the name of tile
    public static string GetName(this TileType tileType)
    {
        return tileType switch
        {
            TileType.ForestPure => "Forest (Pure)",
            TileType.ForestImpure => "Forest (Impure)",
            TileType.CorruptedForest => "Forest (Corrupted)",
            TileType.Lake => "Lake",
            TileType.Cottage => "Cottage",  //cabinTwo, cabinThree
            TileType.RitualCircle => "Ritual Circle",
            TileType.CorruptedRitualCircle => "Ritual Circle (Corrupted)",
            TileType.MagicCircle => "Magic Circle", 
            TileType.MagicCircleWard => "Magic Circle Ward", //magicCircleThree
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
        // return tileType switch
        // {
        //     TileType.ForestPure => "Forest (Pure)",
        //     TileType.ForestImpure => "Forest (Impure)",
        //     TileType.CorruptedForest => "Forest (Corrupted)",
        //     TileType.Lake => "Lake",
        //     TileType.Cottage => "Cottage",  //cabinTwo, cabinThree
        //     TileType.RitualCircle => "Ritual Circle",
        //     TileType.CorruptedRitualCircle => "Ritual Circle (Corrupted)",
        //     TileType.MagicCircle => "Magic Circle", 
        //     TileType.MagicCircleWard => "Magic Circle Ward", //magicCircleThree
        //     TileType.Anthill => "Anthill",
        //     TileType.BerryBush => "Berry Bushes",
        //     TileType.BerryBushPlus => "Berry Bushes +",
        //     TileType.MonsterDen => "Monster Den",
        //     _ => string.Empty,
        // };
        return "Description here.";
    }  

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