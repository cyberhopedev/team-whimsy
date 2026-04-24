using UnityEngine;

public class AntHill : Building
{
    public int chalkPerTick = 5;

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
        if (!ResourceManager.Instance.SpendBerries(1)) return;

        ResourceManager.Instance.AddChalk(chalkPerTick);
    }
}