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

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        EventBus.OnTileCorrupted += OnTileCorruptionChanged;
    }

    void OnDisable()
    {
        EventBus.OnTileCorrupted -= OnTileCorruptionChanged;
    }

    /// <summary>
    /// As soon as game starts, generate the grid
    /// </summary>
    void Start()
    {
        GenerateGrid();
    }

    /// <summary>
    /// Generates the grid for the game based on the given width and height
    /// </summary>
    void GenerateGrid()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);

                GameObject obj = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                Tile tile = obj.GetComponent<Tile>();

                tile.Init(pos);

                tiles.Add(pos, tile);

                // Initialize cache
                corruptionLevels[0].Add(tile);
            }
        }
    }

    /// <summary>
    /// Gets the neighboring tiles of the current Tile position
    /// </summary>
    /// <param name="pos">The position of the current Tile</param>
    /// <returns>A list of neighboring tiles</returns>
    public List<Tile> GetNeighbors(Vector2Int pos)
    {
        List<Tile> neighbors = new List<Tile>();

        Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        foreach(var dir in directions)
        {
            // Tile t = GetTile(pos + dir);
            // if(t != null)
            {
                // neighbors.Add(t);
            }
        }

        return neighbors;
    }

    /// <summary>
    /// Gets all of the corrupted tiles
    /// </summary>
    /// <returns>A list containing all of the corrupted tils</returns>
    public List<Tile> GetCorruptedTiles()
    {
        return corruptedTiles;
    }

    /// <summary>
    /// Gets the levels of corrupted tiles, otherwise just returns a 
    /// new list of tiles
    /// </summary>
    /// <param name="level">The level of corruption</param>
    /// <returns>A list of corruption levels associated with the tiles</returns>
    public List<Tile> GetTilesWithCorruption(int level)
    {
        if(corruptionLevels.ContainsKey(level))
        {
            return corruptionLevels[level];
        }

        return new List<Tile>();
    }

    /// <summary>
    /// Gets all of the tiles "in range" from a centering position
    /// as a list
    /// </summary>
    /// <param name="center">The centering position</param>
    /// <param name="range">The distance from the centering tile</param>
    /// <returns></returns>
    public List<Tile> GetTilesInRange(Vector2Int center, int range)
    {
        List<Tile> result = new List<Tile>();
        foreach (var kvp in tiles)
        {
            if(Vector2Int.Distance(kvp.Key, center) <= range)
            {
                result.Add(kvp.Value);
            }
        }

        return result;
    }

    /// <summary>
    /// Gets all of the tiles that the player can build on
    /// </summary>
    /// <returns>A list of tiles that can be built on</returns>
    public List<Tile> GetBuildableTiles()
    {
        return tiles.Values.Where(t => t.IsBuildable()).ToList();
    }


    /// <summary>
    /// Updates the tile storage after a change occurs relating
    /// to corruption
    /// </summary>
    /// <param name="tile">The current changing tile</param>
    void OnTileCorruptionChanged(Tile tile)
    {
        // Remove from all lists first
        foreach(var list in corruptionLevels.Values)
        {
            list.Remove(tile);
        }
        corruptedTiles.Remove(tile);

        // Add to correct list
        int level = tile.CorruptionLevel;
        if (corruptionLevels.ContainsKey(level))
        {
            corruptionLevels[level].Add(tile);
        }
        if (level > 0)
        {
            corruptedTiles.Add(tile);
        }
    }
}