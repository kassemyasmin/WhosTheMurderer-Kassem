using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ManejadorDialogos : ControladorCanvas
{
    private CargadorDialogos dialogos;
    private ListaInventarioUI inventario;
    private ColeccionSospechosos sospechosos;
    
    private Button[] campoLineas = new Button[6];
    private Text respuestaIntro;

    [SerializeField]
    private bool RecuerdaElecciones = false;

    [SerializeField]
    private string audioMuchasGracias="";

    private LineaDialogo[] lineasActivas = new LineaDialogo[6];
    private LineaDialogo muchasGracias;
    private string personajeActual;

    private HashSet<LineaDialogo> lineasElegidas = new HashSet<LineaDialogo>();

    private Dialogo dialogoActual;
    private LineaDialogo lineaActual;

    private IDictionary<string,int> dialogoEscuchado=new Dictionary<string, int>();
    private Cronometro cronometro;
    private float totalPlayTime;

    private bool closing = false;

    public AudioMixerGroup audioMixer;

    // Use this for initialization
    protected override void CanvasStart () {
        dialogos = FindObjectOfType<CargadorDialogos>();
        inventario = FindObjectOfType<ListaInventarioUI>();
        sospechosos = FindObjectOfType<ColeccionSospechosos>();

        var camposTexto = this.GetComponentsInChildren<Text>();

        foreach (Text t in camposTexto)
            if (t.name=="TextoResp.Intro")
            {
                respuestaIntro = t;
                break;
            }

        var botones = this.GetComponentsInChildren<Button>();
        foreach(Button b in botones)
        {
            int idx;
            var aux = b.name.Substring(8);

            if (b.name.StartsWith("Pregunta") && int.TryParse(b.name.Substring(8), out idx)) //hallo el numero detras del nombre del boton PreguntaX
                campoLineas[idx - 1] = b;
        }

        Motivos = new List<Motivo>();
        cronometro = gameObject.GetComponent<Cronometro>();
        gAna = FindObjectOfType<Analytics>();
        var audiosMuchasGracias = new List<string>();
        audiosMuchasGracias.Add(audioMuchasGracias);
        muchasGracias = new LineaDialogo(9999, "Muchas Gracias", "", false, new List<string>(), new List<string>(), true, null,audiosMuchasGracias, "");
    }

    protected override void CanvasUpdate()
    {
        base.CanvasUpdate();
        if (closing && this.totalPlayTime<cronometro.TiempoTranscurrido )
        {
            closing = false;
            cronometro.StopMeasurement();
            cronometro.ResetMeasurement();
            Ocultar();
        }
    }

    private void EligeOpcion(int opcion)
    {
        cronometro.StopMeasurement();
        if (dialogoEscuchado[dialogoActual.NombrePersonaje] == 1 && lineaActual!=null)
        {
            if (totalPlayTime <= cronometro.TiempoTranscurrido)
            {
                gAna.gv4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("EscuchoHastaFinal")
                    .SetEventAction(dialogoActual.NombrePersonaje)
                    .SetEventLabel(lineaActual.Pregunta)
                    );
                gAna.gv4.DispatchHits();
            }
            else
            {
                gAna.gv4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("NoEscuchoHastaFinal")
                    .SetEventAction(dialogoActual.NombrePersonaje)
                    .SetEventLabel(lineaActual.Pregunta)
                    .SetEventValue(Convert.ToInt64(cronometro.TiempoTranscurrido))
                    );
                gAna.gv4.DispatchHits();
            }
        }
        Limpiar();

        var lineaElegida = lineasActivas[opcion];
        lineaActual = lineaElegida;

        lineasElegidas.Add(lineaElegida);

        if (lineaElegida.VisibilizaNombre)
            sospechosos.VisibilizaNombre(dialogoActual.NombrePersonaje);

        ArmoOpciones(lineaElegida.Respuesta, lineaElegida.LineasSiguientes);

        if (lineaElegida.FinDialogo)
            closing = true;
        else
        {
            gAna.gv4.LogEvent(new EventHitBuilder()
                .SetEventCategory("Personaje")
                .SetEventAction(dialogoActual.NombrePersonaje)
                .SetEventLabel(lineaElegida.Pregunta));
            gAna.gv4.DispatchHits();
        }
        AgregarPistas(lineaElegida.Pistas);
        AgregarSospechosos(lineaElegida.Sospechosos);
        if (lineaElegida.Motivo != null)
            AgregarMotivo(lineaElegida.Motivo);
        PlayAudios(lineaElegida.Audios);

        cronometro.ResetMeasurement();
        cronometro.StartMeasurement();

    }

    private void AgregarMotivo(Motivo _motivo)
    {
        if (_motivo != null)
        {
            Motivos.Add(_motivo);
            gAna.gv4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("Motivo")
                    .SetEventAction(SceneManager.GetActiveScene().name)
                    .SetEventLabel(dialogoActual.NombrePersonaje));
            gAna.gv4.DispatchHits();
        }
    }


    private void AgregarPistas(IEnumerable<string> _pistas)
    {
        foreach (var s in _pistas)
            inventario.AddPista(dialogos.ObtenerPista(s));
    }

    private void AgregarSospechosos(IEnumerable<string> _sospechosos)
    {
        foreach (var s in _sospechosos)
            sospechosos.SospechosoEncontrado(s);
    }


    private void Limpiar()
    {
        StopAudios();
        respuestaIntro.text = "";
        foreach (var t in campoLineas)
            SetBoton(t,"");
        closing = false;
    }

    private void SetBoton(Button b, string texto)
    {
        var campoTexto = b.GetComponentInChildren<Text>();
        campoTexto.text = texto;
        if (texto=="")
            b.gameObject.SetActive(false);
        else
            b.gameObject.SetActive(true);
    }

    private void ArmoOpciones(string _introduccion,IEnumerable<LineaDialogo> _lineas)
    {
        int idx = 0;
        bool final = true;

        respuestaIntro.text = _introduccion;
        foreach (var linea in _lineas)
        {
            final = false;
            if (!lineasElegidas.Contains(linea))
            {
                lineasActivas[idx] = linea;
                SetBoton(campoLineas[idx++], linea.Pregunta);
                if (idx > 5)
                    throw (new Exception("Este dialogo tiene mas de 5 opciones" + personajeActual));
            }
        }
        if (final) // esto imnplica que no hay mas opciones
            ArmoOpciones(_introduccion, dialogoActual.LineasIniciales);
        else
        {
            lineasActivas[idx] = muchasGracias;
            SetBoton(campoLineas[idx], muchasGracias.Pregunta);
        }
    }


    public void MostrarDialogo(string _nombre)
    {
        Limpiar();
        if (!RecuerdaElecciones)
            lineasElegidas.Clear();

        dialogoActual = dialogos.ObtenerDialogo(_nombre);
        if (dialogoActual != null)
        {
            personajeActual = _nombre;
            ArmoOpciones(dialogoActual.Introduccion, dialogoActual.LineasIniciales);

            Mostrar();

            foreach (var linea in dialogoActual.LineasIniciales)
            {
                if (linea.AudioIntro!="")
                    PlayAudio(linea.AudioIntro, 0);
            }
        }

        if (!dialogoEscuchado.ContainsKey(_nombre))
            dialogoEscuchado.Add(_nombre, 0);
        dialogoEscuchado[_nombre]= dialogoEscuchado[_nombre]+1;
        lineaActual = null;
    }

    public void EligeOpcion1()
    {
        EligeOpcion(0);
    }

    public void EligeOpcion2()
    {
        EligeOpcion(1);
    }

    public void EligeOpcion3()
    {
        EligeOpcion(2);
    }

    public void EligeOpcion4()
    {
        EligeOpcion(3);
    }

    public void EligeOpcion5()
    {
        EligeOpcion(4);
    }

    public void EligeOpcion6()
    {
        EligeOpcion(5);
    }

    public void StopAudios()
    {
        foreach (var audioSource in gameObject.GetComponents<AudioSource>())
        {
            audioSource.Stop();
            Destroy(audioSource);
        }
    }

    public void PlayAudios(IEnumerable<string> _audios)
    {
        float timeToWait = 0;
        foreach(string audioName in _audios)
        {
            var len=PlayAudio(audioName, timeToWait);
            timeToWait += len; 
        }
        totalPlayTime = timeToWait;
    }

    private float PlayAudio(string _audio,float timeToWait)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>(_audio);
        audioSource.outputAudioMixerGroup = audioMixer;
        audioSource.Play(Convert.ToUInt32(timeToWait * audioSource.clip.samples / audioSource.clip.length));
        return audioSource.clip.length;
    }

    public IList<Motivo> Motivos { get; private set; }

}
