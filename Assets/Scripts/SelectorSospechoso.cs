using UnityEngine;
using UnityEngine.UI;

public class SelectorSospechoso : MonoBehaviour {

    private ColeccionSospechosos sospechosos;
    private ControladorTutorial controladorTutorial;
    private int currentIndex = 0;
    private ControladorHipotesis controladorHipotesis;
    public Text texto;

    // Use this for initialization
    void Start () {
      
	    sospechosos = FindObjectOfType<ColeccionSospechosos>();
        controladorTutorial = FindObjectOfType<ControladorTutorial>();
        controladorHipotesis = FindObjectOfType<ControladorHipotesis>();
        foreach (var t in GetComponentsInChildren<Text>())
        {
            if (t.name == "NombreSospechoso")
                texto = t;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Siguiente()
    {
        controladorTutorial.SeleccionInventario();

        currentIndex++;
        if (currentIndex >= sospechosos.SospechososEncontrados.Count)
            currentIndex = 0;
        ProcesoSospechoso();
    }

    public Sospechoso SospechosoSeleccionado { get; private set; }

    public void Anterior()
    {
        controladorTutorial.SeleccionInventario();
        currentIndex--;
        if (currentIndex <0 && sospechosos.SospechososEncontrados.Count!=0)
            currentIndex = sospechosos.SospechososEncontrados.Count-1;
        ProcesoSospechoso();

    }

    private void ProcesoSospechoso()
    {
        if (currentIndex< sospechosos.SospechososEncontrados.Count)
        {
            var imagen = this.GetComponentInChildren<Image>();

            imagen.sprite = sospechosos.SospechososEncontrados.Values[currentIndex].Imagen;
            SospechosoSeleccionado = sospechosos.SospechososEncontrados.Values[currentIndex];
            controladorHipotesis.SospechosoSeleccionado = this.SospechosoSeleccionado;
            if (SospechosoSeleccionado.NombreVisible)
                texto.text = SospechosoSeleccionado.Nombre;
            else
                texto.text = "";
        }

    }
}
