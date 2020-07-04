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

    private Analytics gAna;
    private Dropdown resoluciones;

    // Use this for initialization
    void Start()
    {
        var opcionesResolucion = new List<Dropdown.OptionData>();

        Activo = true;
        gAna = FindObjectOfType<Analytics>();
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
        gAna.gv4.LogEvent(new EventHitBuilder()
            .SetEventCategory("Ajustes")
            .SetEventAction("Resolucion")
            .SetEventLabel(Convert.ToString(ancho) + "x" + Convert.ToString(alto)));
        gAna.gv4.DispatchHits();
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

        gAna.gv4.LogScreen(new AppViewHitBuilder()
            .SetScreenName("Ajustes"));
        gAna.gv4.DispatchHits();
    }

    public virtual void Ocultar()
    {
        if (Activo)
        {
            this.gameObject.SetActive(false);
            Activo = false;
            gAna.gv4.LogEvent(new EventHitBuilder()
            .SetEventCategory("Ajustes")
            .SetEventAction("Volumen")
            .SetEventLabel(Convert.ToString(AudioListener.volume)));
            gAna.gv4.DispatchHits();
        }
    }
}