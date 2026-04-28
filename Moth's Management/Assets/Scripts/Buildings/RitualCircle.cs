using UnityEngine;

public class RitualCircle : Building
{
    // Base costs defined in BuildingData, these are the per-placement increments
    private const int magicCostIncrement = 10;
    private const int chalkCostIncrement = 1;

    // Tracks how many ritual circles have been placed globally
    private static int placedCount = 0;

    /// <summary>
    /// Returns the current cost to place the next ritual circle
    /// </summary>
    public static (int magic, int chalk) GetCurrentCost(BuildingData baseData)
    {
        return (
            baseData.magicCost + (placedCount * magicCostIncrement),
            baseData.chalkCost + (placedCount * chalkCostIncrement)
        );
    }

    public override void Init(Tile tile)
    {
        base.Init(tile);
        placedCount++;
    }

    // Reset when game restarts
    public static void ResetCount()
    {
        placedCount = 0;
    }
}