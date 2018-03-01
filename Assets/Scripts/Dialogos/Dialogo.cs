using System.Collections.Generic;

public class Dialogo  {
    
    public Dialogo(string _nombrePersonaje,string _introduccion, IList<LineaDialogo> _lineasIniciales)
    {
        NombrePersonaje = _nombrePersonaje;
        LineasIniciales = _lineasIniciales;
        Introduccion = _introduccion;
    }

    public string Introduccion { get; private set; }
    public string NombrePersonaje { get; private set; }
    public IEnumerable<LineaDialogo> LineasIniciales { get; private set; }
}
