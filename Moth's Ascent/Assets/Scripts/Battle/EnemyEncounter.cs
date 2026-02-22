using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary> 
/// Triggers the battle system by calling this class when the PlayerController
/// collides with an Enemy in the crawler
/// </summary>
public class EnemyEncounter : MonoBehaviour
{
    // Hold a list of enemies that need to spawn
    public List<GameObject> encounterEnemies;

    private void OnTriggerEnter(Collider c)
    {
        // If the player collides with an enemy, disable free movement and enable the battle system
        if (c.TryGetComponent(out PlayerController pc))
        {
            pc.enabled = false;
            pc.GetComponent<PlayerBattler>().enabled = true;
            BattleSystem.Instance.StartBattle(encounterEnemies);

            // Also, make sure to the hide the tilemap we have in the overworld camera
            gameObject.SetActive(false);
        }
    }

    // Test function for backend, ensuring that colliders work and that battle starts
    private void OnTriggerEnter2D(Collider2D c)
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