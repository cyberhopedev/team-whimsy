/// <summary>
/// Interface for anything that can be interacted with, including:
///     - NPCs
///     - Items
///     - Puzzles
/// </summary>
public interface IInteractable
{
    void Interact();
    bool CanInteract();
}