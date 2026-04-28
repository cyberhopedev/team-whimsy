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
    [Header("Cottage Fields")]
    [SerializeField] private TextMeshProUGUI cottageCurrAmt;
    [SerializeField] private TextMeshProUGUI cottageMaxAmt;
    [Header("Ant/Berry Fields")]
    [SerializeField] private Image antBerryIcon;
    [SerializeField] private TextMeshProUGUI antBerryProdAmt;
    [Header("Upgrade Fields")]
    [SerializeField] private TextMeshProUGUI upgradeNextTier;
    [SerializeField] private TextMeshProUGUI upgradeMagicCost;
    [SerializeField] private TextMeshProUGUI upgradeChalkCost;
    [SerializeField] private TextMeshProUGUI upgradeBerriesCost;

    private List<GameObject> uiHovers = new List<GameObject>();

    void Start()
    {
        uiHovers.Add(CottageHover);
        uiHovers.Add(AntBerryHover);
        uiHovers.Add(MagicCircleHover);
        uiHovers.Add(PlaceBuildingHover);
        uiHovers.Add(PlaceRitualHover);
        uiHovers.Add(UpgradeStuffHover);
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
        // Don't change ui if something is selected
        if (selectedTile != null) return;

        Tile tile = TileManager.Instance.GetTileFromMouse();
        if (tile == hoveredTile) return;

        hoveredTile = tile;
        UpdateUIDisplay(tile);
    }

    private void UpdateUIDisplay(Tile tile)
    {
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
                    FillCottageUI(tile);
                    UpgradeStuffHover.SetActive(true);
                    FillUpgradeUI(tile);
                    break;
                case TileType.Cottage3:
                    ShowUI(CottageHover);
                    FillCottageUI(tile);
                    break;
                case TileType.Anthill:
                case TileType.BerryBush:
                case TileType.BerryBushPlus:
                    ShowUI(AntBerryHover);
                    FillAntBerryUI(tile);
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
                case TileType.Lake:
                case TileType.ForestImpure:
                case TileType.CorruptedForest:
                    foreach (GameObject uiGroup in uiHovers)
                    {
                        uiGroup.SetActive(false);
                    }
                    break;
            }
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
    }

    /// <summary>
    /// Handles left click selection of a tile
    /// </summary>
    void HandleClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (Input.mousePosition.x >= 1889) return;  // just clicking on ui

        Tile tile = TileManager.Instance.GetTileFromMouse();
        // Deselect tile if they click on no tile or same tile
        if (tile == null || (tile == selectedTile && selectedTile.GetTileType() != TileType.Cottage))  {
            // Clear highlight on deselect
            if (selectedTile != null)
                selectedTile.SetHighlight(false);
            selectedTile = null;
            Debug.Log("deselecting tile");
            return;
        }
        // Clear previous selection's highlight
        if (selectedTile != null)
            selectedTile.SetHighlight(false);
        selectedTile = tile;
        selectedTile.SetHighlight(true); // border highlight

        Debug.Log("selectedTile: " + selectedTile.GetTileType());
        // Debug.Log("tile type: " + tile.GetTileType() + "  ||  " + "ritualSite?: " + tile.IsRitualSite);

        UpdateUIDisplay(tile);

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
        Debug.Log("tile that is currently selected: " + selectedTile);
        BuildingManager.Instance.TryPlaceSelected(selectedTile, BuildingManager.Instance.GetPrefabForTileType(TileType.BerryBush));
    }

    public void OnAnthill()
    {
        Debug.Log("selectedTile: " + selectedTile);
        BuildingManager.Instance.TryPlaceSelected(selectedTile, BuildingManager.Instance.GetPrefabForTileType(TileType.Anthill));
    }

    private void FillCottageUI(Tile tile)
    {
        Cottage cottage = tile.OccupyingBuilding as Cottage;
        if (cottage == null) return;
        CottageUIData data = cottage.GetCottageUIData();
        // set fields
        cottageCurrAmt.text = data.magicPerClick.ToString();
        cottageMaxAmt.text = data.maxMagic.ToString();
    }

    private void FillAntBerryUI(Tile tile)
    {
        // Try anthill first, then berrybush
        AntHill anthill = tile.OccupyingBuilding as AntHill;
        if (anthill != null) 
        { 
            var data = anthill.GetAntBerryUIData(); 

            // Fill
            antBerryIcon.sprite = data.productionIcon;
            antBerryProdAmt.text = data.productionAmount.ToString();
            return; 
        }

        BerryBush berry = tile.OccupyingBuilding as BerryBush;
        if (berry != null) 
        { 
            var data = berry.GetAntBerryUIData(); 
            // fill
            antBerryIcon.sprite = data.productionIcon;
            antBerryProdAmt.text = data.productionAmount.ToString();
        }
    }

    // private void FillPlaceBuildingUI()
    // {
    //     PlaceBuildingUIData data = BuildingManager.Instance.GetPlaceBuildingUIData();
    //     // fill fields
    // }

    private void FillUpgradeUI(Tile tile)
    {
        // Cottage and MagicCircle both have GetUpgradeUIData()
        Cottage cottage = tile.OccupyingBuilding as Cottage;
        if (cottage != null) 
        { 
            var data = cottage.GetUpgradeUIData(); 
            // fill values
            upgradeNextTier.text = "Upgrade to tier " + data.nextTier.ToString() + ":";
            upgradeMagicCost.text = data.magicCost.ToString();
            upgradeChalkCost.text = data.chalkCost.ToString();
            upgradeBerriesCost.text = "0";
            return; 
        }

        MagicCircle circle = tile.OccupyingBuilding as MagicCircle;
        if (circle != null) 
        { 
            var data = circle.GetUpgradeUIData(); 
            upgradeNextTier.text = "Upgrade to tier " + data.nextTier.ToString() + ":";
            upgradeMagicCost.text = data.magicCost.ToString();
            upgradeChalkCost.text = data.chalkCost.ToString();
            upgradeBerriesCost.text = "0";
        }
    }

    void OnEnable()
    {
        EventBus.OnTileChanged += OnTileChanged;
    }

    void OnDisable()
    {
        EventBus.OnTileChanged -= OnTileChanged;
    }

    void OnTileChanged(Tile tile)
    {
        // Only refresh if this is the tile currently being shown
        if (tile == hoveredTile || tile == selectedTile)
        {
            UpdateUIDisplay(tile);
        }
    }
}