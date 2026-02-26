using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    /// <summary> 
    /// Based on the given collider, if the player triggers it then switch to PlayerBattler
    /// and start the battle with the encountered overworld enemies
    /// </summary>
    /// <param name="c">The collider the overworld player triggers</param>
    private void OnTriggerEnter2D(Collider2D c)
    {
        // If the player collides with an enemy, disable free movement and enable the battle system
        if(c.TryGetComponent(out PlayerController pc) && !_triggered)
        {   
            _triggered = true;
            // Store the enemies before switching scenes
            BattleTransitionData.EncounterEnemies = encounterEnemies;
            // Disables player movement
            pc.enabled = false;
            SceneManager.LoadScene(battleScene);
        }
    }

    // Test function for backend, ensuring that colliders work and that battle starts
    private void OnTriggerEnter2DTest(Collider2D c)
    {
        Debug.Log($"Trigger entered by: {c.gameObject.name}");
        
        if (c.TryGetComponent(out PlayerController pc))
        {
            Debug.Log("Player detected, starting battle!");
            pc.enabled = false;
            pc.GetComponent<PlayerBattler>().enabled = true;
            BattleSystem.Instance.StartBattle(encounterEnemies);
            gameObject.SetActive(false);
        }
    }
}