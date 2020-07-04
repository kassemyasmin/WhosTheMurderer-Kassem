using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayer : MonoBehaviour
{

    GameObject principalCamera;
    public VideoClip clip;


    private void Awake()
    {
        principalCamera = GameObject.Find("Main Camera");
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayVideo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayVideo()
    {
        var videoPlayer = principalCamera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.isLooping = false;
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }
}
