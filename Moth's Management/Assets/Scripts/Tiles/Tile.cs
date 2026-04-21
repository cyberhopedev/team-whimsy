using UnityEngine;

/// <summary>
/// A simple object representing the Tiles in the environment
/// </summary>
public class Tile : MonoBehaviour
{   
    // Position of the current tile
    public Vector2Int GridPosition;
    // Type of the tile
    public TileType Type;

    // Current impurity and max impurity of the tile
    public int Impurity = 0;
    public int MaxImpurity = 100;

    // Represent if a tile is locked or not
    public bool IsLocked = false;

    // Means a ritual site should be placed here
    public bool IsRitualSite = false;

    /// <summary>
    /// Initialize tile
    /// </summary>
    public void Init(Vector2Int pos)
    {
        GridPosition = pos;
        Type = TileType.ForestPure;
        Impurity = 0;
        IsLocked = false;
    }

    /// <summary>
    /// Adds impurity and converts tile if needed
    /// </summary>
    public void AddImpurity(int amount)
    {
        if (IsLocked) return;

        Impurity += amount;

        if (Impurity >= MaxImpurity)
        {
            ConvertToImpure();
        }

        EventBus.OnTileChanged?.Invoke(this);
    }

    /// <summary>
    /// Converts tile when impurity maxes out
    /// </summary>
    void ConvertToImpure()
    {
        if (Type == TileType.ForestPure)
        {
            Type = TileType.ForestImpure;
        }
    }

    /// <summary>
    /// Purifies tile
    /// </summary>
    public void Purify()
    {
        Impurity = 0;

        if (Type == TileType.ForestImpure || Type == TileType.CorruptedForest)
        {
            Type = TileType.ForestPure;
        }

        EventBus.OnTileChanged?.Invoke(this);
    }

    /// <summary>
    /// Checks if the current tile is corrupted
    /// </summary>
    /// <returns>True if the tile is corrupted, false if otherwise</returns>
    public bool IsCorrupted()
    {
        if(Type == TileType.CorruptedForest || Type == TileType.CorruptedRitualCircle)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// Checks if the tile can be built on based on if it empty
    /// or purified
    /// </summary>
    /// <returns>True if the tile can be built on, false if otherwise</returns>
    public bool IsBuildable()
    {
        if(Type == TileType.ForestPure && !IsLocked)
        {
            return true;
        }
        return false;
    }

    // Public getter for tile type
    public TileType GetTileType()
    {
        return Type;
    }

    // Allows sprite to be set from another class
    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void OnHoverEnter()
    {
        Debug.Log($"Hovering over tile at {GridPosition}");
    }

    public void OnHoverExit()
    {
        // something
    }

    public void OnSelect()
    {
        Debug.Log($"Selected tile at {GridPosition} | Type: {Type}");
        EventBus.OnTileSelected?.Invoke(this);
    }

    public void OnDeselect()
    {
        // something
    }
}