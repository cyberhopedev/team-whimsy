using UnityEngine;

/// <summary>
/// Handles the logic for placing a building on the game board
/// </summary>
public class BuildingManager : MonoBehaviour
{
    public GameObject ritualPrefab;
    public GameObject antHillPrefab;
    public GameObject berryPrefab;
    public GameObject cottagePrefab;

    /// <summary>
    /// Places the building prefab based on if the tile type is able
    /// to be built on
    /// </summary>
    /// <param name="tile">The current tile attempting to be built on</param>
    /// <param name="prefab">The Building's prefab</param>
    public void PlaceBuilding(Tile tile, GameObject prefab)
    {
        
    }
}