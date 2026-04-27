using UnityEngine;

public class BerryBush : Building
{
    public int berriesPerTick = 3;
    public int bonusBerriesNearWater = 2;

    private bool isNearWater = false;

    public override void Init(Tile tile)
    {
        base.Init(tile);
        CheckForWater();
        EventBus.OnTick += ProduceBerries;
    }

    void OnDestroy()
    {
        EventBus.OnTick -= ProduceBerries;
    }

    // Next to water means more berries
    void CheckForWater()
    {
        var neighbors = TileManager.Instance.GetNeighbors(tile.GridPosition);
        isNearWater = neighbors.Exists(t => t.Type == TileType.Lake);

        if (isNearWater)
        {
            tile.SetTileType(TileType.BerryBushPlus);
            tile.SetSprite(TileTypes.GetIcon(tile.GetTileType()));
        }
    }

    void ProduceBerries()
    {
        int amount = isNearWater ? berriesPerTick + bonusBerriesNearWater : berriesPerTick;
        ResourceManager.Instance.AddBerries(amount);
    }

    public AntBerryUIData GetAntBerryUIData() => new AntBerryUIData
    {
        productionIcon = Resources.Load<Sprite>("Sprites/BerryIcon"),
        productionAmount = berriesPerTick
    };
}