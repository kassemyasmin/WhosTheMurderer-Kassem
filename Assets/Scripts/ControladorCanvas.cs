using System;
using UnityEngine;

public class ControladorCanvas : MonoBehaviour {

    protected bool firstFrame = true;
    ControladorCamara camara;
    ControladorConjuntoCanvas conjuntoCanvas;
    ControladorClickeable controladorClickeable;
    protected Analytics gAna;

    private ControladorCursor controladorCursor;

    // Use this for initialization
    void Start()
    {
        camara = FindObjectOfType<ControladorCamara>();
        conjuntoCanvas = FindObjectOfType<ControladorConjuntoCanvas>();
        controladorClickeable = FindObjectOfType<ControladorClickeable>();
        controladorCursor = FindObjectOfType<ControladorCursor>();
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
            if (camara == null)
                camara = FindObjectOfType<ControladorCamara>();
            camara.LockCamera();
            Activo = true;
            if (conjuntoCanvas == null)
                conjuntoCanvas = FindObjectOfType<ControladorConjuntoCanvas>();
            conjuntoCanvas.PushCanvas(this);
            if (controladorClickeable == null)
                controladorClickeable = FindObjectOfType<ControladorClickeable>();
            controladorClickeable.DisableAll();
            if (controladorCursor == null)
                controladorCursor = FindObjectOfType<ControladorCursor>();
            controladorCursor.canvasActivo = true;
            DisableMouseLook();
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
            {
                if (conjuntoCanvas == null)
                    conjuntoCanvas = FindObjectOfType<ControladorConjuntoCanvas>();
                conjuntoCanvas.PopCanvas();
            }
            if (controladorClickeable == null)
                controladorClickeable = FindObjectOfType<ControladorClickeable>();
            controladorClickeable.EnableAll();
            controladorCursor.canvasActivo = false;
            EnableMouseLook();
        }
    }

    private void EnableMouseLook()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisableMouseLook()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
