using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages and intializes the tiles within the grid/environment for the gameplay
/// </summary>
public class TileManager : MonoBehaviour
{
    // Singleton instance of the TileManager
    public static TileManager Instance;

    // Grid settings to be set within Unity, also be sure to make a Prefab for the Tile
    [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    public GameObject tilePrefab;

    // Core storage of the tiles within the environment
    private Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();

    // List of the corrupted tiles
    private List<Tile> corruptedTiles = new List<Tile>();
    // Dictionary of the corruption tiles, holding lists of tiles with different levels of corruption (0, 1, 2, 3 -> 0 means pure)
    private Dictionary<int, List<Tile>> corruptionLevels = new Dictionary<int, List<Tile>>()
    {
        {0, new List<Tile>()},
        {1, new List<Tile>()},
        {2, new List<Tile>()},
        {3, new List<Tile>()}
    };

}