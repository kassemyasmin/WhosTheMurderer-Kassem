using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ControladorHipotesis : MonoBehaviour
{
    [SerializeField]
    int minimoPistasEnEvidencia = 3;

    [SerializeField]
    string VideoGanar;

    [SerializeField]
    string VideoPerder;

       //   private CanvasGanaste finalGano;
    //   private CanvasPerdiste finalPierdo;
    private CanvasCasiPerdiste finalCasiPierdo;
    private Text oportunidadesText;
    private ControladorTutorial controladorTutorial;
    private FraseHipotesis fraseHipotesis;
    private CanvasComprobandoTeoria comprobandoTeoria;
    private Persister persister;

    private bool comprobandoHipotesis; 

    private Pista arma;
    private Motivo motivo;
    private Sospechoso sospechosoSeleccionado;
    private Pista[] evidencias;
    private Analytics gAna;
    private Timer timer;
    ControladorCamara controladorCamara;

    private int contador = 3;
    
    public int MinimoPistasEnEvidencia { get { return minimoPistasEnEvidencia; } }

    // Use this for initialization
    void Start()
    {

 //       finalGano = FindObjectOfType<CanvasGanaste>();
 //       finalPierdo = FindObjectOfType<CanvasPerdiste>();
        finalCasiPierdo = FindObjectOfType<CanvasCasiPerdiste>();
//        sospechoso = FindObjectOfType<SelectorSospechoso>();
        controladorTutorial = FindObjectOfType<ControladorTutorial>();
        fraseHipotesis = FindObjectOfType<FraseHipotesis>();

        oportunidadesText = finalCasiPierdo.GetComponentInChildren<Text>();
        evidencias = new Pista[5];
        comprobandoTeoria = FindObjectOfType<CanvasComprobandoTeoria>();
        persister = FindObjectOfType<Persister>();
        gAna = FindObjectOfType<Analytics>();
        timer = FindObjectOfType<Timer>();
        controladorCamara = FindObjectOfType<ControladorCamara>();
    }

    // Update is called once per frame
    void Update()
    {
        if (comprobandoHipotesis && !comprobandoTeoria.Activo)
        {
            CompletarVerificacion();
            comprobandoHipotesis = false;
        }
    }

    public IEnumerable<Pista> Evidencias { get { return evidencias; } }

    public Pista Arma
    {
        get { return arma; }
        set
        {
            arma = value;
            ActualizarFrase();
        }
    }
    public Motivo Motivo
    {
        get { return motivo; }
        set
        {
            motivo = value;
            ActualizarFrase();
        }
    }

    public  Sospechoso SospechosoSeleccionado
    {
        get { return sospechosoSeleccionado; }
        set
        {
            sospechosoSeleccionado = value;
            ActualizarFrase();
        }
    }


    public void SetEvidencia(int _index, Pista _evidencia)
    {
        evidencias[_index] = _evidencia;
        ActualizarFrase();
    }

    private void ActualizarFrase()
    {
        fraseHipotesis.ArmaFrase();
    }


    public void VerificarHipotesis()
    {
        controladorTutorial.ComprobarHipotesis();
        comprobandoTeoria.Mostrar();
        gAna.gv4.LogScreen(new AppViewHitBuilder()
           .SetScreenName("Comprobando teoria"));
        gAna.gv4.DispatchHits();
        comprobandoHipotesis = true;
    }

    public void CompletarVerificacion()
    {
        Debug.Log("Entre 129 ControladorHipotesis");
        bool gano = false;

        if (arma != null && motivo != null && sospechosoSeleccionado != null &&
            arma.Arma && motivo.Verdadero && sospechosoSeleccionado.Culpable)
        {
            gano = true;
            int evidenciasCompletadas = 0;
            for (int c = 0; c < 5; c++)
                if (evidencias[c] != null)
                {
                    evidenciasCompletadas++;
                    if (!evidencias[c].Verdadera)
                        gano = false;
                }
            if (evidenciasCompletadas < minimoPistasEnEvidencia)
                gano = false;
        }

        if (Arma != null)
        {
            gAna.gv4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("Seleccionar")
                    .SetEventAction("Arma")
                    .SetEventLabel(arma.Nombre));
            gAna.gv4.DispatchHits();
        }

        if (Motivo != null)
        {
            gAna.gv4.LogEvent(new EventHitBuilder()
                   .SetEventCategory("Seleccionar")
                   .SetEventAction("Motivo")
                   .SetEventLabel(motivo.Descripcion));
            gAna.gv4.DispatchHits();
        }

        if (SospechosoSeleccionado != null)
        {
            gAna.gv4.LogEvent(new EventHitBuilder()
                   .SetEventCategory("Seleccionar")
                   .SetEventAction("Sospechoso")
                   .SetEventLabel(SospechosoSeleccionado.Nombre));
            gAna.gv4.DispatchHits();
        }

        for (int c = 0; c < 5; c++)
            if (evidencias[c] != null)
            {
                gAna.gv4.LogEvent(new EventHitBuilder()
                  .SetEventCategory("Seleccionar")
                  .SetEventAction("Pista")
                  .SetEventLabel(evidencias[c].Nombre));
                gAna.gv4.DispatchHits();
            }

        if (gano)
        {



            gAna.gv4.LogEvent(new EventHitBuilder()
                .SetEventCategory("Niveles")
                .SetEventAction("Ganar")
                .SetEventLabel(SceneManager.GetActiveScene().name));
            //iohjkljh
           

            gAna.gv4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("GanarTiempo")
                    .SetEventAction(SceneManager.GetActiveScene().name)
                    .SetEventLabel(timer.TiempoRestante));

            gAna.gv4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("GanarOportunidades")
                    .SetEventAction(SceneManager.GetActiveScene().name)
                    .SetEventLabel(Convert.ToString(contador)));
          

            gAna.gv4.LogEvent(new EventHitBuilder()
                   .SetEventCategory("Zoom")
                   .SetEventAction(SceneManager.GetActiveScene().name)
                   .SetEventLabel(Convert.ToString(controladorCamara.ContadorZoom)));
            gAna.gv4.DispatchHits();



            if (SceneManager.GetActiveScene().name == "Caso1")
            {
                PlayerPrefs.SetString("Caso1Resuelto", "True");

            }
            if (SceneManager.GetActiveScene().name == "Caso2")
            {
                PlayerPrefs.SetString("Caso2Resuelto", "True");
            }
            if (SceneManager.GetActiveScene().name == "Caso3")
            {
                PlayerPrefs.SetString("Caso3Resuelto", "True");
            }

            PlayerPrefs.Save();

            SceneManager.LoadScene(VideoGanar);
            controladorCamara.Reset();
        }
        else
        {
            contador--;
            if (contador >= 1)
            {
                oportunidadesText.text = "Estás equivocado, te quedan " + contador.ToString() + " oportunidades";
                finalCasiPierdo.Mostrar();
                controladorCamara.Reset();
            }

            if (contador == 0)
            {
                gAna.gv4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("FinCaso")
                    .SetEventAction(SceneManager.GetActiveScene().name));

                gAna.gv4.LogEvent(new EventHitBuilder()
                   .SetEventCategory("Perder")
                   .SetEventAction(SceneManager.GetActiveScene().name)
                   .SetEventLabel("SinOportunidades"));
                gAna.gv4.DispatchHits();


                //finalPierdo.Mostrar();
                SceneManager.LoadScene(VideoPerder);
            }
        }
    }

}


