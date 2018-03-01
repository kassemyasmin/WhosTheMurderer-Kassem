using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ListaInventarioUI : MonoBehaviour {

    private IDictionary<string,Pista> pistas = new Dictionary<string,Pista>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    /* public void AgregarLineaInventario()
     {
         this.gameObject.AddComponent<InventarioUILine>();
     }*/

    public IList<Pista> Pistas { get { return new List<Pista>(pistas.Values); } } 

 //   private float lastY = 0;
    private void AgregarPistaInventario(Pista _pista)
    {
        pistas.Add(_pista.Nombre, _pista);
/*
        var nuevaLinea = CrearLineaInventario(_pista);

        var lista = this.gameObject.GetComponentInChildren<ListaInventarioUI>();
        nuevaLinea.transform.SetParent(lista.transform,false);

        var position =nuevaLinea.GetComponent<RectTransform>().position;
        position.y += lastY;
        nuevaLinea.GetComponent<RectTransform>().position = position;

        lastY -= 100;
        */
    }
    /*
    private GameObject CrearLineaInventario(Pista _pista)
    {
        var linea = FindObjectOfType<InventarioUILine>();
        var resp=Instantiate(linea.gameObject);

        var imagenes = resp.GetComponentsInChildren<Image>();
        Image imagen=null;

        foreach(Image i in imagenes)
            if (i.name=="ImgPistaInventario")
            {
                imagen = i;
                break;
            }
        var text = resp.GetComponentInChildren<Text>();

        imagen.sprite = _pista.Imagen;
        text.text = _pista.Descripcion;

        resp.GetComponent<InventarioUILine>().Pista = _pista;

        return resp;
    }
    */

    public void AddPista(Pista _pista) //TODO //Debo tener en cuenta no agregar repetidos
    {
        if (!pistas.ContainsKey(_pista.Nombre))
        {
            AgregarPistaInventario(_pista);
        }
    }
}
