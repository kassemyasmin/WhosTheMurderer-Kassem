class EvidenciaEvMal : TipCondition
{
    private readonly ControladorHipotesis controladorHipotesis;

    public EvidenciaEvMal(ControladorHipotesis _controladorHipotesis) : base("EvidenciaEvMal")
    {
        controladorHipotesis = _controladorHipotesis;
    }

    public override bool Evaluate()
    {

        foreach (var ev in controladorHipotesis.Evidencias)
        {
            if (ev != null && !ev.Verdadera)
                return true;
        }
        return false;
    }
}


