using UnityEngine;

public class Cottage : Building
{
    [Header("Upgrade Settings")]
    public int[] magicPerClick = { 10, 20, 30 };     // cottage, cottage1, cottage2
    public int[] magicCapacity = { 100, 200, 300 };   // capacity per upgrade level

    private int tierLevel = 1;
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
        Debug.Log("here :)");
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

        if (!resources.Buy(nextLevel.magicCost, nextLevel.chalkChost, nextLevel.berryCost)) return false;
         
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
}