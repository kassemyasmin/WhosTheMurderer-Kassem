using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ControladorElegirCaso : MonoBehaviour {
    bool firstFrame = true;
    private Button caso2;
    private Button caso3;
    private Persister persister;
    private LevelManager levelManager;

    // Use this for initialization
    void Start()
    {
        Activo = true;
        foreach (var b in GetComponentsInChildren<Button>())
        {
            if (b.name == "Caso2")
                caso2 = b;
            if (b.name == "Caso3")
                caso3 = b;
        }
        persister = FindObjectOfType<Persister>();
        levelManager = FindObjectOfType<LevelManager>();

        UnityEngine.Analytics.Analytics.CustomEvent("Screen", new Dictionary<string, object>
                    {
                        {
                            "Screen", "Jugar"
                        }
                    });

        UnityEngine.Analytics.Analytics.FlushEvents();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstFrame)
        {
            if (PlayerPrefs.GetString("Caso1Resuelto") == "False")
            {
                caso2.enabled = true;
                caso3.enabled = true;
            }
            Ocultar();
            firstFrame = false;
        }
    }

    public bool Activo { get; private set; }


    public virtual void Mostrar()
    {
        if (!Activo)
        {
            this.gameObject.SetActive(true);
            Activo = true;
        }

        UnityEngine.Analytics.Analytics.CustomEvent("Screen", new Dictionary<string, object>
                    {
                        {
                            "Screen", "Elegir Caso"
                        }
                    });

        UnityEngine.Analytics.Analytics.FlushEvents();
    }

    public virtual void Ocultar()
    {
        if (Activo)
        {
            this.gameObject.SetActive(false);
            Activo = false;
        }


    }

    public void IniciarCaso(string caso)
    {

        UnityEngine.Analytics.Analytics.CustomEvent("ElegirCaso", new Dictionary<string, object>
                    {
                        {
                            "IniciaCaso", caso
                        }
                    });

        UnityEngine.Analytics.Analytics.FlushEvents();

        levelManager.LoadScene(caso);
    }


}
