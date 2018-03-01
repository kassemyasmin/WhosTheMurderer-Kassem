using UnityEngine;
using UnityEngine.UI;

class SelectorMotivo : MonoBehaviour
{

    private ManejadorDialogos manejadorDialogos;
    private int currentIndex = 0;
    private ControladorHipotesis controladorHipotesis;
    private ControladorTutorial controladorTutorial;
    private Text texto;

    // Use this for initialization
    void Start()
    {
        manejadorDialogos = FindObjectOfType<ManejadorDialogos>();
        controladorHipotesis = FindObjectOfType<ControladorHipotesis>();
        controladorTutorial = FindObjectOfType<ControladorTutorial>();

        foreach(var t in this.GetComponentsInChildren<Text>())
        {
            if (t.name == "TextoMotivo")
                texto = t;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Siguiente()
    {
        controladorTutorial.SeleccionInventario();
        currentIndex++;
        if (currentIndex >= manejadorDialogos.Motivos.Count)
            currentIndex = 0;
        ProcesoMotivo();
    }

    public Motivo MotivoSeleccionado { get; private set; }

    public void Anterior()
    {
        controladorTutorial.SeleccionInventario();

        currentIndex--;
        if (currentIndex < 0 && manejadorDialogos.Motivos.Count != 0)
            currentIndex = manejadorDialogos.Motivos.Count - 1;
        ProcesoMotivo();
    }

    private void LLenaHipotesis()
    {
        controladorHipotesis.Motivo = MotivoSeleccionado;
    }

    private void ProcesoMotivo()
    {
        if (currentIndex < manejadorDialogos.Motivos.Count)
        {
            texto.text = manejadorDialogos.Motivos[currentIndex].EnSelector;
            MotivoSeleccionado = manejadorDialogos.Motivos[currentIndex];
            LLenaHipotesis();
        }
    }
}
