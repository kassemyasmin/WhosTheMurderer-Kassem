using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ToggleMute : MonoBehaviour
{    
    public Sprite mutedSprite;
    public Sprite unmutedSprite;
    public AudioMixerGroup audioMixer;
    public Image btnMute;

    //private bool voicesMuted;
    //private bool musicMuted;
    public string audioGroup;

    DontDestroyAudioSettings audioData;

    private Analytics gAna;

    private void Start()
    {
        audioData = FindObjectOfType<DontDestroyAudioSettings>();
       /* voicesMuted = audioData.voicesMuted;
        musicMuted = audioData.musicMuted;*/
        gAna = FindObjectOfType<Analytics>();
    }


    public void Toggle()
    {
        if(audioMixer.name == "Voices")
        {
            if (audioData.voicesMuted)
            {
                Unmute(audioGroup);
            }
            else
                Mute(audioGroup);
        }

        if (audioMixer.name == "MusicAndSFX")
        {
            if (audioData.musicMuted)
            {
                Unmute(audioGroup);
            }
            else
                Mute(audioGroup);
        }

    }

    public void Mute(string mixGroup)
    {
        audioMixer.audioMixer.SetFloat(mixGroup, -80f);
        btnMute.sprite = mutedSprite;
        if(mixGroup == "Voices")
        {
            audioData.voicesMuted = true;
            gAna.gv4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("Mute")
                    .SetEventAction(SceneManager.GetActiveScene().name)
                    .SetEventLabel("Voces Muteadas"));
            gAna.gv4.DispatchHits();
        }
        if (mixGroup == "Music")
        {
            audioData.musicMuted = true;
            gAna.gv4.LogEvent(new EventHitBuilder()
                   .SetEventCategory("Mute")
                   .SetEventAction(SceneManager.GetActiveScene().name)
                   .SetEventLabel("Musica Muteada"));
            gAna.gv4.DispatchHits();
        }
            
    }

    public void Unmute(string mixGroup)
    {
        audioMixer.audioMixer.SetFloat(mixGroup, 0f);
        btnMute.sprite = unmutedSprite;
        if (mixGroup == "Voices")
        {
            audioData.voicesMuted = false;
            gAna.gv4.LogEvent(new EventHitBuilder()
                   .SetEventCategory("Unmuted")
                   .SetEventAction(SceneManager.GetActiveScene().name)
                   .SetEventLabel("Voces desmuteadas"));
            gAna.gv4.DispatchHits();        }
            
        if (mixGroup == "Music")
        {
            audioData.musicMuted = false;
            gAna.gv4.LogEvent(new EventHitBuilder()
                   .SetEventCategory("Unmuted")
                   .SetEventAction(SceneManager.GetActiveScene().name)
                   .SetEventLabel("Musica desmuteada"));
            gAna.gv4.DispatchHits();
        }
            
    }
}
