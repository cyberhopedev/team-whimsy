using UnityEngine;

/// <summary>
/// Class to hold helpers for the entire game
/// 
/// Usage can be seen in "Add an Interaction System to your Game" video
/// by Game Code Library
/// </summary>
public static class GlobalHelper
{
    // Generate a unique ID for any object
    public static string GenerateUniqueID(GameObject obj)
    {
        return $"{obj.scene.name}_{obj.transform.position.x}_{obj.transform.position.y}";
    }
}
