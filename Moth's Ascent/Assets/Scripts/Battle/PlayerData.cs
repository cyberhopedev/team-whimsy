using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Battle/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int currentHP;
    public int maxHP = 100;
    public int attackDamage = 10;
    public int speedStat = 5;

    public void ResetHP() => currentHP = maxHP;
}