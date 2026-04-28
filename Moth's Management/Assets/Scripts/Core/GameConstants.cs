using UnityEngine;

/// <summary>
/// Constants for game values logic made by Ethan, currently used in
/// MonsterManager
/// </summary>
public static class GameConstants
{
    // Monster Den Constants
    public const float MonsterSpawnChance     = 0.15f; // F% per den per tick
    public const float MonsterDenFormChance   = 0.05f; // Z% chance to form
    public const float MonsterDenFormCooldown = 10f;   // seconds between formation checks
    public const int   MonsterDenMinDistance  = 4;     // Chebyshev distance

    public const int   ImpurityFromMonster    = 25;  // added when monster enters tile

    // Seconds for time TimeStepSeconds
    public const float TimeStepSeconds        = 2f; 
}