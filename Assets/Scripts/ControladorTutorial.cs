using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class ControladorTutorial : MonoBehaviour {

    Vector3 ultimaPosicion;
    private SortedList<int,PasoTutorial> pasos=new SortedList<int, PasoTutorial>();
    private int current = 0;
    private bool firstFrame=true;
    private Image imagenTutorial;

    [SerializeField]
    private bool active = false;

    private bool timerOn=false;
    private float tiempoRestante;

	// Use this for initialization
	void Start ()
    {
        foreach(var paso in this.GetComponents<PasoTutorial>())
            pasos.Add(paso.Orden, paso);

        imagenTutorial = GetComponentInChildren<Image>();
        AsignarPasoTutorial();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (firstFrame)
        {
            firstFrame = false;
            if (!active)
                Ocultar();
        }
        if (timerOn)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0)
            {
                Next();
            }
        }

    }

    private void ProcesarEventoTutorial(string evento)
    {
        if (pasos.Values[current].Evento == evento)
            Next();
    }

    private void Next()
    {
        if (current + 1 == pasos.Count)
            Ocultar();
        else
        {
            current++;
            AsignarPasoTutorial();
        }
    }

    private void AsignarPasoTutorial()
    {
        imagenTutorial.sprite = pasos.Values[current].Imagen;
        if (pasos.Values[current].timeOut > 0)
        {
            tiempoRestante = pasos.Values[current].timeOut;
            timerOn = true;
        }
        else
            timerOn = false;
    }

    
    public void Movimiento()
    {
        ProcesarEventoTutorial("Movimiento");
    }

    public void ComprobarHipotesis()
    {
        ProcesarEventoTutorial("ComprobarHipotesis");
    }

    public void Dialogo()
    {
        ProcesarEventoTutorial("Dialogo");
    }

    public void SeleccionInventario()
    {
        ProcesarEventoTutorial("SeleccionInventario");
    }

    public void Zoom()
    {
        ProcesarEventoTutorial("Zoom");
    }

    public void ClickPista()
    {
        ProcesarEventoTutorial("ClickPista");
    }

    public void AbrirInventario()
    {
        ProcesarEventoTutorial("AbrirInventario");
    }

    public void Tiempo()
    {
        ProcesarEventoTutorial("Tiempo");
    }

    public void Principal()
    {
        ProcesarEventoTutorial("Principal");
    }

    public void CursorCambia()
    {
        ProcesarEventoTutorial("CurosrCambia");
    }

    public virtual void Ocultar()
    {
        this.gameObject.SetActive(false);
    }
}
