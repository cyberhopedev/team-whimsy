using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages monster spaawning and movement, holding the list of monsters
/// spawning logic from dens, movements each tick, and impurity application
/// when they spawn
/// </summary>
public class MonsterManager : MonoBehaviour
{
    // Instance for the monster manager
    public static MonsterManager Instance;

    [Header("Monster Visual Prefab (optional)")]
    public GameObject monsterPrefab; // drag a sprite prefab in if you want visuals

    // All currently living monsters
    private List<Monster> _monsters = new List<Monster>();
    public IReadOnlyList<Monster> ActiveMonsters => _monsters.AsReadOnly();

    private float _tickTimer;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        _tickTimer += Time.deltaTime;
        if (_tickTimer >= GameConstants.TimeStepSeconds)
        {
            _tickTimer -= GameConstants.TimeStepSeconds;
            ProcessTick();
        }
    }

    public void ProcessTick()
    {
        SpawnFromDens();
        MoveMonsters();
        PruneDeadMonsters();
    }

    /// <summary>
    /// Scans all MonsterDen tiles and rolls F% per den to spawn a monster.
    /// </summary>
    void SpawnFromDens()
    {
        // Use CorruptionManager's helper to find all den tiles
        List<Tile> dens = TileManager.Instance.GetAllTilesOfType(TileType.MonsterDen);

        foreach (Tile den in dens)
        {
            float randomVal = Random.value;
            // Debug.Log("randomVal: " + randomVal);
            if (randomVal <= GameConstants.MonsterSpawnChance) continue;

            Monster monster = new Monster(den.GridPosition);

            // Spawn a visual if a prefab is assigned
            if (monsterPrefab != null)
            {
                monster.Visual = Instantiate(
                    monsterPrefab,
                    new Vector3(den.GridPosition.x, den.GridPosition.y, -0.5f),
                    Quaternion.identity);
            }

            _monsters.Add(monster);
            EventBus.OnMonsterSpawned?.Invoke(den);
            Debug.Log($"[MonsterManager] Monster spawned at {den.GridPosition}");
        }
    }

    /// <summary>
    /// Each monster moves to a random adjacent ForestPure tile.
    /// Entering a tile calls tile.AddImpurity() which can push it to ForestImpure.
    /// </summary>
    void MoveMonsters()
    {
        // Snapshot to avoid mutation during iteration
        List<Monster> snapshot = new List<Monster>(_monsters);

        foreach (Monster monster in snapshot)
        {
            // Gather adjacent pure tiles the monster can move to
            List<Tile> pureNeighbours = new List<Tile>();
            foreach (Tile neighbour in TileManager.Instance.GetNeighbors(monster.Position))
            {
                if (neighbour.Type == TileType.ForestPure)
                    pureNeighbours.Add(neighbour);
            }

            // No pure tiles adjacent — monster stays put this tick
            if (pureNeighbours.Count == 0) continue;

            // Pick a random pure neighbour (equal probability)
            Tile destination = pureNeighbours[Random.Range(0, pureNeighbours.Count)];
            monster.Position  = destination.GridPosition;

            // Move visual if one exists
            if (monster.Visual != null)
            {
                monster.Visual.transform.position = new Vector3(
                    destination.GridPosition.x,
                    destination.GridPosition.y,
                    -0.5f);
            }

            // Monster entering the tile adds impurity — Tile handles overflow
            destination.AddImpurity(GameConstants.ImpurityFromMonster);

            EventBus.OnMonsterMoved?.Invoke(destination);
        }
    }

    /// <summary>
    /// Removes monsters that are now standing on a corrupted tile
    /// (the tile flipped under them between ticks).
    /// </summary>
    void PruneDeadMonsters()
    {
        _monsters.RemoveAll(monster =>
        {
            Tile tile = TileManager.Instance.GetTile(monster.Position);
            if (tile == null || tile.IsCorrupted())
            {
                // Destroy the visual GameObject too
                if (monster.Visual != null)
                    Destroy(monster.Visual);
                return true; // remove from list
            }
            return false;
        });
    }


    /// <summary>
    /// Kills any monster currently on the given tile (e.g. when a ward is applied).
    /// </summary>
    public void KillMonsterAt(Vector2Int pos)
    {
        _monsters.RemoveAll(monster =>
        {
            if (monster.Position != pos) return false;
            if (monster.Visual != null) Destroy(monster.Visual);
            return true;
        });
    }
}