
class SospechosoMalaEleccCondition : TipCondition
{
    private readonly SelectorSospechoso selectorSospechoso;

    public SospechosoMalaEleccCondition(SelectorSospechoso _selectorSospechoso) : base("SospechosoMalaElecc")
    {
        selectorSospechoso = _selectorSospechoso;
    }

    public override bool Evaluate()
    {
        if (selectorSospechoso.SospechosoSeleccionado == null)
            return false;

        return !selectorSospechoso.SospechosoSeleccionado.Culpable;
    }
}

