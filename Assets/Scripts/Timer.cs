using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Timer:MonoBehaviour {

    [SerializeField]
    string VideoPerder;

    [SerializeField]
    float Tiempo;

    public float tiempoRestante { get; private set;}
    bool timerStarted = false;
    Text textoTimer;
    [SerializeField]
    float tipTime = 30;
    public string TiempoRestante { get; private set; }
    ControladorCamara controladorCamara;

    // Use this for initialization
    void Start()
    {
        StartTimer();
        textoTimer = GetComponentInChildren<Text>();
        controladorCamara = FindObjectOfType<ControladorCamara>();
    }
    public void StartTimer()
    {
        tiempoRestante = Tiempo;
        timerStarted = true;
        this.gameObject.SetActive(true);
    }

    public void Substract()
    {
        tiempoRestante -= tipTime;
    }

    public void StopTimer()
    {
        timerStarted = false;
        this.gameObject.SetActive(false);

    }

    public void PauseTimer(bool pausar)
    {
        if (pausar)
            timerStarted = false;
        else
            timerStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted == true)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0)
            {
                //finalPerdiste.Mostrar();
                SceneManager.LoadScene(VideoPerder);
                StopTimer();
                controladorCamara.Reset();

                UnityEngine.Analytics.Analytics.CustomEvent("Perder", new Dictionary<string, object>
                        {
                            {
                                "Scene", SceneManager.GetActiveScene().name
                            },
                             {
                                "Tiempo restante",0
                            }
                        });

                UnityEngine.Analytics.Analytics.FlushEvents();
            }
            ActualizarTexto();
        }
    }


    private void ActualizarTexto()
    {
        int minutes = Mathf.RoundToInt(Mathf.Floor(tiempoRestante / 60));
        int seconds = Mathf.RoundToInt(tiempoRestante % 60);

        var minutesText = Convert.ToString(minutes);
        if (minutesText.Length == 1) minutesText = "0" + minutesText;
        var secondsText = Convert.ToString(seconds);
        if (secondsText.Length == 1) secondsText = "0" + secondsText;
        TiempoRestante = minutesText + ":" + secondsText;

        textoTimer.text = TiempoRestante;
    }


}