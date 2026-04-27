using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

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
    [Header("UI Panels")]
    [SerializeField] private GameObject CottageHover;
    [SerializeField] private GameObject AntBerryHover;
    [SerializeField] private GameObject MagicCircleHover;
    [SerializeField] private GameObject PlaceBuildingHover;
    [SerializeField] private GameObject PlaceRitualHover;
    [SerializeField] private GameObject UpgradeStuffHover;
    private List<GameObject> uiHovers;

    void Start()
    {
        // uiHovers.Add(CottageHover);
        // uiHovers.Add(AntBerryHover);
        // uiHovers.Add(MagicCircleHover);
        // uiHovers.Add(PlaceBuildingHover);
        // uiHovers.Add(PlaceRitualHover);
    }

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
        // Don't show hover stuff unless no tile selected
        if (selectedTile != null) return;

        Tile tile = TileManager.Instance.GetTileFromMouse();

        if (tile == hoveredTile) return;

        // Update UI display
        if (tile != null)
        {
            nameT.text = TileTypes.GetName(tile.GetTileType());
            descriptionT.text = TileTypes.GetDescription(tile.GetTileType());
            icon.sprite = TileTypes.GetIcon(tile.GetTileType());   
            levelT.text = TileTypes.GetTierLvl(tile.GetTileType());

            // Determine which UI sections should be displayed
            switch (tile.GetTileType())
            {
                case TileType.Cottage:
                case TileType.Cottage2:
                    ShowUI(CottageHover);
                    UpgradeStuffHover.SetActive(true);
                    break;
                case TileType.Cottage3:
                    ShowUI(CottageHover);
                    break;
                case TileType.Anthill:
                case TileType.BerryBush:
                case TileType.BerryBushPlus:
                    ShowUI(AntBerryHover);
                    break;
                case TileType.MagicCircle:
                case TileType.MagicCircleA:
                    ShowUI(MagicCircleHover);
                    UpgradeStuffHover.SetActive(true);
                    break;
                case TileType.MagicCircleB:
                    ShowUI(MagicCircleHover);
                    break;
                case TileType.ForestPure:
                    ShowUI(PlaceBuildingHover);
                    break;
            }

            // - cottage hover
            // - antberryhover
            // - magiccirclehover
            // - placebuildinghover
            // - placeritualhover
            // - upgrade stuff
        }
    }

    private void ShowUI(GameObject hover)
    {
        foreach (GameObject uiGroup in uiHovers)
        {
            if (uiGroup != hover)
            {
                uiGroup.SetActive(false);
            } else
            {
                uiGroup.SetActive(true);
            }
        }
        UpgradeStuffHover.SetActive(false);
    }

    /// <summary>
    /// Handles left click selection of a tile
    /// </summary>
    void HandleClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Tile tile = TileManager.Instance.GetTileFromMouse();
        // Deselect tile if they click on no tile or same tile
        if (tile == null || tile == selectedTile)  {
            selectedTile = null;
            return;
        }
        selectedTile = tile;

        Debug.Log("selectedTile: " + selectedTile.GetTileType());
        // Debug.Log("tile type: " + tile.GetTileType() + "  ||  " + "ritualSite?: " + tile.IsRitualSite);

        // If clicking the cottage, collect magic
        if (tile.Type == TileType.Cottage)
        {
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
    }

    public void OnBuildMagicCircle()
    {
        Debug.Log("selectedTile: " + selectedTile);
        BuildingManager.Instance.TryPlaceSelected(selectedTile, BuildingManager.Instance.GetPrefabForTileType(TileType.MagicCircle));
    }

    public void OnRitualCircle()
    {
        Debug.Log("selectedTile: " + selectedTile);
        BuildingManager.Instance.TryPlaceSelected(selectedTile, BuildingManager.Instance.GetPrefabForTileType(TileType.RitualCircle));
    }

    public void OnBerryBush()
    {
        Debug.Log("selectedTile: " + selectedTile);
        BuildingManager.Instance.TryPlaceSelected(selectedTile, BuildingManager.Instance.GetPrefabForTileType(TileType.BerryBush));
    }

    public void OnAnthill()
    {
        Debug.Log("selectedTile: " + selectedTile);
        BuildingManager.Instance.TryPlaceSelected(selectedTile, BuildingManager.Instance.GetPrefabForTileType(TileType.Anthill));
    }
}