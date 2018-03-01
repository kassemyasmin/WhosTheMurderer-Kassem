class EvidenciaEvIncomp : TipCondition
{
    private readonly ControladorHipotesis controladorHipotesis;

    public EvidenciaEvIncomp(ControladorHipotesis _controladorHipotesis) : base("EvidenciaEvIncomp")
    {
        controladorHipotesis = _controladorHipotesis;
    }

    public override bool Evaluate()
    {
        int evidencias=0;

        foreach (var ev in controladorHipotesis.Evidencias)
        {
            if (ev != null)
                evidencias++;
        }

        return evidencias < controladorHipotesis.MinimoPistasEnEvidencia;
    }
}


