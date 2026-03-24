using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that holds the logic for the Incantation Puzzle.
/// `PuzzlePiece` is a small component on each UI Image that implement.
/// `IBeginDragHandler`, `IDragHandler`, `IEndDragHandler` - Unity's built-in drag interfaces. Store the slot index on drop.
/// </summary>
public class IncantationPuzzle : BasePuzzle
{
    [SerializeField] private WordData correctSubject, correctEffect, correctTarget;
    private WordData selectedSubject, selectedEffect, selectedTarget;

    /// <summary>
    /// Allows the player to select the words in a specific
    /// slot  
    /// </summary>
    /// <param name="slot">The slot of the word choice</param>
    /// <param name="word">The data associated with the word</param>
    public void SelectWord(WordSlot slot, WordData word)
    {

    }

    /// <summary>
    /// Checks if the player's chosen words are correct   
    /// </summary>
    public override bool CheckSolution() => pass;
}