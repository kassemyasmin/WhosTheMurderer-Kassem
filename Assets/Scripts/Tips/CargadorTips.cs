using System;
using System.Collections.Generic;
using UnityEngine;

class CargadorTips : MonoBehaviour
{
    class LineData
    {
        public LineData(string _entidad, string _tipConditionId, int _orden, string _textoPista)
        {
            Entidad = _entidad;
            TipConditionId = _tipConditionId;
            Orden = _orden;
            TextoPista = _textoPista;
        }

        public string Entidad { get; private set; }
        public string TipConditionId { get; private set; }
        public int Orden { get; private set; }
        public string TextoPista { get; private set; }
    }

    class GroupId
    {
        public GroupId(string _entidad, string _tipConditionId, int _orden)
        {
            Entidad = _entidad;
            TipConditionId = _tipConditionId;
            Orden = _orden;
        }

        public string Entidad { get; private set; }
        public string TipConditionId { get; private set; }
        public int Orden { get; private set; }
    }

    private IList<LineData> datos;
    private IDictionary<string, TipCondition> tipConditions = new Dictionary<string, TipCondition>();

    [SerializeField]
    private TextAsset txtFile;


    // Use this for initialization
    void Start()
    {
        ListaInventarioUI listaInventarioUI;
        ColeccionSospechosos coleccionSospechosos;
        ControladorHipotesis controladorHipotesis;
        SelectorSospechoso selectorSospechoso;
        ManejadorDialogos manejadorDialogos;

        listaInventarioUI = FindObjectOfType<ListaInventarioUI>();
        coleccionSospechosos = FindObjectOfType<ColeccionSospechosos>();
        controladorHipotesis = FindObjectOfType<ControladorHipotesis>();
        selectorSospechoso = FindObjectOfType<SelectorSospechoso>();
        manejadorDialogos = FindObjectOfType<ManejadorDialogos>();

        //Aca debo popular el array de conditions
        TipCondition tipCondition = new ArmaNoInvCondition(listaInventarioUI);
        tipConditions.Add(tipCondition.TipConditionId,tipCondition);
        tipCondition = new MotivoNoInvCondition(manejadorDialogos);
        tipConditions.Add(tipCondition.TipConditionId, tipCondition);
        tipCondition = new SospechosoNoInvCondition(coleccionSospechosos);
        tipConditions.Add(tipCondition.TipConditionId, tipCondition);
        tipCondition = new ArmaMalaEleccCondition(controladorHipotesis);
        tipConditions.Add(tipCondition.TipConditionId, tipCondition);
        tipCondition = new MotivoMalaEleccCondition(controladorHipotesis);
        tipConditions.Add(tipCondition.TipConditionId, tipCondition);
        tipCondition = new SospechosoMalaEleccCondition(selectorSospechoso);
        tipConditions.Add(tipCondition.TipConditionId, tipCondition);
        tipCondition = new SiempreVerdaderoCondition("ArmaNoCondicion");
        tipConditions.Add(tipCondition.TipConditionId, tipCondition);
        tipCondition = new SiempreVerdaderoCondition("MotivoNoCondicion");
        tipConditions.Add(tipCondition.TipConditionId, tipCondition);
        tipCondition = new SiempreVerdaderoCondition("SospechosoNoCondicion");
        tipConditions.Add(tipCondition.TipConditionId, tipCondition);
        tipCondition = new EvidenciaEvIncomp(controladorHipotesis);
        tipConditions.Add(tipCondition.TipConditionId, tipCondition);
        tipCondition = new EvidenciaEvMal(controladorHipotesis);
        tipConditions.Add(tipCondition.TipConditionId, tipCondition);



        Tips = new List<TipGroup>();
        Load();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IList<TipGroup> Tips { get; private set; }



    private void Load()
    {
        var lineas = ReadTsv();

        IDictionary<GroupId , IList<LineData>> tipsBuffer = new Dictionary<GroupId, IList<LineData>>();

        foreach (string s in lineas)
        {
            LineData linea = ProcesarLinea(s);

            var groupId = new GroupId(linea.Entidad,linea.TipConditionId ,linea.Orden);

            if (!tipsBuffer.ContainsKey(groupId))
                tipsBuffer.Add(groupId, new List<LineData>());

            tipsBuffer[groupId].Add(linea);
        }

        foreach (var gId in tipsBuffer.Keys)
            Tips.Add(CrearTipGroup(gId.Entidad, gId.TipConditionId, gId.Orden, tipsBuffer[gId]));
    }

    private TipGroup CrearTipGroup(string _entidad,string _tipConditionId, int _orden,  IList<LineData> _lineas)
    {
        string fullTipConditionId = _entidad + _tipConditionId;

        if (!tipConditions.ContainsKey(fullTipConditionId))
            throw (new Exception("La condicion " + _tipConditionId + " para la entidad " + _entidad + " no ha sido implementada"));

        var condition = tipConditions[fullTipConditionId];

        IList<Tip> tips = new List<Tip>();

        foreach (var l in _lineas)
            tips.Add(new Tip(l.TextoPista));

        return new TipGroup(condition,_orden,tips);
    }

    private LineData ProcesarLinea(string _linea)
    {
        var campos = _linea.Split('\t');

        string entidad = campos[0];
        string tipConditionId = campos[1];
        int orden = Convert.ToInt32(campos[3]);
        string tip = campos[4];

        return new LineData(entidad,tipConditionId,orden,tip);
    }


    private IList<string> ReadTsv()
    {
        string[] lineas = this.txtFile.text.Split('\n');
        IList<string> resp = new List<string>();
        bool first = true;

        foreach (string s in lineas)
        {
            if (first)      //Evito la primera porque es la de los nombres de los campos
                first = false;
            else
            {
                var l = s.Replace("\r", "");
                if (l!="")
                    resp.Add(l);
            }
        }
        return resp;
    }


}
