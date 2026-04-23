using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Magic cirlce upgrade that provides the upgraded purification radius
/// </summary>
public class MagicCirclePlusB : Building
{
    // Radius of the purification
    public int purifyRadius = 3;

    /// <summary>
    /// Initializes the upgraded magic circle
    /// </summary>
    /// <param name="tile">The tile to be initialized</param>
    public override void Init(Tile tile)
    {
        base.Init(tile);
        PurifyArea();
    }

    /// <summary>
    /// Applies the larger radius of purification to the tiles in range of the
    /// MagicCircle+ B version
    /// </summary>
    void PurifyArea()
    {
        var tiles = TileManager.Instance.GetTilesInRange(tile.GridPosition, purifyRadius);
        foreach(var t in tiles)
        {
            t.Purify();
        }
    }
}