using UnityEngine;
using UnityEngine.SceneManagement;

public class Personaje : AssetClickeable{

    private ManejadorDialogos manejadorDialogos;
    private ControladorTutorial controladorTutorial;

   
    protected override void AssetStart()
    {
        base.AssetStart();
        manejadorDialogos = FindObjectOfType<ManejadorDialogos>();
        controladorTutorial = FindObjectOfType<ControladorTutorial>();
    }

    [SerializeField]
    private string nombre;

    protected override void OnMouseDown()
    {
        controladorTutorial.Dialogo();
        manejadorDialogos.MostrarDialogo(nombre);
        gAna.gv4.LogEvent(new EventHitBuilder()
                   .SetEventCategory("Hablar")
                   .SetEventAction(nombre)
                   .SetEventLabel(SceneManager.GetActiveScene().name)); 
        gAna.gv4.DispatchHits();
    }

}
