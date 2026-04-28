using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MagicCircle : Building
{
    [Header("Upgrade Settings")]
    public int[] purifyRadiusPerTier = { 1, 2, 3 };  // MagicCircle, MagicCircleA, MagicCircleB
    public BuildingData[] upgradeData; // assign 3 BuildingData assets in Inspector

    private int tierLevel = 0;
    public int TierLevel => tierLevel;

    private static readonly TileType[] tierTileTypes =
    {
        TileType.MagicCircle,
        TileType.MagicCircleA,
        TileType.MagicCircleB
    };

    public override void Init(Tile tile)
    {
        base.Init(tile);
        PurifyArea();
    }

    void PurifyArea()
    {
        int radius = purifyRadiusPerTier[tierLevel];
        var tiles = TileManager.Instance.GetTilesInRange(tile.GridPosition, radius);
        foreach (var t in tiles)
        {
            t.Purify();
            if (t.GetTileType() == TileType.CorruptedForest || t.GetTileType() == TileType.ForestImpure)
            {
                t.SetSprite(TileTypes.GetIcon(TileType.ForestPure));
            }
            else if (t.GetTileType() == TileType.CorruptedRitualCircle)
            {
                t.SetSprite(TileTypes.GetIcon(TileType.RitualCircle));
            }
            else if (t.GetTileType() != TileType.ForestPure)
            {
                Debug.Log("We haven't considered " + TileTypes.GetName(t.GetTileType()) + " purifying sprite change");
            }
        }
    }

    public bool TryUpgrade()
    {
        if (tierLevel >= 2) return false;

        ResourceManager resources = ResourceManager.Instance;
        BuildingData nextData = upgradeData[tierLevel + 1];

        if (!resources.CanBuy(nextData.magicCost, nextData.chalkCost, nextData.berryCost)) return false;
        tierLevel++;

        TileType newType = tierTileTypes[tierLevel];
        tile.SetTileType(newType);
        tile.SetSprite(TileTypes.GetIcon(newType));
        EventBus.OnTileChanged?.Invoke(tile);

        // Tier 2 (MagicCircleA): lock purified tiles so they can't be corrupted
        if (tierLevel == 1)
        {
            var tiles = TileManager.Instance.GetTilesInRange(tile.GridPosition, purifyRadiusPerTier[tierLevel]);
            foreach (var t in tiles)
            {
                t.IsLocked = true;
            }
        }

        // Always re-purify on upgrade
        PurifyArea();

        return true;
    }

    public MagicCircleUIData GetMagicCircleUIData() => new MagicCircleUIData
    {
        purificationRadius = purifyRadiusPerTier[tierLevel]
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