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
}
