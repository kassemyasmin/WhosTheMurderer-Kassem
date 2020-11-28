using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class ControladorAjustes1 : MonoBehaviour
{
    bool firstFrame = true;


    public int ResX;
    public int ResY;
    public bool Fullscreen;

    private Dropdown resoluciones;

    // Use this for initialization
    void Start()
    {
        var opcionesResolucion = new List<Dropdown.OptionData>();

        Activo = true;
        resoluciones=GetComponentInChildren<Dropdown>();

        foreach(var res in Screen.resolutions)
        {
            opcionesResolucion.Add(new Dropdown.OptionData(res.width.ToString() + "x" + res.height.ToString()));
        }
        //resoluciones.options.Clear();
        //resoluciones.AddOptions(opcionesResolucion);
    }

    // Update is called once per frame
    void Update()
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
        SetResolution(resolucion.width, resolucion.height,true);
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

    public bool Activo { get; private set; }


    public virtual void Mostrar()
    {
        if (!Activo)
        {
            this.gameObject.SetActive(true);
            Activo = true;
        }

        UnityEngine.Analytics.Analytics.CustomEvent("Ajustes", new Dictionary<string, object>
                    {
                        {
                            "Screen", "Ajustes"
                        }
                    });

        UnityEngine.Analytics.Analytics.FlushEvents();
    }

    public virtual void Ocultar()
    {
        if (Activo)
        {
            this.gameObject.SetActive(false);
            Activo = false;

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