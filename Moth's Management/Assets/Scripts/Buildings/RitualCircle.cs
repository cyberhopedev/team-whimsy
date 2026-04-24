using UnityEngine;

/// <summary>
/// Building part of the win condition that can only be placed in specific locations,
/// on every 4th placement a CorruptedForestTile is spawned on an ForestImpure tile
/// in a random location
/// </summary>
public class RitualCircle : Building
{
    public override void Init(Tile tile)
    {
        base.Init(tile);
        // Set the hardcoded locations for where the ritual circles must be placed
    }

    public void Corrupt()
    {
        // Corrupts a random impure forest tile once this RitualCircle tile is pplaced
    }
}