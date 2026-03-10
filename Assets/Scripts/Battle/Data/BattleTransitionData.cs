using UnityEngine;
using System.Collections.Generic;
/// <summary> 
/// Holds the data between the overworld and battle scene transition
/// </summary>
public static class BattleTransitionData
{
    public static List<GameObject> EncounterEnemies {get; set;}
    public static string ReturnScene = "BetaScene";
}