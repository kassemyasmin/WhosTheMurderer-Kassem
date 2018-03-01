using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MeGustoNoMeGusto : MonoBehaviour
{

    bool firstFrame = true;
    public bool Activo { get; private set; }
    bool? GustoActivo = null;
    bool? FacilActivo = null;
    private Analytics gAna;

    private Button siGusta;
    private Button noGusta;
    private Button siFueFacil;
    private Button noFueFacil;

    // Use this for initialization
    void Start()
    {
        gAna = FindObjectOfType<Analytics>();
        Activo = true;

        foreach (var b in GetComponentsInChildren<Button>())
        {
            switch (b.name)
            {
                case "SiGusto":
                    siGusta = b;
                    break;
                case "NoGusto":
                    noGusta = b;
                    break;
                case "SiFueFacil":
                    siFueFacil = b;
                    break;
                case "NoFueFacil":
                    noFueFacil = b;
                    break;
            }
        }
    }

    private T GetComponentInChildren<T>(string v)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstFrame)
        {
            Ocultar();
            firstFrame = false;
        }
    }

    public void Mostrar()
    {
        if (!Activo)
        {
            this.gameObject.SetActive(true);
            Activo = true;
        }
    }

    public void Ocultar()
    {
        this.gameObject.SetActive(false);
        Activo = false;
    }

    public void SiGusto()
    {

        siGusta.image.color = Color.red;
        noGusta.image.color = Color.grey;
        GustoActivo = true;

    }

    /*  private void DisableGusta()
      {
          siGusta.enabled = false;
          noGusta.enabled = false;
      }*/

    public void NoGusto()
    {
        noGusta.image.color = Color.red;
        siGusta.image.color = Color.gray;
        GustoActivo = false;
    }

    public void SiFueFacil()
    {

        siFueFacil.image.color = Color.red;
        noFueFacil.image.color = Color.gray;
        FacilActivo = true;

    }

    public void NoFueFacil()
    {

        noFueFacil.image.color = Color.red;
        siFueFacil.image.color = Color.gray;
        FacilActivo = false;

    }

    /*  private void DisableFacil()
      {
          siFueFacil.enabled = false;
          noFueFacil.enabled = false;
      }
      */

    public void Continuar(string siguienteEscena)
    {
        if (FacilActivo.HasValue)
        {
            gAna.gv4.LogEvent(new EventHitBuilder()
                        .SetEventCategory("MeGusto")
                        .SetEventAction(SceneManager.GetActiveScene().name)
                        .SetEventLabel(Convert.ToString(GustoActivo.Value)));
            gAna.gv4.DispatchHits();
        }

        if (GustoActivo.HasValue)
        {
            gAna.gv4.LogEvent(new EventHitBuilder()
                        .SetEventCategory("FueFacil")
                        .SetEventAction(SceneManager.GetActiveScene().name)
                        .SetEventLabel(Convert.ToString(FacilActivo.Value)));
            gAna.gv4.DispatchHits();
        }

        SceneManager.LoadScene(siguienteEscena);
    }
}

