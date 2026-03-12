using UnityEngine;

/// <summary>
/// Stores the data for an item in the inventory, 
/// used for saving and loading the inventory state
/// </summary>
[System.Serializable]
public class ItemData
{
    public string itemName;
    public string spriteName;

    public ItemData(string itemName, string spriteName)
    {
        this.itemName = itemName;
        this.spriteName = spriteName;
    }
}