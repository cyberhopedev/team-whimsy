using UnityEngine;

public class Cottage : Building
{
    [Header("Upgrade Settings")]
    public int[] magicPerClick = { 1, 2, 3 };     // cottage, cottage1, cottage2
    public int[] magicCapacity = { 80, 290, 380 };   // capacity per upgrade level

    private int tierLevel = 0;
    private int currentMagic = 0;
    // Getters
    public int TierLevel => tierLevel;
    public int CurrentMagic => currentMagic;
    public int Capacity => magicCapacity[tierLevel];

    public BuildingData[] upgradeData; // assign 3 BuildingData assets in Inspector

    public override void Init(Tile tile)
    {
        base.Init(tile);
    }

    /// <summary>
    /// Called when the player clicks the cottage to collect magic
    /// </summary>
    public int CollectMagic()
    {
        // Debug.Log("collecting magic");
        int amount = Mathf.Min(magicPerClick[tierLevel], magicCapacity[tierLevel] - currentMagic);
        currentMagic += amount;
        ResourceManager.Instance.AddMagic(amount);
        EventBus.OnMagicCollected?.Invoke(currentMagic);
        return amount;
    }

    /// <summary>
    /// Upgrades the cottage if not already at max level
    /// </summary>
    public bool TryUpgrade()
    {
        if (tierLevel >= 2) return false;

        ResourceManager resources = ResourceManager.Instance;
        BuildingData nextData = upgradeData[tierLevel + 1];

        if (!resources.CanBuy(nextData.magicCost, nextData.chalkCost, nextData.berryCost)) return false;
        tierLevel++;
        currentMagic = 0; // fresh capacity for new tier

        TileType[] tierTileTypes = { TileType.Cottage, TileType.Cottage2, TileType.Cottage3 };
        tile.SetTileType(tierTileTypes[tierLevel]);
        tile.SetSprite(TileTypes.GetIcon(tierTileTypes[tierLevel]));
        EventBus.OnTileChanged?.Invoke(tile);

        return true;
    }

    public CottageUIData GetCottageUIData() => new CottageUIData
    {
        magicPerClick = magicPerClick[tierLevel],
        currentMagic = currentMagic,
        maxMagic = magicCapacity[tierLevel] - currentMagic
    };

    public UpgradeUIData GetUpgradeUIData()
    {
        if (tierLevel >= 2) return default;
        BuildingData nextData = upgradeData[tierLevel + 1];

        return new UpgradeUIData
        {
            nextTier = tierLevel + 2,
            magicCost = nextData.magicCost,
            chalkCost = nextData.chalkCost
        };
    }
}