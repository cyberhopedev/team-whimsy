using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool showInventory;
    
    void Awake()
    {
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
}
