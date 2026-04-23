using UnityEngine;
using System.Collections.Generic;

public class MagicCircle : Building
{
    public int purifyRadius = 1;

    public override void Init(Tile tile)
    {
        base.Init(tile);
        PurifyArea();
    }

    void PurifyArea()
    {
        Debug.Log("purifying");
        var tiles = TileManager.Instance.GetTilesInRange(tile.GridPosition, purifyRadius);
        foreach (var t in tiles)
        {
            t.Purify();
            if (t.GetTileType() == TileType.CorruptedForest || t.GetTileType() == TileType.ForestImpure)
            {
                t.SetSprite(TileTypes.GetIcon(TileType.ForestPure));
            }
            else if (t.GetTileType() == TileType.CorruptedRitualCircle)
            {
                t.SetSprite(TileTypes.GetIcon(TileType.RitualCircle));
            } 
            else if (t.GetTileType() != TileType.ForestPure)
            {
                Debug.Log("We haven't considered " + TileTypes.GetName(t.GetTileType()) + "purifying sprite change");
            }
        }
    }
}