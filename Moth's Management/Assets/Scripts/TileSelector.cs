using UnityEngine;

/// <summary>
/// Handles mouse input and tile selection/interaction
/// </summary>
public class TileSelector : MonoBehaviour
{
    private Tile hoveredTile;
    private Tile selectedTile;

    void Update()
    {
        HandleHover();
        HandleClick();
    }

    /// <summary>
    /// Continuously tracks which tile the mouse is over
    /// </summary>
    void HandleHover()
    {
        Tile tile = TileManager.Instance.GetTileFromMouse();

        if (tile == hoveredTile) return;

        // Mouse moved to a new tile
        hoveredTile?.OnHoverExit();
        hoveredTile = tile;
        hoveredTile?.OnHoverEnter();
    }

    /// <summary>
    /// Handles left click selection of a tile
    /// </summary>
    void HandleClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Tile tile = TileManager.Instance.GetTileFromMouse();
        if (tile == null) return;

        selectedTile?.OnDeselect();
        selectedTile = tile;
        selectedTile.OnSelect();
    }
}