using UnityEngine;

/// <summary>
/// A simple object representing the Tiles in the environment
/// </summary>
public class Tile : MonoBehaviour
{   
    // State of the current Tile
    public TileState State { get; private set; }

    public Vector2Int GridPosition {get; private set;}

    /// <summary>
    /// Initializes the Tile's position, needed in TileManager
    /// </summary>
    /// <param name="pos">The currenrt position of the tile</param>
    public void Init(Vector2Int pos)
    {
        GridPosition = pos;
    }

    /// <summary>
    /// Sets/updates the state of the tile, if the tile is corrupted,
    /// invoke the OnTileCorrupted Action
    /// </summary>
    /// <param name="newState"></param>
    public void SetState(TileState newState)
    {
        State = newState;
        if(newState == TileState.CORRUPTED)
        {
            EventBus.OnTileCorrupted?.Invoke(this);
        }
    }

    /// <summary>
    /// Checks if the tile can be built on based on if it empty
    /// or purified
    /// </summary>
    /// <returns>True if the tile can be built on, false if otherwise</returns>
    public bool IsBuildable()
    {
        if(State == TileState.EMPTY || State == TileState.PURIFIED)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}