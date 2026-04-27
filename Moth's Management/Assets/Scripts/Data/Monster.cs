using UnityEngine;

/// <summary>
/// Lightweight runtime data for one monster.
/// Not a MonoBehaviour, as MonsterManager owns its lifecycle.
/// </summary>
public class Monster
{
    public Vector2Int Position { get; set; }
    public GameObject Visual   { get; set; } // optional sprite object
 
    public Monster(Vector2Int startPosition)
    {
        Position = startPosition;
    }
}
