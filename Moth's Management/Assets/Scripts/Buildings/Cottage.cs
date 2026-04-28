using UnityEngine;

public class Cottage : Building
{
    [Header("Upgrade Settings")]
    public int[] magicPerClick = { 1, 2, 3 };     // cottage, cottage1, cottage2
    public int[] magicCapacity = { 80, 120, 210 };   // capacity per upgrade level

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
        if (tierLevel >= 3) return false;

        ResourceManager resources = ResourceManager.Instance;
        BuildingData nextLevel = tierLevel == 0 ? upgradeData[1] : upgradeData[2];

        if (!resources.CanBuy(nextLevel.magicCost, nextLevel.chalkCost, nextLevel.berryCost)) return false;
         
        tierLevel++;
        if (tierLevel == 2)
        {
            tile.SetSprite(Resources.Load<Sprite>("Sprites/cabinTwo"));
        } 
        else if (tierLevel == 3)
        {
            tile.SetSprite(Resources.Load<Sprite>("Sprites/cabinThree"));
        }
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
        BuildingData next = upgradeData[tierLevel]; // next tier data
        return new UpgradeUIData
        {
            nextTier = tierLevel + 1,
            magicCost = tierLevel == 0 ? 60 : 105,
            chalkCost = tierLevel == 0 ? 20 : 35
        };
    }
}