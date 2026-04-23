using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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
     private List<Tile> pureTiles = new List<Tile>();
    private List<Tile> impureTiles = new List<Tile>();
    private List<Tile> ritualTiles = new List<Tile>();
    private List<Tile> corruptedTiles = new List<Tile>();
    private List<Tile> corruptedRitualTiles = new List<Tile>();

    // Amount of rituals the player has completed
    private int ritualCount = 0;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        EventBus.OnTileChanged += UpdateTileCaches;
        EventBus.OnBuildingPlaced += OnBuildingPlaced;
    }

    void OnDisable()
    {
        EventBus.OnTileChanged -= UpdateTileCaches;
        EventBus.OnBuildingPlaced -= OnBuildingPlaced;
    }

    /// <summary>
    /// As soon as game starts, generate the grid and place the cottage
    /// </summary>
    void Start()
    {
        GenerateGrid();
        // InitializeCottage();
        LoadMapFromFile("map01");
        FitCamera();
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
                tile.SetSprite(TileTypes.GetIcon(TileType.ForestPure));

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

        // Purify starting radius (initial ritual effect)
        foreach(var t in GetTilesInRange(center, 2))
        {
            t.Purify();
        }

        Tile centerTile = GetTile(center);
        centerTile.Type = TileType.Cottage;
        EventBus.OnTileChanged?.Invoke(centerTile);
        BuildingManager.Instance.PlaceBuilding(centerTile, BuildingManager.Instance.cottagePrefab);
    }

    // Scale canvas for different resolutions
    void InitCanvas()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();

        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(2560, 1440);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 1;
    }

    // This method makes sure the camera will show all tiles
    void FitCamera()
    {
        Camera cam = Camera.main;

        float verticalSize = height / 2f;
        float horizontalSize = (width / 2f) / cam.aspect;
        cam.orthographicSize = Mathf.Max(verticalSize, horizontalSize) + 0.5f;

        // Calculate how wide the full screen is in world units
        float screenWorldWidth = cam.orthographicSize * 2f * cam.aspect;

        // .25 is to consider the space the UI takes up
        float uiWidthFraction = 0.25f;
        float offset = (screenWorldWidth * uiWidthFraction) / 2f;

        // Center on the grid, then shift left to account for UI panel
        float centerX = (width - 1) / 2f + offset;
        float centerY = (height - 1) / 2f;

        cam.transform.position = new Vector3(centerX, centerY, -10f);
    }

    /// <summary>
    /// Loads a map layout from a text file from Resources/MapFiles/
    /// </summary>
    void LoadMapFromFile(string mapName)
    {
        TextAsset mapFile = Resources.Load<TextAsset>($"MapFiles/{mapName}");

        // Split into rows and reverse so row 0 is the bottom of the map
        string[] rows = mapFile.text.Split('\n');
        System.Array.Reverse(rows);

        for (int y = 0; y < rows.Length; y++)
        {
            string row = rows[y].Trim();
            for (int x = 0; x < row.Length; x++)
            {
                Tile tile = GetTile(new Vector2Int(x, y));
                if (tile == null) continue;

                char symbol = row[x];
                ApplySymbolToTile(symbol, tile, new Vector2Int(x, y));
            }
        }
    }

    /// <summary>
    /// Maps a character symbol to a tile type and applies it
    /// </summary>
    void ApplySymbolToTile(char symbol, Tile tile, Vector2Int pos)
    {
        switch (symbol)
        {
            case 'X':
                tile.Type = TileType.ForestImpure;
                break;
            case '*':
                tile.Type = TileType.ForestPure;
                break;
            case '~':
                tile.Type = TileType.Lake;
                break;
            case 'O':
                tile.Type = TileType.ForestImpure;
                tile.IsRitualSite = true; // means a ritual site should go here later
                break;
            case 'H':
                tile.Type = TileType.Cottage;
                // Spawn cottage building directly, bypassing IsBuildable check
                GameObject obj = Instantiate(BuildingManager.Instance.cottagePrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                Building building = obj.GetComponent<Building>();
                building.Init(tile);
                break;
        }

        tile.SetSprite(TileTypes.GetIcon(tile.Type));
        EventBus.OnTileChanged?.Invoke(tile);
    }

    /// <summary>
    /// Helper to get a tile at a specific position
    /// </summary>
    public Tile GetTile(Vector2Int pos)
    {
        return tiles.TryGetValue(pos, out var tile) ? tile : null;
    }

    /// <summary>
    /// Gets the neighboring tiles of the current Tile position
    /// </summary>
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
    public List<Tile> GetTilesInRange(Vector2Int center, int range)
    {
        List<Tile> tilesInRange = new List<Tile>();
        foreach(var t in tiles)
        {
            if(Vector2Int.Distance(t.Key, center) <= range)
            {
                tilesInRange.Add(t.Value);
            }
        }

        return tilesInRange;
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
        switch(tile.Type)
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

        // Update tile sprite
        tile.SetSprite(TileTypes.GetIcon(tile.Type));
    }
    /// <summary>
    /// Checks for if the building placed is a ritual circle, if it is
    /// ritual count is increased and delegates logic for ritual circle
    /// placement in CorruptionManager
    /// </summary>
    /// <param name="building">The bilding currently being placed by the player</param>
    void OnBuildingPlaced(Building building)
    {
        if(building.tile.Type == TileType.RitualCircle)
        {
            ritualCount++;
            if(ritualCount % 4 == 0)
            {
                // Delegate to CorruptionManager
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