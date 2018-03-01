using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[Serializable]
public class Pista : MonoBehaviour {
    /*
    public Pista(string _descripcion, Image _imagen)
    {
        descripcion = _descripcion;
        imagen = _imagen;
    }
    */
    [SerializeField]
    private string nombre;


    [SerializeField]
    private string descripcion;

    [SerializeField]
    private Sprite imagen;

    [SerializeField]
    private bool arma;

    [SerializeField]
    private bool motivo;

//    [SerializeField]
//    private bool oportunidad;

	[SerializeField]
	private AudioSource pistaHablada;

    [SerializeField]
    private bool verdadera;

    [SerializeField]
    private string FormaUsoComoArma;

    [SerializeField]
    private string dependeDePista;

    public string Descripcion { get { return descripcion; } }
	public Sprite Imagen { get { return imagen; } }
	public string Nombre { get { return nombre; } }
	public AudioSource PistaHablada { get { return pistaHablada; }}
	public bool Arma { get { return arma; } }
	public bool Motivo { get { return motivo; } }
//	public bool Oportunidad { get { return oportunidad; } }
    public bool Verdadera { get { return verdadera; } }
    public string ComoArma { get { return FormaUsoComoArma; } }
    public string DependeDePista { get { return dependeDePista; } }
}
