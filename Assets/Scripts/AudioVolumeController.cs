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

    private Analytics gAna;

    private void Start()
    {
        dontDestroyAudioSettings = FindObjectOfType<DontDestroyAudioSettings>();
        gAna = FindObjectOfType<Analytics>();

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

        gAna.gv4.LogEvent(new EventHitBuilder()
                   .SetEventCategory("Volumen modificado")
                   .SetEventAction(SceneManager.GetActiveScene().name)
                   .SetEventLabel("Voces modificadas"));
        gAna.gv4.DispatchHits();

    }

    public void SetMusicSound()
    {
        masterMixer.SetFloat("Music", Mathf.Log10(musicSlider.value) * 10f);
        dontDestroyAudioSettings.musicVolume = Mathf.Log10(musicSlider.value) * 10f;
        gAna.gv4.LogEvent(new EventHitBuilder()
                  .SetEventCategory("Volumen modificado")
                  .SetEventAction(SceneManager.GetActiveScene().name)
                  .SetEventLabel("Musica modificadas"));
        gAna.gv4.DispatchHits();
    }
}