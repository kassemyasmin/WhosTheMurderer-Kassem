using System;
using UnityEngine;

public class ControladorCanvas : MonoBehaviour {

    protected bool firstFrame = true;
    ControladorCamara camara;
    ControladorConjuntoCanvas conjuntoCanvas;
    ControladorClickeable controladorClickeable;
    protected Analytics gAna;

    // Use this for initialization
    void Start()
    {
        camara = FindObjectOfType<ControladorCamara>();
        conjuntoCanvas = FindObjectOfType<ControladorConjuntoCanvas>();
        controladorClickeable = FindObjectOfType<ControladorClickeable>();
        CanvasStart();
        Activo = true;
        gAna = FindObjectOfType<Analytics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstFrame)
        {
            Ocultar();
            firstFrame = false;
        }
        CanvasUpdate();
    }

    protected virtual void CanvasStart() { }
    protected virtual void CanvasUpdate() { }

    public bool Activo { get; private set; }

    public void Togle()
    {
        if (!Activo)
            Mostrar();
        else
            Ocultar();
    }

    public virtual void Mostrar()
    {
        if (!Activo)
        {
            this.gameObject.SetActive(true);
            camara.LockCamera();
            Activo = true;
            conjuntoCanvas.PushCanvas(this);
            controladorClickeable.DisableAll();
        }
    }

    public virtual void Ocultar()
    {
        if (Activo)
        {
            this.gameObject.SetActive(false);
            camara.UnlockCamera();
            Activo = false;
            if (!firstFrame)
                conjuntoCanvas.PopCanvas();
            controladorClickeable.EnableAll();
        }
    }
}
