public class MagicCircle : Building
{
    public int purifyRadius = 1;

    public override void Init(Tile tile)
    {
        base.Init(tile);
        PurifyArea();
    }

    void PurifyArea()
    {
        var tiles = TileManager.Instance.GetTilesInRange(tile.GridPosition, purifyRadius);
        foreach (var t in tiles)
        {
            t.Purify();
        }
    }
}