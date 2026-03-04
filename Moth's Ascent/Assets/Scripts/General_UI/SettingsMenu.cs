using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text genVolText;
    [SerializeField]
    private TMP_Text musicVolText;
    [SerializeField]
    private TMP_Text controlsText;
    private int controlState;
    
    public AudioMixer audioMixer;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        controlState = 0;
        playerInput.actions.FindActionMap("PlayerArrowKeys").Enable();
        playerInput.actions.FindActionMap("PlayerWASD").Disable();
    }

    public void SetGeneralVolume(float genVol)
    {
        // Update text next to slider
        int stringText = (int)math.floor(genVol);
        genVolText.text = stringText.ToString();

        // Update actual volume
        audioMixer.SetFloat("GeneralVolume", genVol - 80); // for some reason audio is [-80,20]
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
}
