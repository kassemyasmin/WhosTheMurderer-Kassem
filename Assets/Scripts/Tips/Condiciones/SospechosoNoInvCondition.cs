
class SospechosoNoInvCondition : TipCondition
{
    private readonly ColeccionSospechosos sospechosos;

    public SospechosoNoInvCondition(ColeccionSospechosos _sospechosos) : base("SospechosoNoInv")
    {
        sospechosos = _sospechosos;
    }

    public override bool Evaluate()
    {
        foreach (var s in sospechosos.SospechososEncontrados.Values)
            if (s.Culpable)
                return false;
        return true;
    }
}
