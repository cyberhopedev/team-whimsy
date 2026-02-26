using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool showInventory;

    // Possible inventory items
    public enum ITEM {MEALBERRY, POISON_SHROOM}

    // First item slot
    public GameObject slot1;

    // Current inventory
    List<ITEM> inventory;
    
    void Awake()
    {
        // Hide inventory until opened
        showInventory = false;
        InventoryMenu.SetActive(false);
        slot1.SetActive(false);
        inventory = new List<ITEM>();
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

    public void AddItem()
    {
        // how to change UI panel source image in unity using code
        slot1.SetActive(true);
    }
}
