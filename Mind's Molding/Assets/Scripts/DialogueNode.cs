using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "MindsMolding/DialogueNode")]
public class DialogueNode : ScriptableObject
{
    public string speakerName;
    [TextArea] public string[] lines;
    public string triggerKey;
    public DialogueNode nextNode;
    public UnityEvent onComplete;
}