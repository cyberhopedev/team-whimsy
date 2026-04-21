using UnityEngine;

/// <summary>
/// Handles the logic for placing a building on the game board
/// </summary>
public class BuildingManager : MonoBehaviour
{
    // Instance of the BuildingManager
    public static BuildingManager Instance { get; private set; }
    private GameObject selectedPrefab;
    // public GameObject ritualPrefab;
    public GameObject antHillPrefab;
    public GameObject berryPrefab;
    public GameObject cottagePrefab;
    public GameObject magicCirclePrefab;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Places the building prefab based on if the tile type is able
    /// to be built on
    /// </summary>
    /// <param name="tile">The current tile attempting to be built on</param>
    /// <param name="prefab">The Building's prefab</param>
    public void PlaceBuilding(Tile tile, GameObject prefab)
    {
        if (tile == null || !tile.IsBuildable()) {
            return;
        }

        // Create building
        GameObject obj = Instantiate(prefab, new Vector3(tile.GridPosition.x, tile.GridPosition.y, 0), Quaternion.identity);
        Building building = obj.GetComponent<Building>();
        building.Init(tile);

        // Update the tile type based on what was placed
        tile.Type = GetTileTypeForPrefab(prefab);
        EventBus.OnTileChanged?.Invoke(tile);
    }

    TileType GetTileTypeForPrefab(GameObject prefab)
    {
        // if (prefab == ritualPrefab) return TileType.RitualCircle;
        if (prefab == antHillPrefab) return TileType.Anthill;
        if (prefab == berryPrefab) return TileType.BerryBush;
        if (prefab == cottagePrefab) return TileType.Cottage;
        if (prefab == magicCirclePrefab) return TileType.MagicCircle;
        return TileType.ForestPure;
    }

    // Called by UI buttons
    public void SelectBuilding(string buildingName)
    {
        selectedPrefab = buildingName switch
        {
            // "ritual" => ritualPrefab,
            "Anthill" => antHillPrefab,
            "BerryBush" => berryPrefab,
            "MagicCircle" => magicCirclePrefab,
            _ => null
        };
    }

    public void TryPlaceSelected(Tile tile)
    {
        if (selectedPrefab == null) return;
        PlaceBuilding(tile, selectedPrefab);
    }
}