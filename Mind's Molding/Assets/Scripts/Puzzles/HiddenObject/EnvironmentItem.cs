using UnityEngine;

public class EnvironmentItem : MonoBehaviour
{
    private Rigidbody2D rb;
    // Reference to puzzle it will reveal
    [SerializeField] MaterialsPuzzle puzzleInterface;
    [SerializeField] ItemData item;
    private bool canGrab = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void AllowGrab()
    {
        canGrab = true;
    }

    // Initiate sliding puzzle
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (canGrab && collision.gameObject.CompareTag("Player"))
        {
           puzzleInterface.AddItem(item);
           gameObject.SetActive(false);
        } 
    }
}
