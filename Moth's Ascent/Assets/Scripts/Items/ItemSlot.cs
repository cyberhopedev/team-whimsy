using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Slot for each item in the inventory menu, displays quantity and allows
// item to be selected
public class ItemSlot : MonoBehaviour
{
    // Item Data
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;

    // Item Slot
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;

    public void AddItem(string itemName, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.quantity = 1;  // always consume 1 item
        this.itemSprite = itemSprite;
        isFull = true;

        quantityText.text = "1";
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }
    
}
