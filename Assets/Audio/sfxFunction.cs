using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sfxFunction : MonoBehaviour
{
    private static sfxFunction Instance;

    private static AudioSource audioSource;
    private static SfxManager sfxManager;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            sfxManager = GetComponent<SfxManager>();
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Play(string soundName)
    {
        AudioClip audioClip = sfxManager.GetRandomClip(soundName);
        if (audioClip != null) {
            audioSource.PlayOneShot(audioClip);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void OnValueChanged()
    {
        SetVolume(sfxSlider.value);
    }
}
