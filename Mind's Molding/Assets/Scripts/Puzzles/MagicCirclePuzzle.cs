using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that holds the logic for the Magic Circle Puzzle.  
/// Have all the pieces that are UI Images in a grid layout group.
/// Each `PuzzlePiece` knows its `correctSlotIndex`, and every swap, call `CheckSolution()`
/// </summary>
public class MagicCirclePuzzle : BasePuzzle
{
    // Pieces used within the magic circle puzzle
    [SerializeField] private PuzzlePiece[] pieces;

    /// <summary>
    /// Every time a pieced is swapped by the player, check to see if it matches solution;
    /// if it does, then solve the puzzle
    /// </summary>
    public void OnPieceSwapped()
    {
        for(int i = 0; i < pieces.Length; i++)
        {
            if(pieces[i].CurrentSlotIndex != pieces[i].correctSlotIndex)
            {
                return false;
            }
            return true;
        }
    }
}
