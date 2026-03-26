using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    [SerializeField] private DialogueNode[] allNodes;
    [SerializeField] private TextMeshProUGUI dialogueText;

    public void TriggerWhim(string key)
    {
        
    }

    IEnumerator PlayDialogue(DialogueNode node)
    {
        return null;
    }

}