using UnityEngine;

/// <summary>
/// Handles the logic for placing a building on the game board
/// </summary>
public class BuildingManager : MonoBehaviour
{
    // Instance of the BuildingManager
    public static BuildingManager Instance { get; private set; }
    // private GameObject selectedPrefab;
    public GameObject ritualPrefab;
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
            Debug.Log("tile is null!!!!!!!!!!!!!!!!");
            return;
        }
        Debug.Log("inside place building");

        // Create building
        GameObject obj = Instantiate(prefab, new Vector3(tile.GridPosition.x, tile.GridPosition.y, 0), Quaternion.identity);
        Building building = obj.GetComponent<Building>();
        building.Init(tile);

        // Update the tile type based on what was placed

        // Only set type if Init didn't already change it
        if (tile.Type == TileType.ForestPure)
        {
            tile.Type = GetTileTypeForPrefab(prefab);
        }
        tile.SetSprite(TileTypes.GetIcon(tile.GetTileType()));
        EventBus.OnTileChanged?.Invoke(tile);
    }

    TileType GetTileTypeForPrefab(GameObject prefab)
    {
        if (prefab == ritualPrefab) return TileType.RitualCircle;
        if (prefab == antHillPrefab) return TileType.Anthill;
        if (prefab == berryPrefab) return TileType.BerryBush;
        if (prefab == cottagePrefab) return TileType.Cottage;
        if (prefab == magicCirclePrefab) return TileType.MagicCircle;
        return TileType.ForestPure;
    }

    public GameObject GetPrefabForTileType(TileType tile)
    {
        if (tile == TileType.RitualCircle) return ritualPrefab;
        if (tile == TileType.Anthill) return antHillPrefab;
        if (tile == TileType.BerryBush) return berryPrefab;
        if (tile == TileType.Cottage) return cottagePrefab;
        if (tile == TileType.MagicCircle) return magicCirclePrefab;
        return null;
    }

    // Called by UI buttons
    public void SelectBuilding(string buildingName, GameObject selectedPrefab)
    {
        selectedPrefab = buildingName switch
        {
            "ritual" => ritualPrefab,
            "Anthill" => antHillPrefab,
            "BerryBush" => berryPrefab,
            "MagicCircle" => magicCirclePrefab,
            _ => null
        };
    }

    public void TryPlaceSelected(Tile tile, GameObject selectedPrefab)
    {
        if (selectedPrefab == null) return;
        // Make sure you can afford it
        BuildingData costInfo = selectedPrefab.GetComponent<Building>().GetBuildingData();
        // If it's a ritual circle, calculate dynamic cost
        if (selectedPrefab == ritualPrefab)
        {
            var (magicCost, chalkCost) = RitualCircle.GetCurrentCost(costInfo);
            if (!ResourceManager.Instance.CanBuy(magicCost, chalkCost, 0)) return;
        } 
        else
        {
            if (!ResourceManager.Instance.CanBuy(costInfo.magicCost, costInfo.chalkCost, costInfo.berryCost)) return;    
        }
        
        PlaceBuilding(tile, selectedPrefab);
    }

    // Place building data
    public PlaceBuildingUIData GetPlaceBuildingUIData()
    {
        AntHill anthill = antHillPrefab.GetComponent<AntHill>();
        BerryBush berry = berryPrefab.GetComponent<BerryBush>();
        MagicCircle circle = magicCirclePrefab.GetComponent<MagicCircle>();

        return new PlaceBuildingUIData
        {
            anthillChalkPerTick = anthill.chalkPerTick,
            anthillMagicCost = anthill.GetBuildingData().magicCost,
            berryBushBerriesPerTick = berry.berriesPerTick,
            berryBushMagicCost = berry.GetBuildingData().magicCost,
            magicCircleRadius = circle.purifyRadiusPerTier[circle.TierLevel],
            magicCircleMagicCost = circle.GetBuildingData().magicCost,
            magicCircleChalkCost = circle.GetBuildingData().chalkCost
        };
    }
}