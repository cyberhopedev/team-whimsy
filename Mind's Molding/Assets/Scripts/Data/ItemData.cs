using UnityEngine;

/// <summary>
/// Stores the data for an item in the inventory, 
/// used for saving and loading the inventory state
/// </summary>
[CreateAssetMenu(menuName = "MindsMolding/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
    public bool isCraftResult;
}