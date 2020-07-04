using UnityEngine;
using System.Collections;

public class Tip
{
    public Tip( string _mensaje)
    {
        Mensaje = _mensaje;
    }

    public string Mensaje { get; private set; }

}
