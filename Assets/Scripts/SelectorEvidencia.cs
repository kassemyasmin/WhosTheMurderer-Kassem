using UnityEngine;
using UnityEngine.UI;


class SelectorEvidencia : MonoBehaviour
{

    private ListaInventarioUI listaInventario;
    private int currentIndex = 0;
    private ControladorHipotesis controladorHipotesis;
    private ControladorTutorial controladorTutorial;

    [SerializeField]
    int manejaEvidencia = -1;

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
        var start = currentIndex;
        bool found = false;

        controladorTutorial.SeleccionInventario();

        while (!found)
        {
            currentIndex++;
        if (currentIndex >= listaInventario.Pistas.Count)
            currentIndex = 0;
            found = ProcesoPista();
            if (currentIndex == start)
                found = true;
        }
    }

    public Pista PistaSeleccionada { get; private set; }

    public void Anterior()
    {
        var start = currentIndex;
        bool found= false;

        controladorTutorial.SeleccionInventario();

        while (!found)
        {
            currentIndex--;
            if (currentIndex < 0 && listaInventario.Pistas.Count != 0)
                currentIndex = listaInventario.Pistas.Count - 1;
           found= ProcesoPista();
            if (currentIndex == start)
                found = true;
        }
    }

    private void LLenaHipotesis()
    {
        if (manejaEvidencia != -1)
            controladorHipotesis.SetEvidencia(manejaEvidencia, PistaSeleccionada);
    }

    private bool ProcesoPista()
    {

        if (currentIndex < listaInventario.Pistas.Count)
        {
            bool found = false;

            //Me fijo que la evidencia no este ya seleccionada
            foreach (var ev in controladorHipotesis.Evidencias)
                if (ev != null && ev == listaInventario.Pistas[currentIndex])
                    found = true;

            if (found)
                return false;

            var imagen = this.GetComponentInChildren<Image>();

            imagen.sprite = listaInventario.Pistas[currentIndex].Imagen;

            PistaSeleccionada = listaInventario.Pistas[currentIndex];
            LLenaHipotesis();
            return true;
        }
        else
            return false;
    }
}