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
    // Pop up error if you try to start a new game with no more save slots
    [SerializeField]
    GameObject noMoreRoom;

    [SerializeField]
    TMPro.TMP_InputField nameInput;

    private void Start()
    {
        SettingsMenu.Instance.SettingsDoneButton();

        // Hide stuff
        setGameName.SetActive(false);
        noMoreRoom.SetActive(false);
    }

    // Currently loads to beta scene
    public void OnNewGameButton()
    {
        int slot = SaveController.Instance.GetFirstEmptySlot();
        if (slot == -1)
        {
            noMoreRoom.SetActive(true);
            return;
        }
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

    // Save name of game and start next scene, assumes New Game button
    // has already ensured there is an empty slot available
    public void OnSaveButton()
    {
        if (nameInput.text.Length > 0)
        {
            int slot = SaveController.Instance.GetFirstEmptySlot();
            SaveController.Instance.NewGame(slot, nameInput.text);
            SceneManager.LoadScene("BetaScene");
        }
    }

    // Close button for erro pop-up message
    public void OnClosebutton()
    {
        noMoreRoom.SetActive(false);
    }
}