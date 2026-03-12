using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject hover1;
    [SerializeField]
    GameObject hover3;
    [SerializeField]
    GameObject hover4;
    [SerializeField]
    GameObject exitConfirmation;
    GameObject[] hoverBgs;

    // The save slot currently in
    [SerializeField] private int currentSaveSlot = 0; // change in Inspector per save slot UI later

    //Singleton
    public static PauseMenu Instance;

    private void Awake()
    {
        // Singleton instance
        if (Instance == null)
        {
            Instance = this;
            gameObject.SetActive(false); // hide menu
        } else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
{
    if (Instance == this)
        Instance = null;
}

    // Doesn't Start until escape key pressed (code in InventoryManager)
    void Start()
    {
        // Hover buttons list
        hoverBgs = new GameObject[] {hover1, null, hover3, hover4}; // removed 2nd button
        
        // Hide stuff
        SettingsMenu.Instance.SettingsDoneButton();
        exitConfirmation.SetActive(false);
    }

    // Needs to update PlayerController so esc button updates
    public void OnContinueButton()
    {
        PlayerController.Instance.ClosePauseMenu(); 
    }

    /// <summary>
    /// When the player clicks the save button, show all of the slots, allow them
    /// to hover over them, when clicked on the slot the game saves to that slot
    /// and updates
    /// </summary>
    public void OnSaveButton()
    {
        // Show the LoadMenu
        LoadMenu.Instance.ShowMenu();
    }

    public void OnSettingsButton()
    {
        SettingsMenu.Instance.ShowMenu();
    }

    public void OnExitButton()
    {
        exitConfirmation.SetActive(true);
    }

    public void OnExitConfirmation()
    {
        SceneManager.LoadScene(0);  // go back to menu screen
        SettingsMenu.Instance.SettingsDoneButton();
        exitConfirmation.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OnExitRejection()
    {
        exitConfirmation.SetActive(false);
    }

    // Hover background
    public void OnStartHover(int buttonIdx)
    {
        hoverBgs[buttonIdx].SetActive(true);
    }

    public void OnEndHover(int buttonIdx)
    {
        hoverBgs = new GameObject[] {hover1, null, hover3, hover4};
        hoverBgs[buttonIdx].SetActive(false);
    }

    // Remove all hovers
    public void OnEnable()
    {
        for (int i = 0; i < 4; i++)
        {
            OnEndHover(i);
        }
    }
}