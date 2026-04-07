using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class that holds the logic for the Materials Puzzle.  
/// Generate the word buttons at runtime by loading all `WordData` scriptable objects 
/// (use `Resources.LoadAll<WordData>` or assign them in inspector arrays grouped by slot).
/// </summary>
public class MaterialsPuzzle : BasePuzzle
{
    public List<ItemData> inventory = new();
    [SerializeField] private ItemData mudItem, bonesItem, chalkyMixture;
    // [SerializeField] private ItemData rootItem;
    // Screen visuals
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private Image item1;
    [SerializeField] private Image item2;

    /// <summary>
    /// Adds item to the inventory   
    /// </summary>
    /// <param name="item">The item to add </param>
    public void AddItem(ItemData item)
    {
        inventory.Add(item);
        // Visually put item in screen box
        progress.text = inventory.Count.ToString() + "/2";
        if (inventory.Count == 1)
        {
            item1.sprite = item.icon;
        } 
        else
        {
            item2.sprite = item.icon;
        }
        

        TryCraft();
    }

    /// <summary>
    /// Checks if the player's inventory has the required ingredients to
    /// craft the chalky mixture  
    /// </summary>
    public void TryCraft()
    {
        if(inventory.Contains(mudItem) && inventory.Contains(bonesItem)
            && !inventory.Contains(chalkyMixture))
        {
            inventory.Remove(mudItem);
            inventory.Remove(bonesItem);
            inventory.Add(chalkyMixture);
            DialogueManager.Instance.TriggerWhim("crafted_chalky_mixture");
        }

        if(CheckSolution())
        {
            Solve();
        }
    }

    // win result
    public void WinResult()
    {
        Debug.Log("You win ! ");
        // item1.sprite = chalkyMixture.icon;
        // item2.sprite = Resources.Load<Sprite>("Sprites/TransparentSprite");
    }

    /// <summary>
    /// Checks if the player's inventory has the chalky mixture and the root item    
    /// </summary>
    public override bool CheckSolution() => 
        inventory.Contains(chalkyMixture); // && inventory.Contains(rootItem);
}