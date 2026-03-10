using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class Menu : MonoBehaviour
{
    // Pop up to set name of new game you're starting
    [SerializeField]
    GameObject setGameName;

    [SerializeField]
    TMPro.TMP_InputField nameInput;

    private void Start()
    {
        SettingsMenu.Instance.SettingsDoneButton();
        setGameName.SetActive(false);
    }

    // Currently loads to beta scene
    public void OnNewGameButton()
    {
        setGameName.SetActive(true);
    }

    // Show settings menu
    public void OnSettingsButton()
    {
        SettingsMenu.Instance.ShowMenu();
    }

    // Exits application when fully built and running
    public void OnExitButton ()
    {
        Application.Quit();
    }

    // Save name of game and start next scene
    public void OnSaveButton()
    {
        if (nameInput.text.Length > 0)
        {
            int slot = SaveController.Instance.GetFirstEmptySlot();
            if (slot == -1)
            {
                Debug.LogWarning("No empty save slots available");
                return;
            }

            SaveController.Instance.NewGame(slot, nameInput.text);
            SceneManager.LoadScene("BetaScene");
        }
    }
}