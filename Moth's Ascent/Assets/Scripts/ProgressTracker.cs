using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Tracks which battle encounters have been cleared and story events
/// that have been completed. This can be used to determine which encounters
/// what the save data should consider.
/// </summary>
public class ProgressTracker : MonoBehaviour
{
    // Singleton instance of the ProgressTracker
    public static ProgressTracker Instance { get; private set; }

    // List of cleared encounters
    private List<string> _clearedEncountersFlags = new List<string>();
    // List of completed story
    private List<string> _storyProgressionFlags = new List<string>();

    // Read only access to the cleared encounters and story progression flags
    public IReadOnlyList<string> ClearedEncountersFlags => _clearedEncountersFlags.AsReadOnly();
    public IReadOnlyList<string> StoryProgressionFlags => _storyProgressionFlags.AsReadOnly();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Load progression data immediately on init
            if (SaveController.Instance != null && SaveController.Instance.currentSlotIdx >= 0)
            {
                SaveData data = SaveController.Instance.ReadSaveSlot(SaveController.Instance.currentSlotIdx);
                if (data != null)
                {
                    _clearedEncountersFlags = new List<string>(data.clearedEncountersFlags);
                    _storyProgressionFlags = new List<string>(data.storyProgressionFlags);
                    Debug.Log($"ProgressTracker loaded {_clearedEncountersFlags.Count} cleared encounters");
                }
            }
        }
        else
        {
            Destroy(this);
        }
    }

    public void ClearEncounter(string encounterName)
    {
        if (!_clearedEncountersFlags.Contains(encounterName))
            _clearedEncountersFlags.Add(encounterName);
    }

    public void SetStoryFlag(string flagName)
    {
        if (!_storyProgressionFlags.Contains(flagName))
            _storyProgressionFlags.Add(flagName);
    }

    public bool isEncounterCleared(string encounterName) => _clearedEncountersFlags.Contains(encounterName);

    public bool hasStoryFlag(string flagName) => _storyProgressionFlags.Contains(flagName);

    public void LoadEncounters(List<string> encounters) => _clearedEncountersFlags = new List<string>(encounters);

    public void LoadStoryProgression(List<string> storyProgression) => _storyProgressionFlags = new List<string>(storyProgression);
}