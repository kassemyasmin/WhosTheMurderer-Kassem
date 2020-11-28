using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
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


    private void Start()
    {
        audioData = FindObjectOfType<DontDestroyAudioSettings>();
       /* voicesMuted = audioData.voicesMuted;
        musicMuted = audioData.musicMuted;*/
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

            UnityEngine.Analytics.Analytics.CustomEvent("Mute", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        },
                                                {
                            "Voces Muteadas",true
                        }
                    });

            UnityEngine.Analytics.Analytics.FlushEvents();
        }
        if (mixGroup == "Music")
        {
            audioData.musicMuted = true;

            UnityEngine.Analytics.Analytics.CustomEvent("Mute", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        },
                                                {
                            "Musica Muteada",true
                        }
                    });

            UnityEngine.Analytics.Analytics.FlushEvents();
        }
            
    }

    public void Unmute(string mixGroup)
    {
        audioMixer.audioMixer.SetFloat(mixGroup, 0f);
        btnMute.sprite = unmutedSprite;
        if (mixGroup == "Voices")
        {
            audioData.voicesMuted = false;

            UnityEngine.Analytics.Analytics.CustomEvent("Unmuted", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        },
                                                {
                            "Voces desmuteadas",true
                        }
                    });

            UnityEngine.Analytics.Analytics.FlushEvents();
        }
        //cosas raras nuevas
        if (mixGroup == "Music")
        {
            UnityEngine.Analytics.AnalyticsEvent.CustomEvent(new Dictionary<string, object> { { "music Unmuted", true } });
            audioData.musicMuted = false;

                UnityEngine.Analytics.Analytics.CustomEvent("Unmuted", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        },
                        {
                            "Musica desmuteada",true
                        }
                    });

                UnityEngine.Analytics.Analytics.FlushEvents();
        }
        
    }
}
