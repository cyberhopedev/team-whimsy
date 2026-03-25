using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Represents each UI Image inside the Magic Circle puzzle grid and handles all the drag-and-drop logic.
/// Implements three Unity interfaces that the Event System calls automatically.
/// </summary>
public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
{
    public void Awake()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void LandOnSlot(PuzzleSlot targetSlot)
    {
        
    }

    private void SwapWith(PuzzlePiece other, PuzzleSlot targetSlot)
    {
        
    }

    public void MoveToSlot(PuzzleSlot slot)
    {
        
    }
}