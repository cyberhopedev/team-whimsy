using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;
using System;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text genVolText;
    [SerializeField]
    private TMP_Text musicVolText;
    [SerializeField]
    private TMP_Text controlsText;
    [SerializeField]
    private Slider generalSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private TMPro.TMP_Dropdown resolutionDropdown;
    private int controlState;
    
    public AudioMixer audioMixer;

    private Resolution[] resolutions;
    private int DEFAULT_RES_IDX;
    public static SettingsMenu Instance;
    
    // This is a singleton object
    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        // Get the list of available resolutions and fill the dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            Resolution res = resolutions[i];
            resOptions.Add(res.width + " x " + res.height);

            // Set default
            if (res.width == Screen.currentResolution.width && 
                res.height == Screen.currentResolution.height)
            {
                DEFAULT_RES_IDX = i;
            }
        }
        resolutionDropdown.AddOptions(resOptions);

        DefaultSettings();
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
    }

    public void SetGeneralVolume(float genVol)
    {
        // Update text next to slider
        int stringText = (int)math.floor(genVol);
        genVolText.text = stringText.ToString();

        // Update actual volume
        audioMixer.SetFloat("GeneralVolume", genVol - 80); // adjust b/c for some reason audio is [-80,20]
    }

    public void SetMusicVolume(float musicVol)
    {
        // Debug.Log("volume: " + genVol);
        int stringText = (int)math.floor(musicVol);
        musicVolText.text = stringText.ToString();

        // Update actual volume
        audioMixer.SetFloat("MusicVolume", musicVol - 80); 
    }

    public void SwitchControls(int direction)
    {
        // // Control sliding options
        controlState = (controlState + direction + 3) % 3;

        switch (controlState)
        {
            case 0:
                controlsText.text = "Arrow Keys";
                break;
            case 1:
                controlsText.text = "WASD";
                break;
            case 2:
                controlsText.text = "Both";
                break;
        }

        // Tell player controller
        PlayerController.Instance.SetControls(controlState);
    }

    public void SetResolution(int resolutionIdx)
    {
        Resolution res = resolutions[resolutionIdx];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void DefaultSettings()
    {
        // Volume
        SetGeneralVolume(100);
        generalSlider.value = 100;
        SetMusicVolume(80);
        musicSlider.value = 80;

        // Resolution
        resolutionDropdown.value = DEFAULT_RES_IDX;
        resolutionDropdown.RefreshShownValue();

        // // Control Keys - default to both options
        controlState = 2;
        controlsText.text = "Both";
    }

    public void SettingsDoneButton()
    {
        gameObject.SetActive(false);
    }
}
