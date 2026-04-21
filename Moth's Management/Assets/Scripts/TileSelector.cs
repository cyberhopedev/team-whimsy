using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Handles mouse input and tile selection/interaction
/// </summary>
public class TileSelector : MonoBehaviour
{
    private Tile hoveredTile;
    private Tile selectedTile;

    // UI Stuff
    [Header("UI Fields")]
    [SerializeField] private TextMeshProUGUI nameT;
    [SerializeField] private TextMeshProUGUI levelT;
    [SerializeField] private TextMeshProUGUI descriptionT;
    [SerializeField] private Image icon;


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

        // // Mouse moved to a new tile
        // hoveredTile?.OnHoverExit();
        // hoveredTile = tile;
        // hoveredTile?.OnHoverEnter();

        // Update UI display
        if (tile != null)
        {
            nameT.text = TileTypes.GetName(tile.GetTileType());
            descriptionT.text = TileTypes.GetDescription(tile.GetTileType());
            icon.sprite = TileTypes.GetIcon(tile.GetTileType());   
        }
    }

    /// <summary>
    /// Handles left click selection of a tile
    /// </summary>
    void HandleClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Tile tile = TileManager.Instance.GetTileFromMouse();
        if (tile == null) return;

        // If clicking the cottage, collect magic
        if (tile.Type == TileType.Cottage)
        {
            Debug.Log("reached here");
            // FindObjectOfType<Cottage>()?.CollectMagic();
            Cottage cottageTile = FindAnyObjectByType<Cottage>();
            if (cottageTile != null)
            {
                cottageTile.CollectMagic();
            } else
            {
                Debug.Log(":(");
            }
            return;
        }

        // Otherwise attempt to place selected building
        BuildingManager.Instance.TryPlaceSelected(tile);
    }
}