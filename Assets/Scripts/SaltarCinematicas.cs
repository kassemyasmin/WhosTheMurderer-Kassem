using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class SaltarCinematicas : MonoBehaviour
{
    
    public string siguienteEscena;
    
    GameObject principalCamera;
    TimerVideos timerVideo;

    public GameObject skipCanvas;

    private LevelManager lm;
    private MeGustoNoMeGusto mg;
    public UnityEngine.Video.VideoPlayer movTexture;

    public VideoClip clipMovie;
    public VideoClip secondClip;

    bool second=false;

    bool skipped = false;
    bool startedVideo = false;
    bool finishedVideo = false;

    // Use this for initialization
    void Start()
    {
        principalCamera = FindObjectOfType<Camera>().gameObject;
        lm = FindObjectOfType<LevelManager>();
        mg = FindObjectOfType<MeGustoNoMeGusto>();
        timerVideo = GetComponent<TimerVideos>();  
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible=true;
		StartCoroutine(ShowVideos());
        movTexture.loopPointReached += OnLoopPointReached;
    }


    private void Update()
    {

        if (movTexture.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                skipped = true;

                movTexture.Stop();

                if (lm.FirstLoad)
                {

                    UnityEngine.Analytics.Analytics.CustomEvent("NoCinematicasHastaFinal", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name 
                        }
                    });

                    UnityEngine.Analytics.Analytics.FlushEvents();
                }

                if (!second && secondClip != null)
                    StartSecondClip();
                else
                    Continue();
            }
        }
        /*
        if (startedVideo && timerVideo.temporizadorTimer <=  2 && !finishedVideo)
        {
            if (!skipped && lm.FirstLoad)
            {
                gAna.gv4.LogEvent(new EventHitBuilder()
                .SetEventCategory("CinematicasHastaFinal")
                .SetEventAction(SceneManager.GetActiveScene().name));
                gAna.gv4.DispatchHits();
                finishedVideo = true;
            }

            foreach (var audioSource in gameObject.GetComponents<AudioSource>())
            {
                audioSource.Stop();
            }
        }

        else if (timerVideo.temporizadorTimer <= 1)
        {

            if (mg != null)
            {
                mg.Mostrar();
                skipCanvas.SetActive(false);
            }

            Destroy(gameObject);
        }
        */
    }

    private void StartSecondClip()
    {
        second = true;
        StartVideo(secondClip);
    }


    private void OnLoopPointReached(UnityEngine.Video.VideoPlayer _source)
    {
        movTexture.Stop();

        UnityEngine.Analytics.Analytics.CustomEvent("CinematicasHastaFinal", new Dictionary<string, object>
                    { 
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        }
                    });

        UnityEngine.Analytics.Analytics.FlushEvents();


        if (!second && secondClip != null)
            StartSecondClip();
        else
            Continue();
    }

    private void Continue()
    {
        finishedVideo = true;

        if (siguienteEscena != null && siguienteEscena != "")
        {
            SceneManager.LoadScene(siguienteEscena);
        }

        else
        {
            mg.Mostrar();
            skipCanvas.SetActive(false);
        }
    }

    private void StartVideo(VideoClip clip)
    {
        movTexture.clip = clip;
        movTexture.Play();
        startedVideo = true;
    }

    private IEnumerator ShowVideos()
    {
        movTexture = principalCamera.GetComponent<UnityEngine.Video.VideoPlayer>();
        StartVideo(clipMovie);
        
        yield break;
    }
}
