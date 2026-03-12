using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    public GameObject InventoryMenu;
    private bool showInventory;

    // List of items in the inventory, used for saving and loading the inventory state.
    private List<ItemData> _items = new List<ItemData>();

    // Suggested by Claude to fix scope issues
    // Public accessor for the items list, used by SaveController for saving/loading
    public List<ItemData> Items
    {
        get => _items;
        set => _items = value;
    }

    // Instance of the InventoryManager so it can be used in by SaveController
    public static InventoryManager Instance { get; private set; }

    // First item slot
    public ItemSlot[] itemSlot;
    
    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }

        // Hide inventory until opened
        showInventory = false;
        InventoryMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for y keypress
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            showInventory = !showInventory;
            InventoryMenu.SetActive(showInventory);
            // Pause game while in menu
            Time.timeScale = showInventory ? 0 : 1;
        }        
    }

    // For adding item through loot scene
    public void AddItemData(string itemName, int quantity, string spriteName)
    {
        _items.Add(new ItemData(itemName, quantity, spriteName));
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite);
                return;
            }
        }
    }

    /// <summary
    /// Loads the items whenever the save is loaded, used with
    /// SaveController to load the items from the save data and add them to the inventory.
    /// </summary>
    public void LoadItems(List<ItemData> items)
    {
        _items = new List<ItemData>(items);    
    }

    public void RefreshSlotUI()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (i < _items.Count)
            {
                Sprite sprite = Resources.Load<Sprite>("Sprites/" + _items[i].spriteName);
                itemSlot[i].AddItem(_items[i].itemName, _items[i].quantity, sprite);
            }
        }
    }
}