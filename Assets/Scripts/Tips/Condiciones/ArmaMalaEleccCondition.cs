
class ArmaMalaEleccCondition : TipCondition
{
    private readonly ControladorHipotesis controladorHipotesis;

    public ArmaMalaEleccCondition(ControladorHipotesis _controladorHipotesis) : base("ArmaMalaElecc")
    {
        controladorHipotesis = _controladorHipotesis;
    }

    public override bool Evaluate()
    {
        if (controladorHipotesis.Arma == null)
            return false;
        return !controladorHipotesis.Arma.Arma;
    }
}

