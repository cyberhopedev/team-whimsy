using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    [SerializeField] private DialogueNode[] allNodes;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject textBackground;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private GameObject puzzleCanvas;
    private Action onDialogueComplete;
    public bool gameOver = false;
    private int nodeIdx = 0;
    // For skipping through dialogue
    private bool isTyping = false;
    private bool skipRequested = false;

    private void Awake()
    {
        // Singleton instance
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Allow enter key to skip dialogue
        if (isTyping && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            skipRequested = true;
        }
    }

    public void TriggerWhim(string key, Action onComplete = null)
    {
        Debug.Log("whim triggered with key: " + key);
        onDialogueComplete = onComplete;
        dialogueCanvas.layer = 5;
        textBackground.SetActive(true);

        DialogueNode node = System.Array.Find(allNodes, n => n.triggerKey == key);
        if (node == null)
        {
            Debug.LogWarning($"No DialogueNode found for key: '{key}'");
            onComplete?.Invoke(); // Still fire callback so puzzle isn't softlocked
            return;
        }

        StartCoroutine(PlayDialogue(node));
    }

    IEnumerator PlayDialogue(DialogueNode node)
    {
        foreach (string line in node.lines)
        {
            yield return StartCoroutine(TypeLine(line, node.typingSpeed));

            // Allow user to skip through dialogue
            float elapsed = 0f;
            while (elapsed < node.autoProgressDelay && !skipRequested)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame) break;
                elapsed += Time.deltaTime;
                yield return null;
            }
            skipRequested = false;
        }

        // Follow the chain if a next node is assigned
        if (node.nextNode != null)
        {
            yield return StartCoroutine(PlayDialogue(node.nextNode));
        }
        else
        {
            FinishDialogue(node);
        }
    }

    IEnumerator TypeLine(string line, float typingSpeed)
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach (char letter in line)
        {
            if (skipRequested)
            {
                dialogueText.SetText(line); // Instantly complete the line
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        skipRequested = false;
    }

    void FinishDialogue(DialogueNode node)
    {
        // Get dialogue stuff out of the way
        textBackground.SetActive(false);
        dialogueText.text = "";
        dialogueCanvas.layer = 5;
        // Go back to gameplay
        puzzleCanvas.SetActive(true);
        if (!gameOver)
        {
            InfectionMeter.Instance.PauseInfection(false);   
        }

        onDialogueComplete?.Invoke();
        onDialogueComplete = null;
    }
}