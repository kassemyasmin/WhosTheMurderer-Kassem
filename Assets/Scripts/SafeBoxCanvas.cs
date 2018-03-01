using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SafeBoxCanvas : ControladorCanvas
{
    [SerializeField]
    string combinacion;
    string ingresado="";
    Text textoCombinacion;
    SafeBox safe;
    
    protected override void CanvasStart()
    {
        base.CanvasStart();

        safe = FindObjectOfType<SafeBox>();
        foreach (var t in GetComponentsInChildren<Text>())
            if (t.name == "TextoCombinacion")
                textoCombinacion = t;
    }


    public void KeyNumberDown(string _number)
    {
        ingresado += _number;
        CopiarTexto();
    }

    public void KeyBorrarDown()
    {
        ingresado = ingresado.Substring(0, ingresado.Length - 1);
        CopiarTexto();
    }

    public void KeyAbrirDown()
    {
        if (ingresado != combinacion)
        {
            ingresado = "";
            CopiarTexto();
            PlayError();
        }
        else
        {
            safe.Open();
            this.Ocultar();
        }
    }

    private void CopiarTexto()
    {
        textoCombinacion.text = ingresado;
    }

    private void PlayError()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

}

