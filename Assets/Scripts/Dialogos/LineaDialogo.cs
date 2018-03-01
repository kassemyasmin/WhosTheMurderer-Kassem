using System.Collections.Generic;

public class LineaDialogo {

    private IList<LineaDialogo> lineasSiguientes=new List<LineaDialogo>();
    private IList<string> sospechososEncontrados;
    private IList<string> pistasEncontradas;
    private IList<string> audios;

    public LineaDialogo(int opcion, string _pregunta, string _respuesta, bool _visibilizaNombre, IList<string> _sospechososEncontrados, IList<string> _pistasEncontradas, bool _finDialogo , Motivo _motivo, IList<string> _audios, string _audioIntro)
    {
        Pregunta = _pregunta;
        Respuesta = _respuesta;
        VisibilizaNombre = _visibilizaNombre;
        sospechososEncontrados = _sospechososEncontrados;
        pistasEncontradas = _pistasEncontradas;
        FinDialogo = _finDialogo;
        Motivo=_motivo;
        Opcion = opcion;
        audios = _audios;
        AudioIntro = _audioIntro;
    }

    public void AddSiguiente(LineaDialogo linea)
    {
        lineasSiguientes.Add(linea);
    }

    public int Opcion { get; private set; }
    public string Pregunta { get; private set; }
    public string Respuesta { get; private set; }
    public IEnumerable<LineaDialogo> LineasSiguientes { get { return lineasSiguientes; } }
    public IEnumerable<string> Sospechosos { get { return sospechososEncontrados; } }
    public IEnumerable<string> Pistas { get { return pistasEncontradas; } }
    public bool VisibilizaNombre { get; private set; }
    public bool FinDialogo { get; private set; }
    public Motivo Motivo { get; private set; }
    public IEnumerable<string> Audios { get { return audios; } }
    public string AudioIntro { get; private set; }
}
