using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

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
    Analytics gAna;
    ControladorCamara controladorCamara;

    // Use this for initialization
    void Start()
    {
        StartTimer();
        textoTimer = GetComponentInChildren<Text>();
        gAna = FindObjectOfType<Analytics>();
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

                gAna.gv4.LogEvent(new EventHitBuilder()
                  .SetEventCategory("Perder")
                  .SetEventAction(SceneManager.GetActiveScene().name)
                  .SetEventLabel("SinTiempo"));
                gAna.gv4.DispatchHits();
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