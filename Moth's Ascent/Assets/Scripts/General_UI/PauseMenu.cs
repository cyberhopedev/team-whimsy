using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject hover1;
    [SerializeField]
    GameObject hover2;
    [SerializeField]
    GameObject hover3;
    [SerializeField]
    GameObject hover4;
    [SerializeField]
    GameObject exitConfirmation;
    GameObject[] hoverBgs;
    //Singleton
    public static PauseMenu Instance;

    void Start()
    {
        // Singleton instance
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        // Hover buttons list
        hoverBgs = new GameObject[] {hover1, hover2, hover3, hover4};
        
        // Hide stuff
        SettingsMenu.Instance.SettingsDoneButton();
        exitConfirmation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnContinueButton()
    {
        gameObject.SetActive(false);
    }

    public void OnSaveButton()
    {
        
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
        hoverBgs[buttonIdx].SetActive(false);
    }
}
