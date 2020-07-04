using System.Collections.Generic;
using UnityEngine;

public class TipGroup
{
    public TipGroup(TipCondition _condition, int _orden, IList<Tip> _tips)
    {
        Condition = _condition;
        Orden = _orden;
        Tips = _tips;
    }

    public TipCondition Condition { get; private set; }
    public int Orden { get; private set; }
    public IList<Tip> Tips { get; private set; }
}
