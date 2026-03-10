using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

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
    int selectedSlotIdx;


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
        selectedSlotIdx = -1;
    }

    // Show function for main menu to call
    public void ShowMenu()
    {
        GetComponent<Canvas>().enabled = true;
        LoadSlots(); // refresh slot data when reopening
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

    public void OnSlotPress(int slotIdx)
    {
        // Do nothing if slot is empty
        if (gameSlots[slotIdx].IsButtonDisabled())
        {
            return;
        }
        
        selectedSlotIdx = slotIdx;
        // Fix slot highlights
        for (int i = 0; i < gameSlots.Length; i++)
        {
            // Highlight slot
            if (i == slotIdx)
            {
                gameSlots[i].ClickSlot();
            } 
            else  // unhighlight other slots
            {
                gameSlots[i].UnclickSlot();
            }
        }
    }

    // Go back to main menu
    public void OnBackButton()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void OnPlayButton()
    {
        // Disable button if no slot selected
        if (selectedSlotIdx == -1)
        {
            return;
        }

        Debug.Log("reaching here");
        string sceneName = SaveController.Instance.LoadGame(selectedSlotIdx);
        if (sceneName != null)
        {
            Debug.Log("in conditional");
            SceneManager.LoadScene(sceneName);
        }
    }

    public void OnEraseButton()
    {
        // Disable button if no slot selected
        if (selectedSlotIdx == -1)
        {
            return;
        }

        SaveController.Instance.DeleteSaveSlot(selectedSlotIdx);
        gameSlots[selectedSlotIdx].ShowEmpty();
    }

}
