using UnityEngine;

public enum WordSlot { Subject, Effect, Target }

[CreateAssetMenu(menuName = "MindsMolding/WordData")]
public class WordData : ScriptableObject
{
    public Sprite[] runeIcons;       // rune symbols (originally magic word)
    public string translation;     // ex. "Fungus"
    public WordSlot slot;          // which column it belongs to
}