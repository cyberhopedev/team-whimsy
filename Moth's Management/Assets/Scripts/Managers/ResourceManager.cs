using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResourceManager : MonoBehaviour
{
    // Starting amounts
    [Header("Starting Amounts")]
    [SerializeField] private int chalkInit;
    [SerializeField] private int magicInt;
    // Track current amounts
    private int amtChalk;
    private int amtMagic;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Public method to increase chalk supply
    public void AddChalk(int amt)
    {
        amtChalk += amt;
    }

    // Public method to increase chalk supply
    public void AddMagic(int amt)
    {
        amtMagic += amt;
    }
}
