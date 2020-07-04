using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DontDestroyAudioSettings : MonoBehaviour
{
    
    public bool musicMuted;
    
    public bool voicesMuted;

    
    public float voicesVolume;
    
    public float musicVolume;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        musicMuted = false;
        voicesMuted = false;
    }


}
