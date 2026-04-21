using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Handles logic for the corruption spread, interval of spread, and the
/// general timer
/// </summary>
public class CorruptionManager : MonoBehaviour
{
    // Instance for the corruption manager
    public static CorruptionManager Instance;

    [Header("Timing")]
    public float spreadInterval = 5f;
    private float timer;

    [Header("Chances")]
    public float spreadChance = 0.3f;   // H%
    public float impurityChance = 0.25f; // G%

    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Every frame update the timer is updated, spread if timer is >
    /// than spread interval, then reset timer to 0 to loop spread effect
    /// </summary>
    public void ProcessTick()
    {
        ProcessImpurity();
        Spread();
    }

    /// <summary>
    /// Handles impurity within tiles
    /// </summary>
    void ProcessImpurity()
    {
        // Get all of the pure tiles from the game, creating a list of them that need to be increased
        var pureTiles = TileManager.Instance.GetPureTiles();
        List<Tile> toIncrease = new();
        foreach(var tile in pureTiles)
        {
            var neighbors = TileManager.Instance.GetNeighbors(tile.GridPosition);
            foreach(var n in neighbors)
            {
                if(n.Type == TileType.ForestImpure || n.Type == TileType.CorruptedForest)
                {
                    if(Random.value < impurityChance)
                    {
                        toIncrease.Add(tile);
                        break;
                    }
                }
            }
        }

        // Add impurity value to all of the tiles that need to be increased
        foreach(var t in toIncrease)
        {
            t.AddImpurity(10);
        }
    }

    /// <summary>
    /// Spreads the actual corruption to all of the tiles
    /// within the game
    /// </summary>
    void Spread()
    {
        // Get the corrupted tiles and create a new list to hold tiles that need to be corrupted
        var corruptedTiles = TileManager.Instance.GetCorruptedTiles();
        List<Tile> toCorrupt = new();

        // For every tile in the existing corrupted tiles, check it's neighbors and apply spreading rules
        foreach(var tile in corruptedTiles)
        {
            var neighbors = TileManager.Instance.GetNeighbors(tile.GridPosition);
            foreach(var n in neighbors)
            {
                // If tile is locked or already corrupted, continue to next neighbor
                if(n.IsLocked)
                {
                    continue;
                }
                if (n.Type == TileType.CorruptedForest)
                {
                    continue;
                }

                // If it is a ritual circle, make it a corrupted ritual circle
                if(n.Type == TileType.RitualCircle)
                {
                    n.Type = TileType.CorruptedRitualCircle;
                    EventBus.OnTileChanged?.Invoke(n);
                    continue;
                }
                // Handles the H% chance to convert any adjacent tile
                if(Random.value < spreadChance)
                {
                    toCorrupt.Add(n);
                }
            }
        }

        // Fully corrupt the tiles needed to corrupt
        foreach(var tile in toCorrupt)
        {
            tile.Type = TileType.CorruptedForest;
            tile.AddImpurity(100);
            EventBus.OnTileChanged?.Invoke(tile);
        }
    }

    /// <summary>
    /// Called every 4th ritual placement
    /// </summary>
    public void SpawnRandomCorruption()
    {
        var candidates = TileManager.Instance.GetPureTiles();

        if (candidates.Count == 0) return;

        Tile randomTile = candidates[Random.Range(0, candidates.Count)];

        randomTile.Type = TileType.CorruptedForest;
        randomTile.AddImpurity(100);
        EventBus.OnTileChanged?.Invoke(randomTile);
    }
}