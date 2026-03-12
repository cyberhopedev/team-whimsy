using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

/// <summary> 
/// Triggers the battle system by calling this class when the PlayerController
/// collides with an Enemy in the crawler
/// </summary>
public class EnemyEncounter : MonoBehaviour
{
    // Constants for scene names
    public const string battleScene = "BattleMenuScene";
    // Hold a list of enemies that need to spawn
    public List<GameObject> encounterEnemies;
    private bool _triggered = false;
    [SerializeField] private string encounterID; // set unique name per encounter in inspector

    private void Start()
    {
        // // Destroy self if already cleared
        // if (ProgressTracker.Instance != null && 
        //     ProgressTracker.Instance.isEncounterCleared(encounterID))
        // {
        //     Destroy(gameObject);
        // }
        Debug.Log($"EnemyEncounter '{encounterID}' Start. ProgressTracker: {ProgressTracker.Instance}, " +
              $"isCleared: {ProgressTracker.Instance?.isEncounterCleared(encounterID)}");
    
        if (ProgressTracker.Instance != null && 
            ProgressTracker.Instance.isEncounterCleared(encounterID))
        {
            Debug.Log($"Destroying cleared encounter: {encounterID}");
            Destroy(gameObject);
        }
    }

    /// <summary> 
    /// Based on the given collider, if the player triggers it then switch to PlayerBattler
    /// and start the battle with the encountered overworld enemies
    /// </summary>
    /// <param name="c">The collider the overworld player triggers</param>
    private void OnTriggerEnter2D(Collider2D c)
    {
        BattleTransitionData.PlayerPositionBeforeBattle = c.transform.position;
        Debug.Log($"Stored player position before battle: {BattleTransitionData.PlayerPositionBeforeBattle}");

        if (c.TryGetComponent(out PlayerController pc) && !_triggered)
        {   
            _triggered = true;
            BattleTransitionData.EncounterEnemies = encounterEnemies;
            BattleTransitionData.EncounterID = encounterID;
            BattleTransitionData.PlayerPositionBeforeBattle = pc.transform.position; // return to former position on map
            pc.enabled = false;
            gameObject.SetActive(false);
            SceneManager.LoadScene(battleScene);
        }
    }
}