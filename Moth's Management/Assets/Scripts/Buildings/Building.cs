using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents a building object that can be placed by the player.
/// Buildings can only be placed on non-corrupted tiles
/// </summary>
public abstract class Building : MonoBehaviour
{
    // The current tile the building is on
    public Tile tile;
    /// <summary>
    /// Initializes the building
    /// </summary>
    /// <param name="tile">The tile the building is on, if any</param>
    public virtual void Init(Tile tile)
    {
        this.tile = tile;
        EventBus.OnBuildingPlaced?.Invoke(this);
    }
}