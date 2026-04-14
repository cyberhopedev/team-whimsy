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
    public float typingSpeed = 0.05f;
    public float autoProgressDelay = 1.5f;
}