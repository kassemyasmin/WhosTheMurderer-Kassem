using UnityEngine;
using UnityEngine.UI;

class SelectorArma : MonoBehaviour
{

    private ListaInventarioUI listaInventario;
    private int currentIndex = 0;
    private ControladorHipotesis controladorHipotesis;
    private ControladorTutorial controladorTutorial;

    // Use this for initialization
    void Start()
    {
        listaInventario = FindObjectOfType<ListaInventarioUI>();
        controladorHipotesis = FindObjectOfType<ControladorHipotesis>();
        controladorTutorial = FindObjectOfType<ControladorTutorial>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Siguiente()
    {
        controladorTutorial.SeleccionInventario();
        currentIndex++;
        if (currentIndex >= listaInventario.Pistas.Count)
            currentIndex = 0;
        ProcesoPista();
    }

    public Pista PistaSeleccionada { get; private set; }

    public void Anterior()
    {
        controladorTutorial.SeleccionInventario();
        currentIndex--;
        if (currentIndex < 0 && listaInventario.Pistas.Count != 0)
            currentIndex = listaInventario.Pistas.Count - 1;
        ProcesoPista();
    }

    private void LLenaHipotesis()
    {
        controladorHipotesis.Arma = PistaSeleccionada;
    }

    private void ProcesoPista()
    {
        if (currentIndex < listaInventario.Pistas.Count)
        {
            var imagen = this.GetComponentInChildren<Image>();

            imagen.sprite = listaInventario.Pistas[currentIndex].Imagen;

            PistaSeleccionada = listaInventario.Pistas[currentIndex];
            LLenaHipotesis();
        }
    }
}
