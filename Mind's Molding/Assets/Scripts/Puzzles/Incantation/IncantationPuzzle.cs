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
    [SerializeField] private PuzzlePiece[] pieces;
    private WordData selectedSubject, selectedEffect, selectedTarget;

    /// <summary>
    /// Allows the player to select the words in a specific
    /// slot (this approach utilizes dragging given words within the UI)
    /// </summary>
    /// <param name="slot">The slot of the word choice</param>
    /// <param name="word">The data associated with the word</param>
    public void SelectWord(WordSlot slot, WordData word)
    {
        switch(slot)
        {
            case WordSlot.Subject:
                selectedSubject = word;
                break;
            case WordSlot.Effect:
                selectedEffect = word;
                break;
            case WordSlot.Target:
                selectedTarget = word;
                break;
        }
    }

    public void OnPieceSwapped()
    {
        if(CheckSolution())
        {
            Solve();
        }
    }

    /// <summary>
    /// Checks if the player's chosen words are correct   
    /// </summary>
    public override bool CheckSolution()
    {
        WordData subject = null, effect = null, target = null;

        foreach (PuzzlePiece piece in pieces)
        {
            switch (piece.CurrentSlotIndex)
            {
                case 0: subject = piece.wordData; break;
                case 1: effect  = piece.wordData; break;
                case 2: target  = piece.wordData; break;
            }
        }

        return subject == correctSubject &&
               effect  == correctEffect  &&
               target  == correctTarget;
    }

    // win result
    public void WinResult()
    {
        Debug.Log("You win ! ");
        gameObject.SetActive(false);
    }
        
}