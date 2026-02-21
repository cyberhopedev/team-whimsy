using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health
{
    public int health;

    public int max_health;

    public Slider healthBar;

    // Call this before a fight, player can only take max_hits before dying
    protected void Start(int max_hits)
    {
        max_health = max_hits;
        health = max_hits;

        healthBar.maxValue = max_health;
        healthBar.value = health;
    }

    // Called once per frame
    protected void Update()
    {
        
    }

    // Call when player or enemy attacks, stronger hits might be worth more than 1 num_hits
    public void TakeDamage(int num_hits)
    {
        health -= num_hits;
        healthBar.value = health;

        // Die condition
        if (health <= 0)
        {
            // change sprite, destroy object ?
        }
    }

    // Call when character gets healed?
    public void GetHealth(int num_hits)
    {
        // Make sure not to go over max health
        if (health + num_hits >= max_health)
        {
            health = max_health;
        } else
        {
            health += num_hits;
        }
        healthBar.value = health;
    }

    // Call to get health back to 100%
    public void RestoreHealth()
    {
        health = max_health;
        healthBar.value = health;
    }
}