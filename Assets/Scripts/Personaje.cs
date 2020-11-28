using System.Collections.Generic;
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

        UnityEngine.Analytics.Analytics.CustomEvent("Hablar", new Dictionary<string, object>
                    {
                        {
                            "Scene", SceneManager.GetActiveScene().name
                        },
                        {
                            "Nombre ",  nombre
                        }
                    });

        UnityEngine.Analytics.Analytics.FlushEvents();
    }

}
