
public class Motivo
{
    public Motivo(string _descripcion, bool _verdadero, string _enSelector, string _enHipotesis)
    {
        Descripcion = _descripcion;
        Verdadero = _verdadero;
        EnHipotesis = _enHipotesis;
        EnSelector = _enSelector;
    }

    public string EnHipotesis { get; private set; }
    public string EnSelector { get; private set; }
    public string Descripcion { get; private set; }
    public bool Verdadero { get; private set; }


}
