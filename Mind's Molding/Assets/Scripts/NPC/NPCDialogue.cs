using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(filename = "NewNPCDialogue", menuName ="NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
    // Possibly add sound effect for speaking ? (Animal Crossing/Celeste esque?)
    // public AudioClip voiceSound;
    // public float voicePitch = 1f;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
}