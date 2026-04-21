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
    }

    void ProduceBerries()
    {
        int amount = isNearWater ? berriesPerTick + bonusBerriesNearWater : berriesPerTick;
        ResourceManager.Instance.AddBerries(amount);
    }
}