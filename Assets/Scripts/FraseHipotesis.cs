using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[Serializable]
public class FraseHipotesis : MonoBehaviour {

    [SerializeField]
    private string separadorEvidencias;

    [SerializeField]
    private string separadorEvidenciasFinal;

    [SerializeField]
    private string fraseBase;
    [SerializeField]
    private string motivoDefault;
    [SerializeField]
    private string armaDefault;
    [SerializeField]
    private string armaComoDefault;
    [SerializeField]
    private string sospechosoDefault;
    [SerializeField]
    private string evidenciasDefault;

    ControladorHipotesis controladorHipotesis;
    Text textoFrase;

	// Use this for initialization
	void Start ()
    {
        controladorHipotesis = FindObjectOfType<ControladorHipotesis>();
        foreach(var t in GetComponentsInChildren<Text>())
        {
            if (t.name == "FraseHipotesis")
                textoFrase = t;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void ArmaFrase()
    {
        string frase = fraseBase;

        if (controladorHipotesis.Motivo!=null)
            frase = frase.Replace("%m", controladorHipotesis.Motivo.EnHipotesis);
        else
            frase = frase.Replace("%m", motivoDefault);
        if (controladorHipotesis.Arma != null)
        {
            frase = frase.Replace("%a", controladorHipotesis.Arma.Nombre);
            frase = frase.Replace("%ca", controladorHipotesis.Arma.ComoArma);
        }
        else
        {
            frase = frase.Replace("%a", armaDefault);
            frase = frase.Replace("%ca", armaComoDefault);
        }

        if (controladorHipotesis.SospechosoSeleccionado!=null)
            frase = frase.Replace("%s", controladorHipotesis.SospechosoSeleccionado.Nombre);
        else
            frase = frase.Replace("%s", sospechosoDefault);

        int c = 1;
        bool first = true;

        int count=0;
        foreach (var ev in controladorHipotesis.Evidencias)
            if (ev != null)
                count++;


        foreach (var ev in controladorHipotesis.Evidencias)
        {
            if (ev != null)
            {
                string sep;

                if (c == count)
                    sep = separadorEvidenciasFinal;
                else
                    sep = separadorEvidencias;
                if (first)
                {
                    sep = "";
                    first = false;
                }
                frase = frase.Replace("%e" + Convert.ToString(c++), sep + ev.Nombre);
            }
        }

        if (first)
            frase = frase.Replace("%e1","");

        frase=frase.Replace("%e2", "");
        frase =frase.Replace("%e3", "");

        textoFrase.text = frase;        
    }
}
