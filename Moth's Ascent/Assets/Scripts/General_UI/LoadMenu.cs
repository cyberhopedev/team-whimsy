using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class LoadMenu : MonoBehaviour
{
    [SerializeField]
    GameObject slot1;
    [SerializeField]
    GameObject slot2;
    [SerializeField]
    GameObject slot3;
    SaveData[] dataSlots;
    GameSlot[] gameSlots;  // parallel array w/ dataSlots


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Parallel arrays
        GameObject[] slotObjects = new GameObject[3] {slot1, slot2, slot3};
        gameSlots = new GameSlot[3];
        for (int i = 0; i < slotObjects.Length; i++)
        {
            gameSlots[i] = slotObjects[i].GetComponent<GameSlot>();
        }

        // Load in data
        LoadSlots();
    }

    private void LoadSlots()
    {
        // If SaveController can't be found
        if (SaveController.Instance == null)
        {
            Debug.Log("SaveController missing");
            for (int i = 0; i < gameSlots.Length; i++)
            {
                gameSlots[i].ShowEmpty();
            }
            return;
        }

        // Save Controller is found
        dataSlots = SaveController.Instance.GetAllSlots();
        for (int i = 0; i < gameSlots.Length; i++)
        {
            if (dataSlots[i] != null)
            {
                gameSlots[i].FillInSlot(dataSlots[i]);
            } else
            {
                gameSlots[i].ShowEmpty();
            }
        }
    }

    public void onSlotPress(int slotIdx)
    {
        // Do nothing if slot is empty
        if (gameSlots[slotIdx].IsButtonDisabled())
        {
            return;
        }
        // if (dataSlots == null || dataSlots[slotIdx] == null)
        // {
        //     Debug.Log
        // } 
        gameSlots[slotIdx].foundSlot();
    }

}
