using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioVolumeController : MonoBehaviour
{
    public AudioMixer masterMixer;

    public Slider musicSlider;
    public Slider voicesSlider;


    public string audioVoicesMixerGroup;
    public string audioMusicMixerGroup;

    private DontDestroyAudioSettings dontDestroyAudioSettings;


    private void Start()
    {
        dontDestroyAudioSettings = FindObjectOfType<DontDestroyAudioSettings>();

        musicSlider.value = 0f;
        voicesSlider.value = 0f;
    }

    private void Update()
    {
        /*if (musicSlider.value > 0.01f)
            dontDestroyAudioSettings.musicMuted = false;
        if (voicesSlider.value > 0.01f)
            dontDestroyAudioSettings.voicesMuted = false;

        if(dontDestroyAudioSettings.musicMuted)
            masterMixer.SetFloat("Voices", 0.001f);
        if (dontDestroyAudioSettings.musicMuted)
            masterMixer.SetFloat("Music", 0.001f);*/
    }

    public void SetVoicesSound()
    {
        masterMixer.SetFloat("Voices", Mathf.Log10(voicesSlider.value) * 10f);
        dontDestroyAudioSettings.voicesVolume = Mathf.Log10(voicesSlider.value) * 10f;

        UnityEngine.Analytics.Analytics.CustomEvent("Volumen modificado", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        },
                        {
                            "Voces modificadas",  dontDestroyAudioSettings.voicesVolume
                        }
                    });

        UnityEngine.Analytics.Analytics.FlushEvents();
    }

    public void SetMusicSound()
    {
        masterMixer.SetFloat("Music", Mathf.Log10(musicSlider.value) * 10f);
        dontDestroyAudioSettings.musicVolume = Mathf.Log10(musicSlider.value) * 10f;

        UnityEngine.Analytics.Analytics.CustomEvent("Volumen modificado", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        },
                        {
                            "Musica modificada",  dontDestroyAudioSettings.musicVolume
                        }
                    });

        UnityEngine.Analytics.Analytics.FlushEvents();

    }
}