using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Magic cirlce upgrade that provides the permanent ward
/// </summary>
public class MagicCirclePlusA : Building
{
    // Radius of tiles that are permanently predicted
    public int wardRadius = 1;

    /// <summary>
    /// Initializes the upgraded magic cirlce
    /// </summary>
    /// <param name="tile">The tile to be initialized</param>
    public override void Init(Tile tile)
    {
        base.Init(tile);
        ApplyWard();
    }

    /// <summary>
    /// Applies the permanent purification to the tiles in range of the
    /// MagicCircle+ A version
    /// </summary>
    void ApplyWard()
    {
        var tiles = TileManager.Instance.GetTilesInRange(tile.GridPosition, wardRadius);
        foreach(var t in tiles)
        {
            t.Purify();
            t.IsLocked = true;
        }
    }
}