using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text genVolText;
    [SerializeField]
    private TMP_Text musicVolText;
    
    public AudioMixer audioMixer;

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
}
