using UnityEngine;

public class AntHill : Building
{
    public int chalkPerTick = 5;
    public int requiredBerriesPerTick = 2;

    public override void Init(Tile tile)
    {
        base.Init(tile);
        EventBus.OnTick += CollectChalk;
    }

    void OnDestroy()
    {
        EventBus.OnTick -= CollectChalk;
    }

    void CollectChalk()
    {
        // Ants only work if berries are available
        if (!ResourceManager.Instance.SpendBerries(requiredBerriesPerTick)) return;

        ResourceManager.Instance.AddChalk(chalkPerTick, requiredBerriesPerTick);
    }

    public AntBerryUIData GetAntBerryUIData() => new AntBerryUIData
    {
        productionIcon = TileTypes.GetIcon(tile.GetTileType()),
        productionAmount = chalkPerTick
    };
}