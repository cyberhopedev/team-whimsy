using UnityEngine;

public class TempCatalyst : MonoBehaviour
{
    private Rigidbody2D rb;
    // Reference to puzzle it will reveal
    [SerializeField] GameObject puzzleInterface;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Initiate sliding puzzle
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           puzzleInterface.SetActive(true);
        } 
    }
}
