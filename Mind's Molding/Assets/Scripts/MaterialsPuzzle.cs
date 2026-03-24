using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that holds the logic for the Materials Puzzle.  
/// Generate the word buttons at runtime by loading all `WordData` scriptable objects 
/// (use `Resources.LoadAll<WordData>` or assign them in inspector arrays grouped by slot).
/// </summary>
public class MaterialsPuzzle : BasePuzzle
{
    public List<ItemData> inventory = new();
    [SerializeField] private ItemData mudItem, bonesItem, chalkyMixture;
    [SerializeField] private ItemData rootItem;

    /// <summary>
    /// Adds item to the inventory   
    /// </summary>
    /// <param name="item">The item to add </param>
    public void AddItem(ItemData item)
    {
        
    }

    /// <summary>
    /// Checks if the player's inventory has the required ingredients to
    /// craft the chalky mixture  
    /// </summary>
    public void TryCraft()
    {
        
    }

    /// <summary>
    /// Checks if the player's inventory has the chalky mixture and the root item    
    /// </summary>
    public override bool CheckSolution() => pass;
}