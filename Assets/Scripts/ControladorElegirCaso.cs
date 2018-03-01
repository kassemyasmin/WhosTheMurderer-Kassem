using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorElegirCaso : MonoBehaviour {
    bool firstFrame = true;
    private Button caso2;
    private Button caso3;
    private Persister persister;
    private LevelManager levelManager;
    private Analytics gAna;

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
        gAna = FindObjectOfType<Analytics>();

        gAna.gv4.LogScreen(new AppViewHitBuilder()
          .SetScreenName("Jugar"));
        gAna.gv4.DispatchHits();
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

        gAna.gv4.LogScreen(new AppViewHitBuilder()
          .SetScreenName("Elegir Caso"));
        gAna.gv4.DispatchHits();
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
        gAna.gv4.LogEvent(new EventHitBuilder()
            .SetEventCategory("ElegirCaso")
            .SetEventAction("IniciaCaso")
            .SetEventLabel(caso));
        gAna.gv4.DispatchHits();

        levelManager.LoadScene(caso);
    }


}
