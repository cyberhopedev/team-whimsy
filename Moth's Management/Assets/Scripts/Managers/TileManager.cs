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
    public int width = 21;
    public int height = 21;
    public GameObject tilePrefab;

    // Core storage of the tiles within the environment
    private Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();

    // Lists to hold the varying types of tiles
    private List<Tile> corruptedTiles = new List<Tile>();
     private List<Tile> pureTiles = new List<Tile>();
    private List<Tile> impureTiles = new List<Tile>();
    private List<Tile> ritualTiles = new List<Tile>();
    private List<Tile> corruptedRitualTiles = new List<Tile>();

    // Amount of rituals the player has completed
    private int ritualCount = 0;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        EventBus.OnTileCorrupted += UpdateTileCaches;
        EventBus.OnBuildingPlaced += OnBuildingPlaced;
    }

    void OnDisable()
    {
        EventBus.OnTileCorrupted -= UpdateTileCaches;
        EventBus.OnBuildingPlaced -= OnBuildingPlaced;
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
                tile.Type = TileType.ForestPure;

                tiles.Add(pos, tile);
                pureTiles.Add(tile);
            }
        }
    }

    /// <summary>
    /// Initialize the safe space cottage area on the map
    /// </summary>
    void InitializeCottage()
    {
        Vector2Int center = new(width / 2, height / 2);
        Tile centerTile = GetTile(center);
        centerTile.Type = TileType.Cottage;

        // Purify starting radius (initial ritual effect)
        foreach(var t in GetTilesInRange(center, 2))
        {
            t.Impurity = 0;
        }
    }

    /// <summary>
    /// Helper to get a tile at a specific position
    /// </summary>
    /// <param name="pos">The position of the tile</param>
    /// <returns></returns>
    public Tile GetTile(Vector2Int pos)
    {
        return tiles.TryGetValue(pos, out var tile) ? tile : null;
    }

    /// <summary>
    /// Gets the neighboring tiles of the current Tile position
    /// </summary>
    /// <param name="pos">The position of the current Tile</param>
    /// <returns>A list of neighboring tiles</returns>
    public List<Tile> GetNeighbors(Vector2Int pos)
    {
        // Initialize the directions
        Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        // Get the list of neigbors and return it
        List<Tile> neighbors = new List<Tile>();
        foreach(var dir in directions)
        {
            Tile t = GetTile(pos + dir);
            if(t != null)
            {
                neighbors.Add(t);
            }
        }
        return neighbors;
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
    /// Gets the tile that is currently being hovered over based on
    /// mouse position
    /// </summary>
    /// <returns>Position of the tile being hovered over</returns>
    public Tile GetTileFromMouse()
    {
        Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int pos = new(Mathf.RoundToInt(world.x), Mathf.RoundToInt(world.y));
        return GetTile(pos);
    }

    /// <summary>
    /// Helper to update all of the saved tiles
    /// </summary>
    /// <param name="tile">The tile being updated</param>
    void UpdateTileCaches(Tile tile)
    {
        // Remove from all
        pureTiles.Remove(tile);
        impureTiles.Remove(tile);
        corruptedTiles.Remove(tile);
        ritualTiles.Remove(tile);
        corruptedRitualTiles.Remove(tile);

        // Reassign
        switch (tile.Type)
        {
            case TileType.ForestPure:
                pureTiles.Add(tile);
                break;

            case TileType.ForestImpure:
                impureTiles.Add(tile);
                break;

            case TileType.CorruptedForest:
                corruptedTiles.Add(tile);
                break;

            case TileType.RitualCircle:
                ritualTiles.Add(tile);
                break;

            case TileType.CorruptedRitualCircle:
                corruptedRitualTiles.Add(tile);
                break;
        }
    }

    void OnBuildingPlaced(Building building)
    {
        Tile tile = building.tile;

        if (tile.Type == TileType.RitualCircle)
        {
            ritualCount++;

            // Every 4th ritual → spawn corruption
            if (ritualCount % 4 == 0)
            {
                CorruptionManager.Instance.SpawnRandomCorruption();
            }
        }
    }

    /// <summary>
    /// Gets all of the pure tiles
    /// </summary>
    /// <returns>A list containing all of the pure tiles</returns>
    public List<Tile> GetPureTiles()
    {
        return pureTiles;
    }
    /// <summary>
    /// Gets all of the impure tiles
    /// </summary>
    /// <returns>A list containing all of the impure tiles</returns>
    public List<Tile> GetImpureTiles()
    {
        return impureTiles;
    }
    /// <summary>
    /// Gets all of the corrupted tiles
    /// </summary>
    /// <returns>A list containing all of the corrupted tiles</returns>
    public List<Tile> GetCorruptedTiles()
    {
        return corruptedTiles;
    }
    /// <summary>
    /// Gets all of the ritual tiles
    /// </summary>
    /// <returns>A list containing all of the ritual tiles</returns>
    public List<Tile> GetRitualTiles()
    {
        return ritualTiles;
    }

    /// <summary>
    /// Checks if the center is corrupted
    /// </summary>
    /// <returns>True if the center is corrupted, false if otherwise</returns>
    public bool IsCenterCorrupted()
    {
        Tile center = GetTile(new Vector2Int(width / 2, height / 2));
        return center.Type == TileType.CorruptedForest;
    }
    /// <summary>
    /// Check if the player has lost
    /// </summary>
    /// <returns>True if the player lost due to center being corrupted OR 3 corrupted ritual circles exists, false if otherwise</returns>
    public bool HasLost()
    {
        return IsCenterCorrupted() || corruptedRitualTiles.Count >= 3;
    }
    /// <summary>
    /// Check if the player has won
    /// </summary>
    /// <param name="totalRitualsRequired">The number of rituals needed to be completed in order to win</param>
    /// <returns>True if the player has completed the # of rituals needed, false if otherwise</returns>
    public bool HasWon(int totalRitualsRequired)
    {
        return ritualTiles.Count >= totalRitualsRequired && corruptedTiles.Count == 0;
    }

}