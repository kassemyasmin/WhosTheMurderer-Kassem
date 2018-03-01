
class MotivoMalaEleccCondition : TipCondition
{
    private readonly ControladorHipotesis controladorHipotesis;

    public MotivoMalaEleccCondition(ControladorHipotesis _controladorHipotesis) : base("MotivoMalaElecc")
    {
        controladorHipotesis = _controladorHipotesis;
    }

    public override bool Evaluate()
    {
        if (controladorHipotesis.Motivo == null)
            return false;

        return !controladorHipotesis.Motivo.Verdadero;
    }
}

