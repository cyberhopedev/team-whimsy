using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Stores data that needs to be saved for player progression, which includes
/// player position, inventory, current abilities, and cleared encounters
/// </summary>
[System.Serializable]
public class SaveData
{
    // Position of the player in the world, used for loading back into the same spot
    public Vector3 playerPosition;
    // Health of the player, used for loading back with the same health
    public int currentHP;
    // Inventory of the player, used for loading back with the same items
    public List<string> inventoryItems;
    // Abilities of the player, used for loading back with the same abilities
    public List<string> playerAbilities;
    // Cleared encounters, used for loading back with the same progress
    public List<string> clearedEncounters;
    public string mapBoundary; // possibly use for when going between "squares" in the dungeon

    // Game progression flags
    public List<string> clearedEncountersFlags = new List<string>();
    public List<string> storyProgressionFlags = new List<string>();

    // Load menu information
    public string saveTimestamp;
    public float totalPlayTimeSeconds;
    public string locationName; 
}