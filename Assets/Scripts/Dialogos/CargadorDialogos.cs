using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class CargadorDialogos : MonoBehaviour {

    private IDictionary<string, Pista> pistasDialogos = new Dictionary<string, Pista>();

    class LineData
    {
        public LineData(string _nombre, string _introduccion, int _opcion, string _pregunta, string _respuesta, IList<int> _desbloqueaOpciones, bool _visibilizaNombre, IList<string> _sospechososEncontrados , IList<string> _pistasEncontradas, bool _motivo, bool _motivoVerdadero, string _motivoEnSelector, string _motivoEnHipotesis, IList<string> _audios,string _audioIntro)
        {
            Nombre = _nombre;
            Introduccion = _introduccion;
            Opcion = _opcion;
            Pregunta = _pregunta;
            Respuesta = _respuesta;
            DesbloqueaOpciones = _desbloqueaOpciones;
            VisibilizaNombre = _visibilizaNombre;
            SospechososEncontrados = _sospechososEncontrados;
            PistasEncontradas = _pistasEncontradas;
            EsMotivo = _motivo;
            EsMotivoVerdadero = _motivoVerdadero;
            MotivoEnSelector = _motivoEnSelector;
            MotivoEnHipotesis = _motivoEnHipotesis;
            Audios = _audios;
            AudioIntro = _audioIntro;
        }

        public string Nombre { get; private set; }
        public string Introduccion { get; private set; }
        public int Opcion { get; private set; }
        public string Pregunta { get; private set; }
        public string Respuesta { get; private set; }
        public IList<int> DesbloqueaOpciones  { get; private set; }
        public bool VisibilizaNombre { get; private set; }
        public IList<string> SospechososEncontrados { get; private set; }
        public IList<string> PistasEncontradas { get; private set; }
        public bool EsMotivo { get; private set; }
        public bool EsMotivoVerdadero { get; private set; }
        public string MotivoEnSelector { get; private set; }
        public string MotivoEnHipotesis { get; private set; }
        public IList<string> Audios { get; private set; }
        public string AudioIntro { get; private set; }
    }

    private IList<LineData> datos;

    private IDictionary<string, Dialogo> Dialogos = new Dictionary<string, Dialogo>();

    [SerializeField]
    private TextAsset txtFile;


    // Use this for initialization
    void Start () {
        Load();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Dialogo ObtenerDialogo(string _nombre)
    {
        if (!Dialogos.ContainsKey(_nombre))
            return null;
        return Dialogos[_nombre];
    }

    private void LoadPistas()
    {
        var pistas = this.gameObject.GetComponentsInChildren<Pista>();

        foreach (Pista p in pistas)
            pistasDialogos.Add(p.Nombre, p);
    }

    public Pista ObtenerPista(string _nombre)
    {
        return pistasDialogos[_nombre];
    }

    private void Load()
    {
        var lineas = ReadTsv();

        IDictionary<string, IList<LineData>> dialogosBuffer = new Dictionary<string, IList<LineData>>();

        foreach(string s in lineas)
        {
            LineData linea = ProcesarLinea(s);

            if (!dialogosBuffer.ContainsKey(linea.Nombre))
                dialogosBuffer.Add(linea.Nombre, new List<LineData>());

            dialogosBuffer[linea.Nombre].Add(linea);
        }

        foreach(string personaje in dialogosBuffer.Keys)
        {
            Dialogos.Add(personaje, CrearDialogo(personaje, dialogosBuffer[personaje]));
        }
    }

    private Dialogo CrearDialogo(string _nombre, IList<LineData> _lineas)
    {
        LineaDialogo[] bufferLineas = new LineaDialogo[_lineas.Count];
        IList<LineaDialogo> iniciales = new List<LineaDialogo>();
        string introduccion;
        IDictionary<int, IList<LineaDialogo>> indiceOpciones = new Dictionary<int, IList<LineaDialogo>>();

        introduccion = _lineas[0].Introduccion;

        for(int c=0; c<_lineas.Count;c++)
        {
            var linea = _lineas[c];

            Motivo motivo = null;
            if (linea.EsMotivo)
                motivo = new Motivo(linea.Respuesta, linea.EsMotivoVerdadero,linea.MotivoEnSelector,linea.MotivoEnHipotesis);

            if (!indiceOpciones.Keys.Contains(linea.Opcion))
                indiceOpciones.Add(linea.Opcion, new List<LineaDialogo>());
            var ld = new LineaDialogo(linea.Opcion, linea.Pregunta, linea.Respuesta, linea.VisibilizaNombre, linea.SospechososEncontrados, linea.PistasEncontradas, false, motivo,linea.Audios,linea.AudioIntro);
            bufferLineas[c] = ld;
            indiceOpciones[linea.Opcion].Add(ld);

            if (linea.Opcion == 1)
                iniciales.Add(ld);
        }

        for (int c = 0; c < _lineas.Count; c++)
        {
            var linea = _lineas[c];

            foreach (int idx in linea.DesbloqueaOpciones)
                if (idx != 0)
                    if (indiceOpciones.Keys.Contains(idx))
                        foreach (var lineaSiguiente in indiceOpciones[idx])
                            bufferLineas[c].AddSiguiente(lineaSiguiente);
                    else
                        Debug.LogError("Error en datos dialogo " + _nombre + "\r\n Linea: " + linea.Pregunta);
        }

        return new Dialogo(_nombre,introduccion, iniciales);
    }

    private LineData ProcesarLinea(string _linea)
    {
        var campos = _linea.Split('\t');
        var length = campos.GetLength(0);

        string nombre = campos[0];
        string introduccion = campos[1];
        int opcion = Convert.ToInt32(campos[3]);
        string pregunta = campos[4];
        string respuesta = campos[5];
        bool esMotivo = false;
        bool esMotivoVerdadero = false;
        string motivoEnSelector ;
        string motivoEnHipotesis =" aún no se de que manera" ;
        IList<string> audios = new List<string>();
        string audioIntro = "";

        IList<int> desbloqueos = new List<int>();
        string[] desbloq = campos[6].Split(',');

        foreach (string s in desbloq)
        {
            if (s!="" && s.Trim()!="0")
                desbloqueos.Add(Convert.ToInt32(s));
        }

        bool visibilizaNombre = false;

        if (length>7)
            visibilizaNombre=(campos[7] == "S");

        IList<string> sospechosos = new List<string>();

        if (length > 8)
        {
            string[] sosp = campos[8].Split(',');
            foreach (string s in sosp)
                if (s!="")
                    sospechosos.Add(s);
        }

        IList<string> pistas = new List<string>();
        if (length > 9)
        {
            string[] pist = campos[9].Split(',');
            foreach (string s in pist)
            {
                if (s != "")
                {
                    if (!pistasDialogos.ContainsKey(s))
                        throw (new Exception("Los dialogos no contienen una pista con el nombre" + s));
                    pistas.Add(s);
                }
            }
        }
        if (length > 10)
            esMotivo = (campos[10] == "S");
        if (length > 11)
            esMotivoVerdadero = (campos[11] == "S");
        motivoEnSelector = respuesta;
        if (length > 12)
            motivoEnSelector = campos[12];
        if (length > 13)
            motivoEnHipotesis = campos[13];
        if (length > 14 && campos[14]!="")
            foreach (string audio in campos[14].Split(';'))
                audios.Add(audio);
        if (length > 15)
            audioIntro = campos[15];

        return new LineData(nombre, introduccion, opcion, pregunta, respuesta, desbloqueos,visibilizaNombre,sospechosos,pistas,esMotivo,esMotivoVerdadero,motivoEnSelector,motivoEnHipotesis,audios,audioIntro);    }


    private IList<string> ReadTsv()
    {
        string[] lineas= this.txtFile.text.Split('\n');
        IList<string> resp = new List<string>();
        bool first = true;

        foreach (string s in lineas)
        {
            if (first)      //Evito la primera porque es la de los nombres de los campos
                first = false;
            else
            {
                var l = s.Replace("\r", "");
                if (l != "")
                    resp.Add(l);
            }
        }
        return resp;
    }

}


