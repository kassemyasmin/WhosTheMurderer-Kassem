using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorTeclado : MonoBehaviour
{

    Inventario inventario;
    Pausa pausa;
    ControladorConjuntoCanvas conjuntoCanvas;
    ControladorCamara camara;
    ControladorTutorial controladorTutorial;
    Analytics gAna;

    // Use this for initialization
    void Start()
    {

        inventario = FindObjectOfType<Inventario>();
        pausa = FindObjectOfType<Pausa>();
        conjuntoCanvas = FindObjectOfType<ControladorConjuntoCanvas>();
        camara = FindObjectOfType<ControladorCamara>();
        controladorTutorial = FindObjectOfType<ControladorTutorial>();
        gAna = FindObjectOfType<Analytics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            controladorTutorial.Movimiento();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            controladorTutorial.AbrirInventario();
            inventario.Togle();

            UnityEngine.Analytics.Analytics.CustomEvent("Inventario", new Dictionary<string, object>
                    {
                        {
                            "Inventario", SceneManager.GetActiveScene().name
                        }
                    });

            UnityEngine.Analytics.Analytics.FlushEvents();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!conjuntoCanvas.Escape())
            {
                UnityEngine.Analytics.Analytics.CustomEvent("Pausa", new Dictionary<string, object>
                    {
                        {
                            "Pausa", SceneManager.GetActiveScene().name
                        }
                    });

                UnityEngine.Analytics.Analytics.FlushEvents();

                pausa.Togle();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
            Zoom();
        if (Input.GetMouseButtonDown(1))
            Zoom();
    }

    private void Zoom()
    {
        controladorTutorial.Zoom();
        camara.ZoomTogle();
    }

}
