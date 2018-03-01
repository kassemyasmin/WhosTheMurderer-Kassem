using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class ManejadorTips : MonoBehaviour
{
    private HashSet<Tip> tipsMostrados = new HashSet<Tip>();
    private Text texto;
    private CargadorTips cargadorTips;
    private Timer timer;

    [SerializeField]
    private string noTipMessage= "No hay más tips";

    public int ContadorTips { get; private set; }

    void Start()
    {
        foreach (var t in GetComponentsInChildren<Text>())
            if (t.name == "TextoTip")
                texto = t;
        cargadorTips = FindObjectOfType<CargadorTips>();
        timer = FindObjectOfType<Timer>();
        ContadorTips = 0;
    }

    public void Mostrar()
    {
        SeleccionarTip();
        timer.Substract();
        //        base.Mostrar();
        ContadorTips ++;
    }

    public void ResetMemory()
    {
        tipsMostrados = new HashSet<Tip>();
    }

    private void SeleccionarTip()
    {
        var tipsPosibles = ObtenerTipsPosibles() ;
        Tip tip;

        if (tipsPosibles.Count == 0)
        {
            tip = new Tip(noTipMessage);
            texto.text = tip.Mensaje;
        }
        else
        {
            tip=RandomSelect(tipsPosibles.Values[0]);
            tipsMostrados.Add(tip);
            texto.text = tip.Mensaje;
        }
    }

    private SortedList<int, IList<TipGroup>> ObtenerTipsPosibles()
    {
        SortedList<int, IList<TipGroup>> resp = new SortedList<int, IList<TipGroup>>();

        foreach (var tg in cargadorTips.Tips)
        {
            if (tg.Condition.Evaluate())
            {
                //Aca mismo voy a sacar aquellas que ya fueron mostradas
                IList<Tip> tips=new List<Tip>();

                foreach (var t in tg.Tips)
                    if (!tipsMostrados.Contains(t)) tips.Add(t);

                var tipGroupFiltrado = new TipGroup(tg.Condition, tg.Orden, tips);

                if (tips.Count != 0)
                {
                    IList<TipGroup> groups;
                    if (resp.ContainsKey(tipGroupFiltrado.Orden))
                        groups = resp[tipGroupFiltrado.Orden];
                    else
                    {
                        groups = new List<TipGroup>();
                        resp.Add(tipGroupFiltrado.Orden, groups);
                    }
                    groups.Add(tipGroupFiltrado);
                }
            }
        }

        return resp;
    }

    private Tip RandomSelect (IList<TipGroup> tipsPosibles)
    {
        System.Random rnd = new System.Random();

        var tg = tipsPosibles[rnd.Next(0, tipsPosibles.Count-1)];
        return tg.Tips[rnd.Next(0, tg.Tips.Count - 1)];
    }
}
