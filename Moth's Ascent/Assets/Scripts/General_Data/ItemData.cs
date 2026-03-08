using UnityEngine;

/// <summary>
/// Stores the data for an item in the inventory, 
/// used for saving and loading the inventory state
/// </summary>
[System.Serializable]
public class ItemData
{
    public string itemName;
    public int quantity;
    public string spriteName;

    public ItemData(string itemName, int quantity, string spriteName)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.spriteName = spriteName;
    }
}