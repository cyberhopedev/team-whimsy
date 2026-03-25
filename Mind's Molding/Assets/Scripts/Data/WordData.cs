using UnityEngine;

public enum WordSlot { Subject, Effect, Target }

[CreateAssetMenu(menuName = "MindsMolding/WordData")]
public class WordData : ScriptableObject
{
    public string magicWord;       // ex. "Vethara"
    public string translation;     // ex. "Fungus"
    public WordSlot slot;          // which column it belongs to
}