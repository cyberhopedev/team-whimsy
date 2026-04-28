using UnityEngine;
// Structs for filling in hover information for UI

public struct CottageUIData
{
    public int magicPerClick;
    public int currentMagic;
    public int maxMagic;
}

public struct AntBerryUIData
{
    public Sprite productionIcon;
    public int productionAmount;
}

public struct MagicCircleUIData
{
    public int purificationRadius;
}

public struct PlaceBuildingUIData
{
    public int anthillChalkPerTick;
    public int anthillMagicCost;
    public int berryBushBerriesPerTick;
    public int berryBushMagicCost;
    public int magicCircleRadius;
    public int magicCircleMagicCost;
    public int magicCircleChalkCost;
}

public struct PlaceRitualUIData
{
    public int magicCost;
    public int chalkCost;
}

public struct UpgradeUIData
{
    public int nextTier;
    public int magicCost;
    public int chalkCost;
}