using UnityEngine;

/// <summary>
/// Scriptable object to be used to set info for different buildings
/// </summary>
[CreateAssetMenu(menuName = "Game/Building")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public int chalkCost;
    public int berryCost;
    public int magicCost;
}