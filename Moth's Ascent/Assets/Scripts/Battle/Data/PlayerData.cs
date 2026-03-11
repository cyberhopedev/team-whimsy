using UnityEngine;
using System.Collections.Generic;

/// <summary> 
/// Holds the data between the overworld and battle scene transition
/// </summary>
[CreateAssetMenu(fileName = "PlayerData", menuName = "Battle/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int currentHP;
    public int maxHP = 100;
    public int attackDamage = 10;
    public int speedStat = 5;

    /// Known abilities and attacks are used to populate the battle options, so they need to be stored here
    public List<Attack> knownAttacks = new List<Attack>()
    {
        // No matter what, start with struggle by default
        Attack.STRUGGLE
    };
    public List<Ability> knownAbilities = new List<Ability>();

    public void ResetHP() => currentHP = maxHP;

    public void LearnAttack(Attack newAttack)
    {
        if (!knownAttacks.Contains(newAttack))
        {
            knownAttacks.Add(newAttack);
            Debug.Log("Learned attack: " + newAttack.GetName());
        }
        else
        {
            Debug.Log(newAttack.GetName() + " is already known.");
        }
    }

    public void LearnAbility(Ability newAbility)
    {
        if (!knownAbilities.Contains(newAbility))
        {
            knownAbilities.Add(newAbility);
            Debug.Log("Learned ability: " + newAbility.GetName());
        }
        else
        {
            Debug.Log(newAbility.GetName() + " is already known.");
        }
    }
}