using System;
using System.Collections.Generic;
using UnityEngine;			 
								 
using UnityEngine.UI;

public class ControladorAjustesEnJuego : ControladorCanvas
{
    public int ResX;
    public int ResY;
    public bool Fullscreen;

    private Dropdown resoluciones;

    // Use this for initialization
    protected override void CanvasStart()
    {
        var opcionesResolucion = new List<Dropdown.OptionData>();

        resoluciones = GetComponentInChildren<Dropdown>();

        foreach (var res in Screen.resolutions)
        {
            opcionesResolucion.Add(new Dropdown.OptionData(res.width.ToString() + "x" + res.height.ToString()));
        }
        /*resoluciones.options.Clear();
        resoluciones.AddOptions(opcionesResolucion);*/
    }

    // Update is called once per frame
    protected override void CanvasUpdate()
    {
        if (firstFrame)
        {
            Ocultar();
            firstFrame = false;
        }
    }


    public void ChangeResolution()
    {
        var resolucion = Screen.resolutions[resoluciones.value];
        SetResolution(resolucion.width, resolucion.height, true);
    }

    private void SetResolution(int ancho, int alto, bool fullscreen)
    {
        Screen.SetResolution(ancho, alto, fullscreen);

        UnityEngine.Analytics.Analytics.CustomEvent("Ajustes", new Dictionary<string, object>
                    {
                        {
                            "Resolucion", Convert.ToString(ancho) + "x" + Convert.ToString(alto)
                        }
                    });

        UnityEngine.Analytics.Analytics.FlushEvents();

    }

    public void SubirVolumen()
    {
        if (AudioListener.volume <= 0f)
            AudioListener.volume += 0.1f;
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

    public void BajarVolumen()
    {
        if (AudioListener.volume >= 0f)
        {
            AudioListener.volume -= 0.1f;
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }

   //public bool Activo { get; private set; }


    public override void Mostrar()
    {
        if (!Activo)
        {
            this.gameObject.SetActive(true);
        }

        UnityEngine.Analytics.Analytics.CustomEvent("Ajustes en juego");
    }

    public override void Ocultar()
    {
        if (Activo)
        {
            this.gameObject.SetActive(false);

            UnityEngine.Analytics.Analytics.CustomEvent("Ajustes", new Dictionary<string, object>
                    {
                        {
                            "Volumen", AudioListener.volume
                        }
                    });

            UnityEngine.Analytics.Analytics.FlushEvents();
        }
    }
}