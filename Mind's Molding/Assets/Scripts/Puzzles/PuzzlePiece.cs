using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Represents each UI Image inside the Magic Circle puzzle grid and handles all the drag-and-drop logic.
/// Implements three Unity interfaces that the Event System calls automatically.
/// </summary>
public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    /// <summary>
    ///  Inspector data
    /// </summary>
    [Header("Puzzle Data")]
    public int correctSlotIndex;
    public int CurrentSlotIndex {get; private set;}
    [Header("References")]
    [SerializeField] private MagicCirclePuzzle originPuzzle;

    // The slot this piece currently lives in
    private Transform parentSlot;
    // A top-level Canvas transform for drag rendering
    private Transform dragContainer;
    private CanvasGroup canvasGroup;
    private Canvas rootCanvas;

    /// <summary>
    /// 
    /// </summary>
    public void Awake()
    {
        // Get the canvas'
        canvasGroup = GetComponent<CanvasGroup>();
        rootCanvas = GetComponentInParent<Canvas>();

        // The piece starts in whatever slot it's parented to
        parentSlot       = transform.parent;
        CurrentSlotIndex = parentSlot.GetComponent<PuzzleSlot>().slotIndex;

        // Register as the occupant of that slot
        parentSlot.GetComponent<PuzzleSlot>().occupant = this;

        // Find or create a drag container at the root of the Canvas
        dragContainer = rootCanvas.transform.Find("DragContainer");
        if (dragContainer == null)
        {
            var go = new GameObject("DragContainer");
            dragContainer = go.transform;
            dragContainer.SetParent(rootCanvas.transform, false);
        }
    }

    /// <summary>
    /// Implements IBeginDragHandler
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Move to drag container so it renders on top of everything
        transform.SetParent(dragContainer);
        transform.SetAsLastSibling();

        // Disable raycasting so the drop event reaches the slot beneath
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Implements IDragHandler
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        // Follow the pointer in Canvas space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rootCanvas.transform as RectTransform,
            eventData.position,
            rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
            out Vector2 localPos);

        (transform as RectTransform).localPosition = localPos;
    }

    /// <summary>
    /// Called by PuzzleSlot when something is dropped onto it
    /// </summary>
    /// <param name="targetSlot"></param>
    public void LandOnSlot(PuzzleSlot targetSlot)
    {
        PuzzlePiece occupant = targetSlot.occupant;

        if(occupant != null && occupant != this)
        {
            SwapWith(occupant, targetSlot);
        }
        else
        {
            MoveToSlot(targetSlot);
        }

        canvasGroup.blocksRaycasts = true;
        originPuzzle.OnPieceSwapped();
    }

    private void SwapWith(PuzzlePiece other, PuzzleSlot targetSlot)
    {
        PuzzleSlot myOldSlot = parentSlot.GetComponent<PuzzleSlot>();

        // Move the other piece to my old slot
        other.MoveToSlot(myOldSlot);

        // Move myself to the target slot
        MoveToSlot(targetSlot);
    }

    /// <summary>
    /// Moves the puzzle piece   
    /// </summary>
    /// <param name="slot"></param>
    public void MoveToSlot(PuzzleSlot slot)
    {
        // Clear old slot occupant
        if(parentSlot != null)
        {
            parentSlot.GetComponent<PuzzleSlot>().occupant = null;
        }

        // Reparent and snap to slot's position
        transform.SetParent(slot.transform);
        (transform as RectTransform).localPosition = Vector2.zero;
        (transform as RectTransform).localScale     = Vector3.one;

        // Update state
        parentSlot       = slot.transform;
        CurrentSlotIndex = slot.slotIndex;
        slot.occupant    = this;
    }
}