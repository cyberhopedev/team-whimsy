using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents an interactable object in the scene that can be engaged with by the player.
/// Manages item data, interaction range, and triggers interaction events when the player is within range.
/// </summary>
public class Interactable : MonoBehaviour
{
    /// The item data associated with this interactable object.
    [SerializeField] public ItemData itemData;

    /// The maximum distance from the player at which this object can be interacted with.
    [SerializeField] private float interactRange = 1.5f;

    /// Unity event invoked when the player successfully interacts with this object.
    public UnityEvent onInteract;

    /// <summary>
    /// Updates the interactable state each frame.
    /// Checks if the player is within interaction range and handles input detection.
    /// </summary>
    public void Update()
    {
        // If the player is within range, press the "E" key to pickup
        var player = FindObjectOfType<PlayerController>();
        if(Vector2.Distance(transform.position, player.transform.position) < interactRange
            && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    /// <summary>
    /// Executes the interaction with this object.
    /// Invokes the onInteract event and applies the effects of interacting with the associated item.
    /// </summary>
    public void Interact()
    {
        onInteract?.Invoke();
        FindObjectOfType<MaterialsPuzzle>().AddItem(itemData);
        gameObject.SetActive(false);
    }
}