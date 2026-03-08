using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public static InventoryManager Instance;
    
    void Awake()
    {
        // Singleton
         if (Instance == null)
        {
            Instance = this;
        }
        else
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

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        // Add item to be saved in the save data
        _items.Add(new ItemData(itemName, quantity, itemSprite.name));

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
}
