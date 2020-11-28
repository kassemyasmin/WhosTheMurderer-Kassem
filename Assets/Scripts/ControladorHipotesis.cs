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

    public int feedbackCount;

    public ControladorCanvas inventario;

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
    /*public GameObject armaOkGo;
    public GameObject armaMalGo;*/
    private Motivo motivo;
    /*public GameObject motivoOkGo;
    public GameObject motivoMalGo;	*/
    private Sospechoso sospechosoSeleccionado;
    /*public GameObject sospechosoOkGo;
    public GameObject sospechosoMalGo;*/
    private Pista[] evidencias;
    /*public GameObject[] evidenciasOkGo;
    public GameObject[] evidenciasMalGo;*/
    private ManejadorTips tips;
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
        comprobandoTeoria.Ocultar();
        persister = FindObjectOfType<Persister>();
        tips = FindObjectOfType<ManejadorTips>();
        timer = FindObjectOfType<Timer>();
        controladorCamara = FindObjectOfType<ControladorCamara>();
    }

    // Update is called once per frame
    void Update()
    {
        if (comprobandoHipotesis && !comprobandoTeoria.Activo)
        {
            inventario.gameObject.GetComponent<Canvas>().enabled = true;
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

    public Sospechoso SospechosoSeleccionado
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
        comprobandoTeoria.LapsoDeTiempo = 8f;
        inventario.gameObject.GetComponent<Canvas>().enabled = false;

        UnityEngine.Analytics.Analytics.CustomEvent("Comprobando teoria");

        UnityEngine.Analytics.Analytics.FlushEvents();

        comprobandoHipotesis = true;
    }


    public void CompletarVerificacion()
    {
        Debug.Log("Entre 129 ControladorHipotesis");
        //HideFeedback();
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
            UnityEngine.Analytics.Analytics.CustomEvent("Seleccionar", new Dictionary<string, object>
                    {
                        {
                            "Arma", arma.Nombre
                        }
                    });

            UnityEngine.Analytics.Analytics.FlushEvents();

        }

        if (Motivo != null)
        {
            UnityEngine.Analytics.Analytics.CustomEvent("Seleccionar", new Dictionary<string, object>
                    {
                        {
                            "Motivo", motivo.Descripcion
                        }
                    });

            UnityEngine.Analytics.Analytics.FlushEvents();
        }

        if (SospechosoSeleccionado != null)
        {

            UnityEngine.Analytics.Analytics.CustomEvent("Seleccionar", new Dictionary<string, object>
                    {
                        {
                            "Sospechoso", SospechosoSeleccionado.Nombre
                        }
                    });

            UnityEngine.Analytics.Analytics.FlushEvents();
        }

        for (int c = 0; c < 5; c++)
            if (evidencias[c] != null)
            {
                UnityEngine.Analytics.Analytics.CustomEvent("Seleccionar", new Dictionary<string, object>
                    {
                        {
                            "Pista", evidencias[c].Nombre
                        }
                    });

                UnityEngine.Analytics.Analytics.FlushEvents();
            }

        if (gano)
        {
            UnityEngine.Analytics.Analytics.CustomEvent("Niveles", new Dictionary<string, object>
                    {
                        {
                            "Ganar", SceneManager.GetActiveScene().name
                        }
                    });

            UnityEngine.Analytics.Analytics.CustomEvent("GanarTiempo", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        },
                                                {
                            "Tiempo Restante", timer.TiempoRestante
                        }
                    });

            UnityEngine.Analytics.Analytics.CustomEvent("GanarOportunidades", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        },
                                                {
                            "contador", Convert.ToString(contador)
                        }
                    });

            UnityEngine.Analytics.Analytics.CustomEvent("Tips", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        },
                                                {
                            "Tips", Convert.ToString(tips.ContadorTips)
                        }
                    });

            UnityEngine.Analytics.Analytics.CustomEvent("Zoom", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        },
                                                {
                            "Zoom", Convert.ToString(controladorCamara.ContadorZoom)
                        }
                    });

            UnityEngine.Analytics.Analytics.FlushEvents();


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
            if (contador >= 1 && arma != null && motivo != null && sospechosoSeleccionado != null)
            {
                var contadorAux = 0;
                bool evidenciasCorrectas;
                string contarAuxFrase = "";
                if (arma.Arma) contadorAux++;
                else contadorAux--;
                if (motivo.Verdadero) contadorAux++;
                else contadorAux--;
                if (sospechosoSeleccionado.Culpable) contadorAux++;
                else contadorAux--;

                int evidenciasCompletadas = 0;

                for (int c = 0; c < 5; c++)
                    if (evidencias[c] != null)
                    {
                        evidenciasCompletadas++;
                        contadorAux++;
                        if (!evidencias[c].Verdadera)
                            evidenciasCorrectas = false;
                    }
                if (evidenciasCompletadas < minimoPistasEnEvidencia)
                    evidenciasCorrectas = false;
                if (contadorAux == 0)
                {
                    contarAuxFrase = "Tu teoría es completamente incorrecta";
                }
                else if (contadorAux == 1 || contadorAux == 2)
                {
                    contarAuxFrase = "Tu teoría tiene erroes, pero algunas pistas estan bien";
                }
                else if (contadorAux == 3 || contadorAux == 4)
                {
                    contarAuxFrase = "Tu teoría tiene pocos erroes, solo te queda adivinar cuales pistas estan incorrectas";
                }
                else if (contadorAux == 5)
                {
                    contarAuxFrase = "Ya casi lo adivinas, solo te queda corregir los últimos detalles";
                }

                oportunidadesText.text = contarAuxFrase + ". Te quedan " + contador.ToString() + " oportunidades. Deberías revisar " +
                    "tu hipótesis.";
                inventario.Ocultar();
                finalCasiPierdo.Mostrar();
                controladorCamara.Reset();
                /*if (feedbackCount >= contador)
                {
                    if (arma != null)
                    {
                        if (arma.Arma)
                        {
                            armaOkGo.SetActive(true);
                        }
                        else
                        {
                            armaMalGo.SetActive(true);
                        }
                    }

                    if (sospechosoSeleccionado != null)
                    {
                        if (sospechosoSeleccionado.Culpable)
                        {
                            sospechosoOkGo.SetActive(true);
                        }
                        else
                        {
                            sospechosoMalGo.SetActive(true);
                        }
                    }

                    if (motivo != null)
                    {
                        if (motivo.Verdadero)
                        {
                            motivoOkGo.SetActive(true);
                        }
                        else
                        {
                            motivoMalGo.SetActive(true);
                        }
                    }

                    for (int i = 0; i < evidencias.Length; i++)
                    {
                        if (evidencias[i] != null)
                        {
                            if (evidencias[i].Verdadera)
                            {
                                evidenciasOkGo[i].SetActive(true);
                            }
                            else
                            {
                                evidenciasMalGo[i].SetActive(true);
                            }
                        }
                    }*/

            }
            else
            {
                oportunidadesText.text = "Te falto seleccionar alguna pista.";
                inventario.Ocultar();
                finalCasiPierdo.Mostrar();
                controladorCamara.Reset();
            }

            if (contador == 0)
            {
                UnityEngine.Analytics.Analytics.CustomEvent("FinCaso", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        },
                                                {
                            "Perder","SinOportunidades"
                        }
                    });

                

                UnityEngine.Analytics.Analytics.FlushEvents();

                //finalPierdo.Mostrar();
                SceneManager.LoadScene(VideoPerder);
            }
        }
    }

    /*public void HideFeedback()
			{
				armaOkGo.SetActive(false);
				armaMalGo.SetActive(false);

				motivoOkGo.SetActive(false);
				motivoMalGo.SetActive(false);

				sospechosoOkGo.SetActive(false);
				sospechosoMalGo.SetActive(false);

				foreach (var evidenciaOkGo in evidenciasOkGo)
				{
					evidenciaOkGo.SetActive(false);
				}

				foreach (var evidenciaMalGo in evidenciasMalGo)
				{
					evidenciaMalGo.SetActive(false);
				}*/
}



