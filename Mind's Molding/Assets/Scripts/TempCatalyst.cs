using UnityEngine;
using UnityEngine.UI;

public class TempCatalyst : MonoBehaviour
{
    private Rigidbody2D rb;
    // Reference to puzzle it will reveal
    [SerializeField] GameObject puzzleInterface;
    [SerializeField] GameObject materialPuzzleInterface;
    [SerializeField][TextArea] private string itemName = "";
    private int progressIdx = 0; // 0 = start magic circle, 1 = apply chalk, 2 = win condition, -1 = can't interact
    public static TempCatalyst Instance;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetProgressIdx(int idx)
    {
        progressIdx = idx;
    }

    // Initiate sliding puzzle
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (itemName == "Circle" && progressIdx == 0)
            {
                DialogueManager.Instance.TriggerWhim("Circle", () => puzzleInterface.SetActive(true));
                progressIdx = -1; //can't interact with until ingredients for chalky mixture found
            } 
            else if (itemName == "Circle" && progressIdx == 1)
            {
                // turn to chalk stuff
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/CompleteWChalk");
                materialPuzzleInterface.SetActive(false);
                progressIdx = -1;
                DialogueManager.Instance.TriggerWhim("Book", () => TableTrigger.Instance.SetInteractable(true));
            }
            else if (itemName == "Circle" && progressIdx == 2)
            {
                DialogueManager.Instance.TriggerWhim("Finish");
            }
            else if (itemName == "Circle" && progressIdx == -1)
            {
                return;
            }
            else
            {
                puzzleInterface.SetActive(true); 
            }
        } 
    }
}
