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

[RequireComponent(typeof(PlayerInput))]
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

    private PlayerInput playerInput;

    private Resolution[] resolutions;
    private int DEFAULT_RES_IDX;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
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
                Debug.Log("idx: " + i);
            }
        }
        resolutionDropdown.AddOptions(resOptions);

        DefaultSettings();
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
        // Control sliding options
        if (controlState == 2 && direction == 1)
        {
            controlState = 0;
        } else if (controlState == 0 && direction == -1)
        {
            controlState = 2;
        } else
        {
            controlState += direction;   
        }

        // Switch actual text and controls
        switch (controlState)
        {
            case 0:  // arrow keys
                controlsText.text = "Arrow Keys";
                playerInput.actions.FindActionMap("PlayerArrowKeys").Enable();
                playerInput.actions.FindActionMap("PlayerWASD").Disable();
                break;
            case 1:  // WASD keys
                controlsText.text = "WASD";
                playerInput.actions.FindActionMap("PlayerArrowKeys").Disable();
                playerInput.actions.FindActionMap("PlayerWASD").Enable();
                break;
            case 2:  // both
                controlsText.text = "Both";
                playerInput.actions.FindActionMap("PlayerArrowKeys").Enable();
                playerInput.actions.FindActionMap("PlayerWASD").Enable();
                break; 
        }
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

        // Control Keys
        controlState = 0;
        playerInput.actions.FindActionMap("PlayerArrowKeys").Enable();
        playerInput.actions.FindActionMap("PlayerWASD").Disable();
        controlsText.text = "Arrow Keys";
    }

    public void SettingsDoneButton()
    {
        gameObject.SetActive(false);
    }
}
