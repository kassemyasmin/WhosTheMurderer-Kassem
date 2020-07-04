using UnityEngine.SceneManagement;

public class Objeto : AssetClickeable
{

    PistaUI pistaUI;
    ListaInventarioUI listaInventario;
    ControladorTutorial controladorTutorial;



    // Use this for initialization
    protected override void AssetStart()
    {
        base.AssetStart();
        pistaUI = FindObjectOfType<PistaUI>();
        listaInventario = FindObjectOfType<ListaInventarioUI>();
        controladorTutorial = FindObjectOfType<ControladorTutorial>();
    }

    protected override void OnMouseDown()
    {
        var pistas = this.GetComponents<Pista>();

        controladorTutorial.ClickPista();

        if (pistas.GetLength(0) > 0)
        {
            pistaUI.ShowPista(pistas[0]);
            listaInventario.AddPista(pistas[0]);

            gAna.gv4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("EncontrarPista")
                    .SetEventAction(SceneManager.GetActiveScene().name)
                    .SetEventLabel("Pista " + pistas[0].Nombre));
            gAna.gv4.DispatchHits();
        }
    }

}
