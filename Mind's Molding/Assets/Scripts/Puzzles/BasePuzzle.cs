using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Abstract class that each Puzzle part of the contract will inherit
/// </summary>
public abstract class BasePuzzle : MonoBehaviour
{
    // Status of the puzzle being solved or not
    public bool IsSolved {get; private set;}
    // The set event that occurs when the puzzle is solved
    public UnityEvent OnPuzzleSolved;
    // Each puzzle will have it's own solutions
    public abstract bool CheckSolution();

    /// <summary>
    /// Solves the puzzle
    /// </summary>
    protected void Solve()
    {
        // If the puzzle is already solved, stop here
        if(IsSolved)
        {
            return;
        }

        // Otherwise, when this method is called update IsSolved, invoke the event, , and trigger dialogue for Whim stating completion
        IsSolved = true;
        OnPuzzleSolved?.Invoke();
        DialogueManager.Instance.TriggerWhim($"puzzle_{name}_complete");
    }
}