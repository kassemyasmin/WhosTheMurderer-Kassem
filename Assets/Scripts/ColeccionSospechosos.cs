using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ColeccionSospechosos : MonoBehaviour {

    private SortedList<string, Sospechoso> sospechososDelCaso = new SortedList<string, Sospechoso>();
    private SortedList<string, Sospechoso> sospechososEncontrados = new SortedList<string, Sospechoso>();
    Analytics gAna;

    // Use this for initialization
    void Start () {
        var sospechososComponents = this.GetComponents<Sospechoso>();

        foreach(Sospechoso s in sospechososComponents)
        {
            sospechososDelCaso.Add(s.Nombre, s);
        }
        gAna = FindObjectOfType<Analytics>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public SortedList<string,Sospechoso> SospechososEncontrados { get { return sospechososEncontrados; } }


    public void SospechosoEncontrado(string _nombre)
    {
        if (!sospechososEncontrados.ContainsKey(_nombre))
            sospechososEncontrados.Add(_nombre, sospechososDelCaso[_nombre]);
        gAna.gv4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("EncontrarSospechoso")
                    .SetEventAction(SceneManager.GetActiveScene().name)
                    .SetEventLabel(_nombre));
        gAna.gv4.DispatchHits();
    }

    public void VisibilizaNombre(string nombre)
    {
        sospechososDelCaso[nombre].NombreVisible = true;
        if (sospechososEncontrados.ContainsKey(nombre))
            SospechososEncontrados[nombre].NombreVisible = true;
    }
}
