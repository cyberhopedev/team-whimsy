using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadMenu : MonoBehaviour
{
    public static LoadMenu Instance;
    SaveData[] slots;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
        
        // Get slot data
        slots = SaveController.Instance.GetAllSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
