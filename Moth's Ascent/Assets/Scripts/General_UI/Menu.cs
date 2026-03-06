using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Menu : MonoBehaviour
{

    private void Awake()
    {
        SettingsMenu.Instance.SettingsDoneButton();
    }

    // Currently loads to beta scene
    public void OnNewGameButton()
    {
        SceneManager.LoadScene("BetaScene");
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
}