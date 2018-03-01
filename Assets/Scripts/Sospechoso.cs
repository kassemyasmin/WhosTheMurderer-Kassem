using UnityEngine;
using System.Collections;

public class Sospechoso : MonoBehaviour {

    [SerializeField]
    private string descripcion;

    [SerializeField]
    private Sprite imagen;

    [SerializeField]
    private string nombre;


    [SerializeField]
    private bool culpable;

    public string Descripcion { get { return descripcion; } }
    public Sprite Imagen { get { return imagen; } }
    public bool Culpable { get { return culpable; } }
    public string Nombre { get { return nombre; } }
    public bool NombreVisible { get; set; }


}
