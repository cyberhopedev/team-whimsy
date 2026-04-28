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
        isNearWater = neighbors.Exists(t => t.GetTileType() == TileType.Lake);
        Debug.Log("isNearWater: " + isNearWater);

        if (isNearWater)
        {
            tile.SetTileType(TileType.BerryBushPlus);
            tile.SetSprite(TileTypes.GetIcon(TileType.BerryBushPlus));

            berriesPerTick += bonusBerriesNearWater;
            EventBus.OnTileChanged?.Invoke(tile);
        }
    }

    void ProduceBerries()
    {
        int amount = berriesPerTick;
        ResourceManager.Instance.AddBerries(amount);
    }

    public AntBerryUIData GetAntBerryUIData() => new AntBerryUIData
    {
        productionIcon = TileTypes.GetIcon(tile.GetTileType()),
        productionAmount = berriesPerTick
    };
}