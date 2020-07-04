using UnityEngine;

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
            gAna.gv4.LogScreen(new AppViewHitBuilder()
                .SetScreenName("Inventario"));
            gAna.gv4.DispatchHits();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!conjuntoCanvas.Escape())
            {
                pausa.Togle();
                gAna.gv4.LogScreen(new AppViewHitBuilder()
                    .SetScreenName("Pausa"));
                gAna.gv4.DispatchHits();
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
