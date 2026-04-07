using UnityEngine;
using UnityEngine.UI;

public class TempCatalyst : MonoBehaviour
{
    private Rigidbody2D rb;
    // Reference to puzzle it will reveal
    [SerializeField] GameObject puzzleInterface;
    [SerializeField][TextArea] private string itemName = "";
    private int progressIdx = 0; // 0 = start magic circle, 1 = apply chalk, -1 = can't interact

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
