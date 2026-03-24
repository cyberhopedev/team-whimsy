using UnityEngine;

/// <summary>
/// Subscribes to all three puzzles' `OnPuzzleSolved` events. 
/// Once all three flags are true, calls `GameManager.TriggerVictory()`.
/// </summary>
public class RitualResolver : MonoBehaviour
{
    // Magic circle puzzle flag
    [SerializeField] private MagicCirclePuzzle circlePuzzle;
    // Incantation puzzle flag
    [SerializeField] private IncantationPuzzle incantationPuzzle;
    // Materials puzzle flag
    [SerializeField] private MaterialsPuzzle materialsPuzzle;

    bool hasMagicCircle, hasIncantation, hasMaterials;

    /// <summary>
    /// Called when the script instance is being loaded. Registers event listeners for puzzle completion.
    /// </summary>
    public void OnEnable()
    {
        circlePuzzle.OnPuzzleSolved.AddListener(() => { pass; });
        incantationPuzzle.OnPuzzleSolved.AddListener(() => { pass; });
        materialsPuzzle.OnPuzzleSolved.AddListener(() => { pass; });
    }

    /// <summary>
    /// Checks if the player has solved all of the puzzles
    /// </summary>
    public void Check()
    {
        
    }
}