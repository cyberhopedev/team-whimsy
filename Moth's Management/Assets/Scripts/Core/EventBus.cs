using System;
using UnityEngine;

/// <summary>
/// Communication of events related to tiles in order to understand
/// the state of the game at it's current action/event/turn
/// </summary>
public static class EventBus
{
    public static Action<Tile> OnTileChanged;
    public static Action<Building> OnBuildingPlaced;
    public static Action OnResourceChanged;
    public static Action OnRitualCompleted;
    public static Action<bool> OnGameOver;
    public static Action OnTick;
}