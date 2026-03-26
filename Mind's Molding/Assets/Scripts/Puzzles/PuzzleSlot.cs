using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class PuzzleSlot : MonoBehaviour
{
    public int slotIndex;
    public PuzzlePiece occupant;

    public void OnDrop(PointerEventData eventData)
    {
        // The thing being dragged
        PuzzlePiece incoming = eventData.pointerDrag?.GetComponent<PuzzlePiece>();
        if(incoming == null)
        {
            return;
        }

        incoming.LandOnSlot(this);
    }
}