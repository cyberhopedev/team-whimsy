using UnityEngine;

public class TableTrigger : MonoBehaviour
{
    private Rigidbody2D rb;
    // Reference to puzzle it will reveal
    [SerializeField] GameObject puzzleInterface;
    private bool canInteract = false;
    public static TableTrigger Instance;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetInteractable(bool canInteract)
    {
        this.canInteract = canInteract;
    }

    // Initiate sliding puzzle
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (canInteract && collision.gameObject.CompareTag("Player"))
        {
            puzzleInterface.SetActive(true);
        } 
    }

    public void OnCloseButton()
    {
        puzzleInterface.SetActive(false);
    }
}
